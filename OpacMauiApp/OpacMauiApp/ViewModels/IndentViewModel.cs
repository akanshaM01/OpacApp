using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpacMauiApp.Models;
using OpacMauiApp.Services;
using System.Collections.ObjectModel;

namespace OpacMauiApp.ViewModels;

public partial class IndentViewModel : ObservableObject
{
    private readonly ApiService _api;

    public ObservableCollection<IndentMod>
        Indents
    { get; } = new();

    private List<IndentMod>
        masterData = new();

    public IndentViewModel(ApiService api)
    {
        _api = api;

        DateTime? DateFrom;
        DateTime? DateTo;
    }

    [ObservableProperty]
    string indentNumber;

    [ObservableProperty]
    int? selectedDepartment;

    [ObservableProperty]
    DateTime? dateFrom;

    [ObservableProperty]
    DateTime? dateTo;

    [ObservableProperty]
    bool isBusy;

    [RelayCommand]
    async Task Search()
    {
        try
        {
            IsBusy = true;

            Indents.Clear();

            var response =
                await _api.CheckIndent();

            if (response == null ||
                !response.IsSuccess ||
                response.Data == null)
            {
                await Shell.Current.DisplayAlert(
                    "Info",
                    "No Record Found",
                    "OK");

                return;
            }

            masterData = response.Data;

            IEnumerable<IndentMod> query =
                masterData;

            if (!string.IsNullOrWhiteSpace(IndentNumber))
            {
                query = query.Where(x =>
                    !string.IsNullOrWhiteSpace(x.indentnumber) &&
                    x.indentnumber.Contains(
                        IndentNumber,
                        StringComparison.OrdinalIgnoreCase));
            }

            if (SelectedDepartment.HasValue &&
                SelectedDepartment > 0)
            {
                query = query.Where(x =>
                    x.departmentcode ==
                    SelectedDepartment);
            }

            if (DateFrom.HasValue)
            {
                query = query.Where(x =>
                    x.indentdate.HasValue &&
                    x.indentdate.Value.Date >=
                    DateFrom.Value.Date);
            }

            if (DateTo.HasValue)
            {
                query = query.Where(x =>
                    x.indentdate.HasValue &&
                    x.indentdate.Value.Date <=
                    DateTo.Value.Date);
            }

            var result =
                query.ToList();

            if (!result.Any())
            {
                await Shell.Current.DisplayAlert(
                    "Info",
                    "No Record Found",
                    "OK");

                return;
            }

            foreach (var item in result)
            {
                Indents.Add(item);
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
        IndentNumber = "";

        SelectedDepartment = null;

        DateFrom = null;

        DateTo = null;

        Indents.Clear();

        masterData.Clear();
    }
}