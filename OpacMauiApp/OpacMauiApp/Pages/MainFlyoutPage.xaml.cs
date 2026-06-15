namespace OpacMauiApp.Pages;

public partial class MainFlyoutPage : FlyoutPage
{
    public MainFlyoutPage()
    {
        InitializeComponent();

        Detail = new NavigationPage(new DashboardPage());
        Title = "Library OPAC";

        var page = new DashboardPage();

        page.Title = "Dashboard";

        Detail = new NavigationPage(page)
        {
            Title = "Dashboard"
        };
    }

    private void Catalogue_Clicked(
        object sender,
        EventArgs e)
    {
        CatalogueMenu.IsVisible =
            !CatalogueMenu.IsVisible;
    }

    private void JournalManagement_Clicked(
        object sender,
        EventArgs e)
    {
        JournalMenu.IsVisible =
            !JournalMenu.IsVisible;
    }
    private void MemeberDetails_Clicked(object sender,EventArgs e)
    {
        MemberMenu.IsVisible =
            !MemberMenu.IsVisible;
    }
    private void Library_Clicked(object sender,EventArgs e)
    {
        LibraryMenu.IsVisible =
            !LibraryMenu.IsVisible;
    }
    private void Integrated_Clicked(object sender,EventArgs e)
    {
        IntegratedMenu.IsVisible =
            !IntegratedMenu.IsVisible;
    }


    private void BasicCatalogue_Clicked(object sender,EventArgs e)
    {
        Detail =
            new NavigationPage(
                new BasicCataloguePage());
    }
    private void JournalCatalogue_Clicked(object sender,EventArgs e)
    {
        Detail =
            new NavigationPage(
                new JournalCataloguePage());

        //IsPresented = false;
    }

    private void JournalAccession_Clicked(object sender, EventArgs e)
    {
        Detail =
            new NavigationPage(
                IPlatformApplication.Current.Services
                .GetService<JournalAccessionPage>());

        
    }
    private void JournalArrival_Clicked(object sender,EventArgs e)
    {
        Detail = new NavigationPage(IPlatformApplication.Current.Services.GetService<JournalArrivalPage>());
        //new JournalArrivalPage());

        
    }
    private void JournalIssue_Clicked(object sender,EventArgs e)
    {
        Detail = new NavigationPage(IPlatformApplication.Current.Services.GetService<JournalIssuePage>());
        //new JournalArrivalPage());

        
    }
    private void MemberSearch_Clicked(object sender, EventArgs e)
    {
        Detail = new NavigationPage(IPlatformApplication.Current.Services.GetService<Memberpage>());
        


    }
    private void Overdue_Clicked(object sender, EventArgs e)
    {
        Detail = new NavigationPage(IPlatformApplication.Current.Services.GetService<Overduedetailpage>());
       

    }
    private void NewArrival_Clicked(object sender, EventArgs e)
    {
        Detail = new NavigationPage(IPlatformApplication.Current.Services.GetService<ArrivalDetailPage>());
        


    }
    private void Digital_Clicked(object sender, EventArgs e)
    {
        Detail = new NavigationPage(IPlatformApplication.Current.Services.GetService<DigitalContSearchPage>());
       
    }
    private void IndentStatus_Clicked(object sender, EventArgs e)
    {
        Detail = new NavigationPage(IPlatformApplication.Current.Services.GetService<IndentPage>());
       
    }
    private void IssueReturn_Clicked(object sender, EventArgs e)
    {
        Detail = new NavigationPage(IPlatformApplication.Current.Services.GetService<CheckIssueReturnPage>());
       
    }
}