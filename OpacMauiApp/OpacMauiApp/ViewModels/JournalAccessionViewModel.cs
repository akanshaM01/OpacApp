using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpacMauiApp.Models;
using OpacMauiApp.Services;
using System.Collections.ObjectModel;

namespace OpacMauiApp.ViewModels;

public partial class JournalAccessionViewModel : ObservableObject
{
    private readonly ApiService _api;

    public ObservableCollection<VJournalAccession>Journals { get; } = new();

    [ObservableProperty] string title;
    [ObservableProperty] string accno;
    [ObservableProperty] string dept;
    [ObservableProperty] string issn;
    [ObservableProperty] string publisher;
    [ObservableProperty] string agent;
    [ObservableProperty] string filterText;

    [ObservableProperty]
    DateTime? entryDateFrom;

    [ObservableProperty]
    DateTime? entryDateTo;

    [ObservableProperty]
    bool isBusy;

    public JournalAccessionViewModel(
        ApiService api)
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
                string.IsNullOrWhiteSpace(Agent) &&
                EntryDateFrom == null &&
                EntryDateTo == null)
            {
                await Shell.Current.DisplayAlert(
                    "Warning",
                    "Enter Some Values",
                    "OK");

                return;
            }

            IsBusy = true;

            Journals.Clear();

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

            if (response != null &&
                response.IsSuccess &&
                response.Data != null)
            {
                foreach (var item in response.Data)
                {
                    item.ShowMore = false;
                    Journals.Add(item);
                }

                if (Journals.Count == 0)
                {
                    await Shell.Current.DisplayAlert(
                        "Info",
                        "No Record Found",
                        "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert(
                    "Info",
                    response?.Mesg ?? "No Record Found",
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
        Title = string.Empty;
        Accno = string.Empty;
        Dept = string.Empty;
        Issn = string.Empty;
        Publisher = string.Empty;
        Agent = string.Empty;

        EntryDateFrom = null;
        EntryDateTo = null;

        FilterText = string.Empty;

        Journals.Clear();
    }

    [RelayCommand]
    void ToggleMore(
        VJournalAccession item)
    {
        item.ShowMore = !item.ShowMore;

        var index =
            Journals.IndexOf(item);

        if (index >= 0)
        {
            Journals.RemoveAt(index);
            Journals.Insert(index, item);
        }
    }
}