using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpacMauiApp.Models;
using OpacMauiApp.Services;
using System.Collections.ObjectModel;

namespace OpacMauiApp.ViewModels;

public partial class JournalArrivalViewModel : ObservableObject
{
    private readonly ApiService _api;

    public ObservableCollection<JournalArrival>
        Arrivals
    { get; } = new();

    [ObservableProperty]
    string journalNo;

    [ObservableProperty]
    string filterText;

    [ObservableProperty]
    bool isPending;

    [ObservableProperty]
    bool isArrived;

    [ObservableProperty]
    DateTime? arrivalFrom;

    [ObservableProperty]
    DateTime? arrivalTo;

    [ObservableProperty]
    bool isBusy;

    private List<JournalArrival> masterData = new();
      
    public JournalArrivalViewModel(ApiService api)
    {
        _api = api;
    }

    [RelayCommand]
    async Task Search()
    {
        try
        {
            IsBusy = true;

            var response =
                await _api.Getjournal_arrival_view();

            Arrivals.Clear();

            if (response?.Data == null)
                return;

            masterData = response.Data;

            IEnumerable<JournalArrival> query =
                masterData;

            if (!string.IsNullOrWhiteSpace(JournalNo))
            {
                query = query.Where(x =>
                    !string.IsNullOrWhiteSpace(x.journal_no) &&
                    x.journal_no.Contains(
                        JournalNo,
                        StringComparison.OrdinalIgnoreCase));
            }

            //query = query.Where(x =>
            //    x.arr_date.HasValue &&
            //    x.arr_date.Value.Date >= ArrivalFrom.Date &&
            //    x.arr_date.Value.Date <= ArrivalTo.Date);
            if (ArrivalFrom.HasValue)
            {
                query = query.Where(x =>
                    x.arr_date.HasValue &&
                    x.arr_date.Value.Date >= ArrivalFrom.Value.Date);
            }

            if (ArrivalTo.HasValue)
            {
                query = query.Where(x =>
                    x.arr_date.HasValue &&
                    x.arr_date.Value.Date <= ArrivalTo.Value.Date);
            }
            if (IsArrived && !IsPending)
            {
                query = query.Where(x =>
                    !string.IsNullOrWhiteSpace(x.Status) &&
                    x.Status.Trim().Equals("Arrived",
                    StringComparison.OrdinalIgnoreCase));
            }
            else if (IsPending && !IsArrived)
            {
                query = query.Where(x =>
                    string.IsNullOrWhiteSpace(x.Status) ||
                    !x.Status.Trim().Equals("Arrived",
                    StringComparison.OrdinalIgnoreCase));
            }

            foreach (var item in query)
            {
                Arrivals.Add(item);
            }

            //if (Arrivals.Count == 0)
            //{
            //    await Shell.Current.DisplayAlert(
            //        "Info",
            //        "No Record Found",
            //        "OK");
            //}
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    void Reset()
    {
        JournalNo = "";

        IsPending = false;
        IsArrived = false;

        ArrivalFrom = null;
        ArrivalTo = null;

        Arrivals.Clear();
    }
}