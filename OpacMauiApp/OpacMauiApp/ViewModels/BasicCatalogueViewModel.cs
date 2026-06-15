using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpacMauiApp.Models;
using OpacMauiApp.Services;
using System.Collections.ObjectModel;

namespace OpacMauiApp.ViewModels;

public partial class BasicCatalogueViewModel: ObservableObject
{
    private readonly ApiService _api;

    public ObservableCollection<BasicSearchOpacMod> Books { get; set; } = new();

    [ObservableProperty]
    string title;

    [ObservableProperty]
    string author;

    [ObservableProperty]
    string publisher;

    [ObservableProperty]
    string subject;

    [ObservableProperty]
    string keywords;

    [ObservableProperty]
    bool isLoading;

    public BasicCatalogueViewModel(ApiService api)
    {
        _api =IPlatformApplication.Current.Services.GetService<ApiService>();
    }

    [RelayCommand]
    async Task Search()
    {
        try
        {
            IsLoading = true;

            var req =
                new BasicSearchReqAdo
                {
                    Title = Title,
                    Author = Author,
                    Publisher = Publisher,
                    Subject = Subject,
                    Keywords = Keywords,
                    OnTitle =!string.IsNullOrWhiteSpace(Title),

                    OnAuthor =!string.IsNullOrWhiteSpace(Author),

                    OnPublisher =!string.IsNullOrWhiteSpace(Publisher),

                    OnSubject =!string.IsNullOrWhiteSpace(Subject),

                    OnKeyword = !string.IsNullOrWhiteSpace(Keywords),

                    DatabasesLibs =
                                [
                                    new DbsLibs
                                    {
                                        DatabaseId = ApiService.SelectedDatabaseId,

                                        libIds =
                                        [
                                            ApiService.SelectedLibId
                                        ]
                                    }
                                ]
                };

            var resp =
                await _api.BasicSearchAdo(req);

            Books.Clear();

            if (resp.IsSuccess)
            {
                foreach (var item in resp.Data)
                {
                    Books.Add(item);
                }
            }
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    void Reset()
    {
        Title = "";
        Author = "";
        Publisher = "";
        Subject = "";
        Keywords = "";

        Books.Clear();
    }
}

    //[RelayCommand]
    // async Task AISearch()
    //{
    //    try
    //    {
    //        IsLoading = true;

    //        var req = new TitleAuthAICmd
    //        {
    //            Title = AITitle,
    //            TitleAuthor = TitleAuthor
    //        };

    //        var resp =
    //            await _api.SearchTitleAuthAI(req);

    //        Books.Clear();

    //        if (resp.IsSuccess)
    //        {
    //            foreach (var item in resp.Data)
    //            {
    //                Books.Add(item);
    //            }
    //        }
    //    }
    //    finally
    //    { IsLoading = false; }
    //        IsLoading = false;
    //    }
    //}

  