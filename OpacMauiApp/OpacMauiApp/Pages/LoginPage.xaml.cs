using OpacMauiApp.Models;
using OpacMauiApp.Services;

namespace OpacMauiApp.Pages;

public partial class LoginPage : ContentPage
{
    ApiService api = new ApiService();

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void btnLogin_Clicked(
        object sender,
        EventArgs e)
    {
        try
        {
            btnLogin.IsEnabled = false;
            btnLogin.Text = "Please Wait...";

            LoginCmd model = new()
            {
                Login = txtUser.Text,
                Password = txtPass.Text
            };

            var response = await api.Login(model);

            btnLogin.IsEnabled = true;
            btnLogin.Text = "LOGIN";

            if (response != null && response.IsSuccess)
            {
                // SAVE USER
                await SecureStorage.SetAsync(
                    "userid",
                    response.Data?.Userid ?? "");

                // OPEN LIBRARY PAGE
                await Navigation.PushAsync(
                    new SelectLibraryPage(
                        response.Data.LibIdAll,
                        txtUser.Text,
                        txtPass.Text
                    )
                );
            }
            else
            {
                await DisplayAlert(
                    "Error",
                    response?.Mesg ?? "Login Failed",
                    "OK");
            }
        }
        catch (Exception ex)
        {
            btnLogin.IsEnabled = true;
            btnLogin.Text = "LOGIN";

            await DisplayAlert(
                "Error",
                ex.Message,
                "OK");
        }
    }
}