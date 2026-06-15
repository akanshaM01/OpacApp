using OpacMauiApp.ViewModels;

namespace OpacMauiApp.Pages;

public partial class IndentPage : ContentPage
{
    public IndentPage(IndentViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}