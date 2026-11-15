using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToirDesktop.ViewModels;

public partial class EquipmentViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    [ObservableProperty] public List<EqTableItem?> _tableEq = new();
    [ObservableProperty] public int _eqItemsCount = 0; 
   
    public List<string> Statuses {get; } = new List<string>{"в работе","в ремонте", "в простое","выведено","все статусы"};

    [ObservableProperty] public string _searchStr = string.Empty;

    [ObservableProperty] public string _selectedStatus = "все статусы";

    public EquipmentViewModel(MainWindowViewModel main)
    {
        _main = main;
        _ = LoadTableAsync();
    }

    private async Task<string> LoadTableAsync()
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
}
