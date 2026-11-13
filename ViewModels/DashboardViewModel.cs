using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ToirDesktop.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    [ObservableProperty] public List<TableOrderResponse?> _table = new();
    [ObservableProperty] public int _tableItemsCount = 0;

    public DashboardViewModel(MainWindowViewModel main)
    {
        _main = main;
        _ = LoadTableAsync();
    }

    public async Task<string> LoadTableAsync()
    {
       
        var res = await ApiService.Instance.GetTableOrders();
        if (res?.data is null)
        {
            Table = new List<TableOrderResponse?>();
            TableItemsCount = 0;
            return "error";
        }
        Table = res.data;
        return "";

    }
}
