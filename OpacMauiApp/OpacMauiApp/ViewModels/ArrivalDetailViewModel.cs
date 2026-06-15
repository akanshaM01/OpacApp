using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpacMauiApp.Models;
using OpacMauiApp.Services;
using System.Collections.ObjectModel;

namespace OpacMauiApp.ViewModels;

public partial class ArrivalDetailViewModel
    : ObservableObject
{
    private readonly ApiService _api;

    public ObservableCollection<BookArrivalMod>
        Arrivals
    { get; } = new();

    [ObservableProperty]
    string arrMrnNo;

    [ObservableProperty]
    string orderNumber;

    [ObservableProperty]
    DateTime? dateFrom;

    [ObservableProperty]
    DateTime? dateTo;

    [ObservableProperty]
    bool isBusy;

    public ArrivalDetailViewModel(
        ApiService api)
    {
        _api = api;
    }

    [RelayCommand]
    async Task Search()
    {
        try
        {
            IsBusy = true;

            Arrivals.Clear();

            var cmd =
                new ArrivalCmd
                {
                    ArrivalId = 0,
                    arrmrnno = ArrMrnNo ?? "",
                    ordernumber = OrderNumber ?? "",
                    dateFrom = DateFrom,
                    dateTo = DateTo,
                    Uncataloged = true
                };

            var response =
                await _api.GetBookArrival(cmd);

            if (response == null ||
                !response.IsSuccess ||
                response.Data == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Info",
                    "No Record Found",
                    "OK");

                return;
            }

            foreach (var item in response.Data)
            {
                Arrivals.Add(item);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    void Reset()
    {
        ArrMrnNo = "";
        OrderNumber = "";

        DateFrom = null;
        DateTo = null;

        Arrivals.Clear();
    }
}