using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using OpacMauiApp.Pages;
using OpacMauiApp.Services;
using OpacMauiApp.ViewModels;

namespace OpacMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont(
                        "OpenSans-Regular.ttf",
                        "OpenSansRegular");

                    fonts.AddFont(
                        "OpenSans-Semibold.ttf",
                        "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<ApiService>();
             builder.Services.AddTransient<JournalCatalogueViewModel>();
            builder.Services.AddTransient< JournalCataloguePage>();
            builder.Services.AddTransient<JournalAccessionPage>();
            builder.Services.AddTransient<JournalAccessionViewModel>();
            builder.Services.AddTransient<JournalArrivalViewModel>();
            builder.Services.AddTransient<JournalIssueViewModel>();
            builder.Services.AddTransient<MemberOverdueDetailViewModel>();
            builder.Services.AddTransient<MemberViewModel>();
            builder.Services.AddTransient<JournalArrivalPage>();
            builder.Services.AddTransient<JournalIssuePage>();
            builder.Services.AddTransient<Memberpage>();
            builder.Services.AddTransient<Overduedetailpage>();
            builder.Services.AddTransient<ArrivalDetailViewModel>();
            builder.Services.AddTransient<ArrivalDetailPage>();
            builder.Services.AddTransient<DigitalContSearchPage>();
            builder.Services.AddTransient<DigitalContSearchViewModel>();
            builder.Services.AddTransient<IndentViewModel>();
            builder.Services.AddTransient<IndentPage>();
            builder.Services.AddTransient<CheckIssueReturnPage>();
            builder.Services.AddTransient<CheckIssueReturnViewModel>();
            builder.UseMauiApp<App>().UseMauiCommunityToolkit();

            // HTTP CLIENT

            builder.Services.AddSingleton(sp =>
            {
                var handler = new HttpClientHandler();

                handler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler
                    .DangerousAcceptAnyServerCertificateValidator;

                return new HttpClient(handler)
                {
                    BaseAddress =
                        new Uri("https://libapi.mssplonline.com/api/")
                    //("https://libapi.mssplonline.com/api/")
                    // ("https://localhost:7296/api/")
                };
            });

            return builder.Build();
        }
    }
}