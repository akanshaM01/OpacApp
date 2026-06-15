using OpacMauiApp.Models;
using OpacMauiApp.Pages;

namespace OpacMauiApp;

public partial class App : Application
{
    public static int SelectedLibraryId;
    public static int SelectedDatabaseId { get; set; }
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(
            new LoginPage());
    }
}