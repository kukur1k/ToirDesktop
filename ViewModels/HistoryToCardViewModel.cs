using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToirDesktop.ViewModels;

public partial class HistoryTOCardViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    [ObservableProperty] public HistoryCardTO _cardTo = new(null, null, null);

   public HistoryTOCardViewModel(MainWindowViewModel main, int EqId)
    {
        _main = main;
        _ = LoadCardToAsync(EqId);
    }

    public async Task<string?> LoadCardToAsync(int EqId)
    {
        var res = await ApiService.Instance.GetHistoryTo(EqId);
        if (res is null)
        {
            return "error";
        }
        CardTo = res;
        return null;
    }

}
