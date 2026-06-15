using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OpacMauiApp.Models;
using OpacMauiApp.Pages;
using OpacMauiApp.Services;

namespace OpacMauiApp.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly ApiService _api;

    [ObservableProperty]
    string userName;

    [ObservableProperty]
    string password;

    [ObservableProperty]
    string loginResponse;

    [ObservableProperty]
    bool isBusy;

    public LoginViewModel(ApiService api)
    {
        _api = api;
    }

    [RelayCommand]
    async Task Login()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                LoginResponse = "Enter User ID";
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                LoginResponse = "Enter Password";
                return;
            }

            IsBusy = true;

            LoginCmd model = new()
            {
                Login = UserName,
                Password = Password
            };

            var response = await _api.Login(model);

            IsBusy = false;

            if (response == null)
            {
                LoginResponse = "API response null";
                return;
            }


            if (response.IsSuccess)
            {
                ApiService.Token =
                    response.Data.UniqueId ?? "";

                await Application.Current.MainPage.Navigation.PushAsync(
    new SelectLibraryPage(
        response.Data.LibIdAll,
        UserName,
        Password));
            }
            else
            {
                LoginResponse = response.Mesg;
            }
        }
        catch (Exception ex)
        {
            IsBusy = false;
            LoginResponse = ex.Message;
        }
    }
}