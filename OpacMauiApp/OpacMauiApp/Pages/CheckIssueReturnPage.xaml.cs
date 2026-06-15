using OpacMauiApp.ViewModels;

namespace OpacMauiApp.Pages;

public partial class CheckIssueReturnPage : ContentPage
{
    public CheckIssueReturnPage(
        CheckIssueReturnViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}