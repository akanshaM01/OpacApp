using OpacMauiApp.ViewModels;

namespace OpacMauiApp.Pages;

public partial class Overduedetailpage : ContentPage
{
	public Overduedetailpage(MemberOverdueDetailViewModel vm)
    {
		InitializeComponent();
        BindingContext = vm;
    }
}
