using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpacMauiApp.Models;
using OpacMauiApp.Services;
using System.Collections.ObjectModel;

namespace OpacMauiApp.ViewModels;

public partial class MemberOverdueDetailViewModel : ObservableObject
{
    private readonly ApiService _api;

    public ObservableCollection<CircUserMod>
        Members
    { get; } = new();

    private List<CircUserMod> masterData = new();

    [ObservableProperty]
    string memberCode;

    [ObservableProperty]
    bool isBusy;

    public MemberOverdueDetailViewModel(
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

            Members.Clear();

            var response =
                await _api.FindMember();

            if (response == null ||
                !response.IsSuccess ||
                response.Data == null)
                return;

            masterData = response.Data;

            IEnumerable<CircUserMod> query =
                masterData;

            if (!string.IsNullOrWhiteSpace(MemberCode))
            {
                query = query.Where(x =>
                    !string.IsNullOrWhiteSpace(x.usercode) &&
                    x.usercode.Trim().Equals(
                        MemberCode.Trim(),
                        StringComparison.OrdinalIgnoreCase));
            }

            foreach (var item in query)
            {
                Members.Add(item);
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
        MemberCode = "";

        Members.Clear();

        masterData.Clear();
    }
}