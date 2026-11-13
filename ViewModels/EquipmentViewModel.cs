using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ToirDesktop.ViewModels;

public partial class EquipmentViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    [ObservableProperty] public List<EqTableItem?> _tableEq = new();
    [ObservableProperty] public int _eqItemsCount = 0; 
   

    public EquipmentViewModel(MainWindowViewModel main)
    {
        _main = main;
        _ = LoadTableAsync();
    }

    private async Task<string> LoadTableAsync()
    {
       
        var res = await ApiService.Instance.GetTableEq();
        if (res is null)
        {
            TableEq = new List<EqTableItem?>();
            EqItemsCount = 0;
            return "error";
        }
        TableEq = res;
        return "";

    }

}
