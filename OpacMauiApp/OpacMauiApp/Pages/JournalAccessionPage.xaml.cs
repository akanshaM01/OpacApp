using OpacMauiApp.ViewModels;

namespace OpacMauiApp.Pages;

public partial class JournalAccessionPage : ContentPage
{
    public JournalAccessionPage(
        JournalAccessionViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}