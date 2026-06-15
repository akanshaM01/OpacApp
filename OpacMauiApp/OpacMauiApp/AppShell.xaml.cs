using OpacMauiApp.Pages;

namespace OpacMauiApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }

    private async void BasicCatalogue_Clicked(
        object sender,
        EventArgs e)
    {
        await Navigation.PushAsync(
            new BasicCataloguePage());
    }

    private async void JournalCatalogue_Clicked(
        object sender,
        EventArgs e)
    {
        var page =
            IPlatformApplication.Current.Services
            .GetRequiredService<JournalCataloguePage>();

        await Navigation.PushAsync(page);
    }
}