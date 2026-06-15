using OpacMauiApp.ViewModels;

namespace OpacMauiApp.Pages;

public partial class JournalCataloguePage : ContentPage
{
    //public JournalCataloguePage(JournalCatalogueViewModel vm)
    //{
    //    InitializeComponent();

    //    BindingContext = vm;
    //}
    public JournalCataloguePage()
    {
        InitializeComponent();

        BindingContext =
            IPlatformApplication.Current.Services
            .GetService<JournalCatalogueViewModel>();
    }
}