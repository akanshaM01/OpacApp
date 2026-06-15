using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpacMauiApp.Models;
using OpacMauiApp.Services;
using System.Collections.ObjectModel;

namespace OpacMauiApp.ViewModels;

public partial class JournalIssueViewModel : ObservableObject
{
    private readonly ApiService _api;

    public ObservableCollection<JournalIssueMod>
        Issues
    { get; } = new();

    private List<JournalIssueMod>
        masterData = new();

    [ObservableProperty]
    string memberCode;

    [ObservableProperty]
    DateTime? issueFrom;

    [ObservableProperty]
    DateTime? issueTo;

    [ObservableProperty]
    bool isBusy;

    public JournalIssueViewModel(ApiService api)
    {
        _api = api;
    }

    [RelayCommand]
    async Task Issued()
    {
        await LoadData("Issued");
    }

    [RelayCommand]
    async Task Returned()
    {
        await LoadData("Returned");
    }

    async Task LoadData(string statusFilter)
    {
        try
        {
            IsBusy = true;

            Issues.Clear();

            var response =
                await _api.GetJournalIssue();

            if (response == null ||
                !response.IsSuccess ||
                response.Data == null)
            {
                return;
            }

            masterData = response.Data;

            IEnumerable<JournalIssueMod> query =
                masterData;

            // Member Code

            if (!string.IsNullOrWhiteSpace(MemberCode))
            {
                query = query.Where(x =>
                    !string.IsNullOrWhiteSpace(x.MemberCode) &&
                    x.MemberCode.Trim().Equals(
                        MemberCode.Trim(),
                        StringComparison.OrdinalIgnoreCase));
            }

            // Date From

            if (IssueFrom.HasValue)
            {
                query = query.Where(x =>
                    x.IssueDate.HasValue &&
                    x.IssueDate.Value.Date >=
                    IssueFrom.Value.Date);
            }

            // Date To

            if (IssueTo.HasValue)
            {
                query = query.Where(x =>
                    x.IssueDate.HasValue &&
                    x.IssueDate.Value.Date <=
                    IssueTo.Value.Date);
            }

            // Status

            if (!string.IsNullOrWhiteSpace(statusFilter))
            {
                query = query.Where(x =>
                    !string.IsNullOrWhiteSpace(x.IssStatus) &&
                    x.IssStatus.Trim().Equals(
                        statusFilter.Trim(),
                        StringComparison.OrdinalIgnoreCase));
            }

            query = query
                .OrderByDescending(x => x.IssueDate);

            foreach (var item in query)
            {
                Issues.Add(item);
            }
        }
        catch
        {
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    void Reset()
    {
        MemberCode = "";

        IssueFrom = null;
        IssueTo = null;

        Issues.Clear();

        masterData.Clear();
    }
}