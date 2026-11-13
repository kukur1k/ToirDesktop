using CommunityToolkit.Mvvm.ComponentModel;

namespace ToirDesktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase _currentPage;
    [ObservableProperty] private string _userName = "Фамилия. И.О";
    [ObservableProperty] private string _userRole = "Master";
    
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
    }

    public void GoToDashboardPage() => CurrentPage = new DashboardViewModel(this);



}
