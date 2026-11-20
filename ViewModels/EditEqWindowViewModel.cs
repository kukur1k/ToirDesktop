using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToirDesktop.ViewModels;

public partial class EditEqWindowViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    [ObservableProperty] private EqRequest _equipmentStr = new();

    // ← свойства для DatePicker
    [ObservableProperty] private DateTimeOffset? _dateStartWorkPicker;
    [ObservableProperty] private DateTimeOffset? _dateStartExpPicker;
    [ObservableProperty] private DateTimeOffset? _warrancyTimePicker;

    public List<string> Statuses { get; } = new()
        { "в работе", "в ремонте", "в простое", "выведено" };

    public EditEqWindowViewModel(MainWindowViewModel main, EqTableItem eq)
    {
        _main = main;

        EquipmentStr.Id           = Convert.ToInt32(eq.Id);
        EquipmentStr.InventNumber = eq.InventNumber;
        EquipmentStr.Name         = eq.Name;
        EquipmentStr.Model        = eq.Model;
        EquipmentStr.Manufacture  = eq.Manufacture;
        EquipmentStr.Workshop     = eq.Workshop;
        EquipmentStr.EqStatus     = eq.EqStatus;
        EquipmentStr.SerialNumber = eq.SerialNumber;

        EquipmentStr.DateStartWork = eq.DateStartWork;
        EquipmentStr.DateStartExp  = eq.DateStartExp;
        EquipmentStr.WarrancyTime  = eq.WarrancyTime;

        DateStartWorkPicker = ParseToOffset(eq.DateStartWork);
        DateStartExpPicker  = ParseToOffset(eq.DateStartExp);
        WarrancyTimePicker  = ParseToOffset(eq.WarrancyTime);
    }

    partial void OnDateStartWorkPickerChanged(DateTimeOffset? value)
        => EquipmentStr.DateStartWork = value?.ToString("yyyy-MM-dd");

    partial void OnDateStartExpPickerChanged(DateTimeOffset? value)
        => EquipmentStr.DateStartExp = value?.ToString("yyyy-MM-dd");

    partial void OnWarrancyTimePickerChanged(DateTimeOffset? value)
        => EquipmentStr.WarrancyTime = value?.ToString("yyyy-MM-dd");

    private static DateTimeOffset? ParseToOffset(string? s)
    {
        if (string.IsNullOrWhiteSpace(s)) return null;

        if (DateTimeOffset.TryParse(s, System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None, out var dt))
            return dt;

        if (DateTimeOffset.TryParse(s, System.Globalization.CultureInfo.CurrentCulture,
            System.Globalization.DateTimeStyles.None, out dt))
            return dt;

        return null;
    }
        

    [RelayCommand]
    public async Task SaveEq()
    {
         if (DateStartWorkPicker.HasValue)
            EquipmentStr.DateStartWork = DateStartWorkPicker.Value.ToString("yyyy-MM-dd");
        if (DateStartExpPicker.HasValue)
            EquipmentStr.DateStartExp = DateStartExpPicker.Value.ToString("yyyy-MM-dd");
        if (WarrancyTimePicker.HasValue)
            EquipmentStr.WarrancyTime = WarrancyTimePicker.Value.ToString("yyyy-MM-dd");

        await ApiService.Instance.PutEquipment(EquipmentStr.Id, EquipmentStr);
    }
}