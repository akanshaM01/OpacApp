using OpacMauiApp.Services;
using OpacMauiApp.ViewModels;

namespace OpacMauiApp.Pages;

public partial class BasicCataloguePage : ContentPage
{
    public BasicCataloguePage()
    {
        InitializeComponent();

        BindingContext =
            new BasicCatalogueViewModel(
                new ApiService());
    }
}