using OpacMauiApp.Models;
using OpacMauiApp.Services;

namespace OpacMauiApp.Pages;

public partial class SelectLibraryPage : ContentPage
{
    List<LibrarySetupLimitMod> libs;


string username;
    string password;

    ApiService api = new();

    public SelectLibraryPage(
        List<LibrarySetupLimitMod> libraries,
        string user,
        string pass)
    {
        InitializeComponent();

        libs = libraries;

        username = user;
        password = pass;

        collectionLibraries.ItemsSource = libs;
    }

    private async void SelectLib_Clicked(object sender, EventArgs e)
    {
        try
        {
            var button = sender as Button;

            var lib =
                button?.BindingContext
                as LibrarySetupLimitMod;

            if (lib == null)
                return;

            // STEP 1
            // Temporary selected lib
            ApiService.SelectedLibId =
                lib.Id ?? 0;

            // STEP 2
            // LoginSetLibrary call
            var req = new LoginSetLibDecrCmd
            {
                login = username,
                password = password,
                libId = lib.Id.ToString(),
                fingerprint = DeviceInfo.Name
            };

            var response =
                await api.LoginSetLibrary(req);

            if (!response.IsSuccess)
            {
                await DisplayAlert(
                    "Error",
                    response.Mesg,
                    "OK");

                return;
            }

            // STEP 3
            // Save Token
            ApiService.Token =
                response.Data
                .AuthTokenResponse
                .AccessToken;

            await SecureStorage.SetAsync(
                "token",
                ApiService.Token);

            // STEP 4
            // Save Selected Library
            ApiService.SelectedLibId =
                response.Data.LibId ?? 0;

            // STEP 5
            // IMPORTANT:
            // Web me ye automatically hota tha
            // Isliye ab manually GetOpacDbs call karenge

            var dbResponse =
                await api.GetOpacDbs();

            if (dbResponse != null &&
                dbResponse.IsSuccess &&
                dbResponse.Data != null)
            {
                // Selected library ka database nikalo
                foreach (var db in dbResponse.Data)
                {
                    if (db.Libraries == null)
                        continue;

                    var selectedLibrary =
                        db.Libraries
                        .FirstOrDefault(x =>
                            x.LibId ==
                            ApiService.SelectedLibId);

                    if (selectedLibrary != null)
                    {
                        ApiService.SelectedDatabaseId =
                            db.DataBaseId ?? 0;

                        break;
                    }
                }
            }

            // STEP 6
            // Safety fallback
            if (ApiService.SelectedDatabaseId == 0)
            {
                ApiService.SelectedDatabaseId =
                    response.Data.DatabaseId ?? 0;
            }

            // Open App
            Application.Current.MainPage = new MainFlyoutPage();
            //new AppShell();
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Exception",
                ex.Message,
                "OK");
        }
    }


}
