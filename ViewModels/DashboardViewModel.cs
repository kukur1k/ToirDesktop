using CommunityToolkit.Mvvm.ComponentModel;

namespace ToirDesktop.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    public DashboardViewModel(MainWindowViewModel main)
    {
        _main = main;
    }
}
