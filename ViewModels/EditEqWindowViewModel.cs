using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToirDesktop.ViewModels;

public partial class EditEqWindowViewModel : ViewModelBase
{
    // private readonly MainWindowViewModel _main;

    // [ObservableProperty] EqRequest? _equipmentStr = null;

    // public EditEqWindowViewModel(MainWindowViewModel main, EqTableItem eq)
    // {
    //     _main = main;
    //     EquipmentStr.Id = eq.Id;
    //     EquipmentStr.InvNumber = eq.InventNumber;
    //     EquipmentStr.Name = eq.Name;
    //     EquipmentStr.Model = eq.Model;
    //     EquipmentStr.WorkStart = eq.DateStartWork;
    //     EquipmentStr.Manifacture = eq.Manufacture;
    //     EquipmentStr.Workshop = eq;
    //     EquipmentStr.Status = eq.EqStatus;
    // }


    // [RelayCommand]
    // public async Task SaveEq()
    // {
    //     await ApiService.Instance.PutEquipment(EquipmentStr.Id, EquipmentStr);
    // }
}
