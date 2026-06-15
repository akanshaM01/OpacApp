using OpacMauiApp.ViewModels;

namespace OpacMauiApp.Pages;

public partial class ArrivalDetailPage : ContentPage
{
    public ArrivalDetailPage(
        ArrivalDetailViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}