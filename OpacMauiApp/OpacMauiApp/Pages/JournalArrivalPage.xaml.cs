using OpacMauiApp.ViewModels;

namespace OpacMauiApp.Pages;

public partial class JournalArrivalPage : ContentPage
{
    public JournalArrivalPage(JournalArrivalViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}