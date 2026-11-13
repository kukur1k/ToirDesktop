using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToirDesktop.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    [ObservableProperty] string _email = string.Empty;
    [ObservableProperty] string _pass = string.Empty;

    [ObservableProperty] string _errorText = string.Empty;
     [ObservableProperty] bool _hasError = false;

    public LoginViewModel(MainWindowViewModel main)
    {
        _main = main;
    }

    [RelayCommand]
    public async Task OnLogin()
    {
        var error = await ApiService.Instance.LoginAsync(Email, Pass);
        if (error is not null)
        {
            ErrorText = error.ToString();
            HasError = true;
            return;
        }
        var user = await ApiService.Instance.GetMe();
        if (user is null)
        {
            ErrorText = "пользователь пуст";
            HasError = true;
            return;
        }
        _main.OnLogged(String.Join(" ", user.LastName, user.FirstName), user.Role);
        _main.GoToDashboardPage();
    }
}
