using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpacMauiApp.Models;
using OpacMauiApp.Services;
using System.Collections.ObjectModel;

namespace OpacMauiApp.ViewModels;

public partial class JournalCatalogueViewModel
    : ObservableObject
{
    private readonly ApiService _api;

    public ObservableCollection<VJournalAccession>
        Journals
    { get; set; } = new();

    [ObservableProperty]
    string title;

    [ObservableProperty]
    string accno;

    [ObservableProperty]
    string dept;

    [ObservableProperty]
    string issn;

    [ObservableProperty]
    string publisher;

    [ObservableProperty]
    string agent;

    [ObservableProperty]
    DateTime? entryDateFrom;

    [ObservableProperty]
    DateTime? entryDateTo;

    [ObservableProperty]
    bool isBusy;

    public JournalCatalogueViewModel(ApiService api)
    {
        _api = api;
    }

    [RelayCommand]
    async Task Search()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Title) &&
                string.IsNullOrWhiteSpace(Accno) &&
                string.IsNullOrWhiteSpace(Dept) &&
                string.IsNullOrWhiteSpace(Issn) &&
                string.IsNullOrWhiteSpace(Publisher) &&
                string.IsNullOrWhiteSpace(Agent))
            {
                await Shell.Current.DisplayAlert(
                    "Warning",
                    "Enter Some Values",
                    "OK");

                return;
            }

            IsBusy = true;

            var req = new AccnSearchAdv
            {
                Title = Title,
                Accno = Accno,
                Dept = Dept,
                Issn = Issn,
                publisher = Publisher,
                Agent = Agent,
                EntryDateFrom = EntryDateFrom,
                EntryDateTo = EntryDateTo
            };

            var response =
                await _api.SearchJourAccession(req);

            Journals.Clear();

            if (response != null &&
                response.IsSuccess &&
                response.Data != null)
            {
                foreach (var item in response.Data)
                {
                    Journals.Add(item);
                }
            }
            else
            {
                await Shell.Current.DisplayAlert(
                    "Info",
                    response?.Mesg ?? "No Data Found",
                    "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert(
                "Error",
                ex.Message,
                "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    void Reset()
    {
        Title = "";
        Accno = "";
        Dept = "";
        Issn = "";
        Publisher = "";
        Agent = "";

        EntryDateFrom = null;
        EntryDateTo = null;

        Journals.Clear();
    }
}