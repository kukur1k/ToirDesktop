using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToirDesktop.ViewModels;

public partial class EqInfoCardViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    [ObservableProperty] EqTableItem _eq;

    public EqInfoCardViewModel(MainWindowViewModel main, int eqId)
    {
        _main = main;
        var allEq =  ApiService.Instance.GetTableEq(null, null);
        _ = LoadAsync(eqId);
    }

    private async Task LoadAsync(int eqId)
    {
        var allEq = await ApiService.Instance.GetTableEq(null, null);
        if (allEq is null) return;
        Eq = allEq.FirstOrDefault(x => x.Id == eqId.ToString());
    }

}
