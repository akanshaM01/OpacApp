using OpacMauiApp.ViewModels;

namespace OpacMauiApp.Pages;

public partial class Memberpage : ContentPage
{
	public Memberpage(MemberViewModel vm)
    {
		InitializeComponent();
        BindingContext = vm;
    }
}

