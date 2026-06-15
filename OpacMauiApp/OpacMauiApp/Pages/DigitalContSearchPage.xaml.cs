using OpacMauiApp.ViewModels;

namespace OpacMauiApp.Pages;

public partial class DigitalContSearchPage : ContentPage
{
    public DigitalContSearchPage(
        DigitalContSearchViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}