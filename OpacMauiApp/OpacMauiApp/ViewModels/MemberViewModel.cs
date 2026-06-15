using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpacMauiApp.Models;
using OpacMauiApp.Services;
using System.Collections.ObjectModel;

namespace OpacMauiApp.ViewModels;

public partial class MemberViewModel : ObservableObject
{
    private readonly ApiService _api;

    public ObservableCollection<CircUserMod>Members{ get; } = new();

    private List<CircUserMod>masterData = new();

    [ObservableProperty]
    string memberId;

    [ObservableProperty]
    bool isBusy;

    public MemberViewModel(ApiService api)
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
            {
                return;
            }

            masterData = response.Data;

            IEnumerable<CircUserMod> query =
                masterData;

            if (!string.IsNullOrWhiteSpace(MemberId))
            {
                query = query.Where(x =>
                    !string.IsNullOrWhiteSpace(x.usercode) &&
                    x.usercode.Trim().Equals(
                        MemberId.Trim(),
                        StringComparison.OrdinalIgnoreCase));
            }

            foreach (var item in query)
            {
                Members.Add(item);
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert(
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
        MemberId = "";

        Members.Clear();

        masterData.Clear();
    }
}