using OpacMauiApp.ViewModels;

namespace OpacMauiApp.Pages;

public partial class JournalIssuePage : ContentPage
{
    public JournalIssuePage(JournalIssueViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}