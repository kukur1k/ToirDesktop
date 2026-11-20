using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Flowery.Controls.Scheduler.Models;

namespace ToirDesktop.ViewModels;

public partial class SheduleViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;



    [ObservableProperty] public List<SheduleToItem> _tableShedule = new();


    [ObservableProperty] public List<SheduleToItem> _filteredShedule = new();
   
    public List<string> Statuses { get; } = new()
         {"запланировано", "назначено",
															"выполняется",
															"выполнено",
															"отменено",
															"просрочено"};

    public SheduleViewModel(MainWindowViewModel main)
    {
        _main = main;
        _ = LoadDataAsync();
    }


    private async Task<string> LoadDataAsync()
    {
        // по идее в задании 1 дата, можно поменять на 1 параметр в апи
        var data = await ApiService.Instance.GetSheduleTo(new sheduleFiltersRequest(null, null, null, null));
        if (data == null)
        {
            return "error";
        }
        TableShedule = data;
        UpdateFilteredSchedule();

        Appointments = 
            data.Select(x => 
            
            //эти поля обязательны для отметок
            new AppointmentModel
            {
                Id = x.Id,
                Subject = $"{x.EqName} - {x.ToTitle}",
                Start = DateTime.Parse(x.Date).Date.AddHours(9),
                End = DateTime.Parse(x.Date).Date.AddHours(10)
            }
            ).ToList();


        return "";
    }



    [ObservableProperty] public DateTime? _selectedDate = null;

    [ObservableProperty] public List<AppointmentModel> _appointments;


    partial void OnSelectedDateChanged(DateTime? value)
    {
        UpdateFilteredSchedule();
    }

    private void UpdateFilteredSchedule()
    {
        if (SelectedDate == null)
        {
            FilteredShedule = TableShedule;
            return;
        }

        FilteredShedule = TableShedule
            .Where(x =>
                DateTime.Parse(x.Date) == SelectedDate.Value.Date)
            .ToList();
    }
    
}