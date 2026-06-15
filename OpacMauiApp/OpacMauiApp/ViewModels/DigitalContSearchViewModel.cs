using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpacMauiApp.Models;
using OpacMauiApp.Services;
using System.Collections.ObjectModel;

namespace OpacMauiApp.ViewModels;

public partial class DigitalContSearchViewModel
    : ObservableObject
{
    private readonly ApiService _api;

    public ObservableCollection<DigitalContMod>
        Contents
    { get; } = new();

    private List<DigitalContMod>
        masterData = new();

    [ObservableProperty]
    string title;

    [ObservableProperty]
    bool isBusy;

    public DigitalContSearchViewModel(
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

            Contents.Clear();

            var response =
                await _api.SearchDigitalContent();

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

            masterData =
                response.Data.ToList();

            IEnumerable<DigitalContMod> query =
                masterData;

            if (!string.IsNullOrWhiteSpace(Title))
            {
                query = query.Where(x =>
                    (!string.IsNullOrWhiteSpace(x.Title) &&
                     x.Title.Contains(
                         Title,
                         StringComparison.OrdinalIgnoreCase))
                    ||
                    (!string.IsNullOrWhiteSpace(x.Descr) &&
                     x.Descr.Contains(
                         Title,
                         StringComparison.OrdinalIgnoreCase)));
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
                Contents.Add(item);
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
        Title = string.Empty;

        Contents.Clear();

        masterData.Clear();
    }
}