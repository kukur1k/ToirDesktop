using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToirDesktop.ViewModels;

public partial class EqDetailsViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    [ObservableProperty] ViewModelBase _currentPage;
    [ObservableProperty] int _eqId;

    [ObservableProperty] public string _title = string.Empty;

    public EqDetailsViewModel(MainWindowViewModel main, int eqId)
    {
        _main = main;
        EqId = eqId;
        GoToMainEqInfo();
    }

    [RelayCommand]
    public void GoToMainEqInfo()
    {
        Title = "Основная информация";
        CurrentPage = new EqInfoCardViewModel(_main, EqId);
    }

    [RelayCommand]
    public void GoToEqHistory()
    {
        Title = "История ТО";
        CurrentPage = new HistoryTOCardViewModel(_main, EqId);
    }

    [RelayCommand]
    public void GoToOrderHistory()
    {
        Title = "Заявки";
        CurrentPage = new OrdersByEqViewModel(_main, EqId);
    }
}
