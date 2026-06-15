using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpacMauiApp.Models;
using OpacMauiApp.Services;
using System.Collections.ObjectModel;

namespace OpacMauiApp.ViewModels;

public partial class CheckIssueReturnViewModel : ObservableObject
{
    private readonly ApiService _api;

    public ObservableCollection<CircIssueTransactionMod>
        Transactions
    { get; } = new();

    private List<CircIssueTransactionMod>
        masterData = new();

    [ObservableProperty]
    string accessionNo;

    [ObservableProperty]
    string userId;

    [ObservableProperty]
    bool isBusy;

    public CheckIssueReturnViewModel(
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

            Transactions.Clear();

            var response =
                await _api.CheckIssueRetSearch();

            if (response == null ||
                !response.IsSuccess ||
                response.Data == null)
            {
                await MainThread.InvokeOnMainThreadAsync(
                    async () =>
                    {
                        await Shell.Current.DisplayAlert(
                            "Info",
                            "No Record Found",
                            "OK");
                    });

                return;
            }

            masterData = response.Data;

            IEnumerable<CircIssueTransactionMod>
                query = masterData;

            if (!string.IsNullOrWhiteSpace(
                AccessionNo))
            {
                query = query.Where(x =>
                    !string.IsNullOrWhiteSpace(
                        x.accno) &&
                    x.accno.Contains(
                        AccessionNo,
                        StringComparison
                            .OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(
                UserId))
            {
                query = query.Where(x =>
                    !string.IsNullOrWhiteSpace(
                        x.userid1) &&
                    x.userid1.Contains(
                        UserId,
                        StringComparison
                            .OrdinalIgnoreCase));
            }

            var finalData =
                query.ToList();

            if (!finalData.Any())
            {
                await MainThread.InvokeOnMainThreadAsync(
                    async () =>
                    {
                        await Shell.Current.DisplayAlert(
                            "Info",
                            "No Matching Record Found",
                            "OK");
                    });

                return;
            }

            foreach (var item in finalData)
            {
                Transactions.Add(item);
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
        AccessionNo = "";
        UserId = "";

        Transactions.Clear();

        masterData.Clear();
    }
}