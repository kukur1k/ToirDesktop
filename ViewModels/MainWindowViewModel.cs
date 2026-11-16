using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToirDesktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase _currentPage;
    [ObservableProperty] private string _userName = "Фамилия. И.О";
    [ObservableProperty] private string _userRole = "Мастер";

    [ObservableProperty] public string _workshop = "Имя цеха";
    
    [ObservableProperty] public string _initial = "ФИ";
    [ObservableProperty] private string _lastUpdated = "";

    public MainWindowViewModel()
    {
        _currentPage = new LoginViewModel(this);
    }

    public bool IsLoginPage => CurrentPage is LoginViewModel;


    partial void OnCurrentPageChanged(ViewModelBase value)
    {
        OnPropertyChanged(nameof(IsLoginPage));
    }
    
    public void OnLogged(string fullName, string role)
    {
        UserName = fullName;
        UserRole = role;

        var parts = fullName.Split(' ');
        Initial = parts[0].Substring(0, 1).ToUpper() + parts[1].Substring(0, 1).ToUpper();
        
    }

    [RelayCommand]
    public void GoToDashboardPage() => CurrentPage = new DashboardViewModel(this);

   [RelayCommand]
    public void GoToEquipmentPage() => CurrentPage = new EquipmentViewModel(this);
    
    [RelayCommand]
    public void GoToHistoryCardPage(int eqId) => CurrentPage = new HistoryTOCardViewModel(this, eqId);
    [RelayCommand]
    public void GoToOrdersByEqPage(int eqId) => CurrentPage = new OrdersByEqViewModel(this, eqId);



}
