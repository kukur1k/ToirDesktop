using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToirDesktop.Views;

namespace ToirDesktop.ViewModels;

public partial class EquipmentViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    [ObservableProperty] public List<EqTableItem?> _tableEq = new();

    [ObservableProperty] public EqTableItem? _selectedItem = null;
    [ObservableProperty] public int _eqItemsCount = 0; 
   
    public List<string> Statuses {get; } = new List<string>{"в работе","в ремонте", "в простое","выведено","все статусы"};

    [ObservableProperty] public string _searchStr = string.Empty;

    [ObservableProperty] public string _selectedStatus = "все статусы";

    [ObservableProperty] public string _errorLoad = string.Empty;
    [ObservableProperty] public bool _hasError = false;


    public EquipmentViewModel(MainWindowViewModel main)
    {
        _main = main;
        _ = LoadTableAsync();
    }

    public async Task<string> LoadTableAsync()
    {
        //=======Пофиксить в АПИ и в ПРИЛОЖЕНИИ=======
        // Эндпоинты этой группы обеспечивают полный цикл
        //  управления оборудованием на предприятии: просмотр списка
        //   оборудования с возможностью фильтрации по цеху, статусу,
        //    модели или поиску по инвентарному номеру
       
        var res = await ApiService.Instance.GetTableEq(SelectedStatus, SearchStr);
        if (res is null)
        {
            TableEq = new List<EqTableItem?>();
            EqItemsCount = 0;
            return "error";
        }
        TableEq = res;
        return "";

    }

    partial void OnSelectedStatusChanged(string value)
    {
        _ = LoadTableAsync();
    }

    partial void OnSearchStrChanged(string value)
    {
        _ = LoadTableAsync();
    }

    [RelayCommand]
    public void ClearFilters()
    {
        SelectedStatus = "все статусы";
        SearchStr = string.Empty;
        _ = LoadTableAsync();
    }

    [RelayCommand]
    public void GoToCardTo(string eqId)
    {
        var id = Convert.ToInt32(eqId);
        _main.GoToHistoryCardPage(id);
    }
    
    [RelayCommand]
    public void GoToWoByEq(string eqId)
    {
        var id = Convert.ToInt32(eqId);
        _main.GoToOrdersByEqPage(id);
    }

    [RelayCommand]
    public async Task DeleteEq()
    {
        if (SelectedItem is null) return;
        var error = await ApiService.Instance.DeleteEquipment(Convert.ToInt32(SelectedItem.Id));
        if (error is not null)
        {
            ErrorLoad = error;
            HasError = true;
        }
        else
        {
            ErrorLoad = string.Empty;
            HasError = false;
        }
        _ = await LoadTableAsync();
    }


    
     [RelayCommand]
    public void GoToEqDetailPage(string eqId){
        _main.CurrentPage = new EqDetailsViewModel(_main, Convert.ToInt32(eqId));
    }

     [RelayCommand]
    public async Task EditEq()
    {
        var modal = new Window
        {
            Title = "Редактировать",
            Width = 450,
            Height = 450,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Content = new EditEqWindowView
            {
                DataContext = new EditEqWindowViewModel(_main, SelectedItem)
            }
        };

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            await modal.ShowDialog(desktop.MainWindow);
        }

        await LoadTableAsync();
    }


}
