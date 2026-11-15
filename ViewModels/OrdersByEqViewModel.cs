using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToirDesktop.ViewModels;

public partial class OrdersByEqViewModel : ViewModelBase
{
    public readonly MainWindowViewModel _main;
    
    [ObservableProperty] public List<WorkOrder> _workOrders;
    

        
    public OrdersByEqViewModel(MainWindowViewModel main, int eqId)
    {
        _main = main;
        _ = LoadWOrdersAsync(eqId);
    }

    public async Task<string?> LoadWOrdersAsync(int eqId)
    {
        var res = await ApiService.Instance.GetWoByEq(eqId);
        if (res is null)
        {
            return "error";
        }

        WorkOrders = res;
        return null;
    }

  
}
