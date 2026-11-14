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
    [ObservableProperty] public DashboardKPI? _kpi = null;

    [ObservableProperty] public string? _eqCountStr = null;
    [ObservableProperty] public int? _countActiveOrders = 0;
    [ObservableProperty] public int? _countLostOrders = 0;
    [ObservableProperty] public int? _nowPlanOrders = 0;
    [ObservableProperty] public int? _nowCreateOrders = 0;
    [ObservableProperty] public List<SheduleUpcomingTO?> _SheduleUpcTo = new();


    public DashboardViewModel(MainWindowViewModel main)
    {
        _main = main;
        _ = LoadTableAsync();
        _ = LoadKpiCard();
        _ = LoadSheduleUpcomingToAsync();
    }

    public async Task<string> LoadSheduleUpcomingToAsync()
    {
        var res = await ApiService.Instance.GetUpcomingTO();
        if (res is null)
        {
            return "error";
        }
        SheduleUpcTo = res;
        return "";

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

    public async Task<string> LoadKpiCard()
    {
        var res = await ApiService.Instance.GetMainDashboardKPI();
        if (res is null)
        {
            return "error";
        }
        Kpi = res;
        EqCountStr = Kpi.eqCountString;
        CountActiveOrders = Kpi.countActiveOrders;
        CountLostOrders = Kpi.countActiveOrdersLost;
        NowPlanOrders = Kpi.nowPlanOrders;
        NowCreateOrders = Kpi.nowCreateOrders;
        return "";
    }
}
