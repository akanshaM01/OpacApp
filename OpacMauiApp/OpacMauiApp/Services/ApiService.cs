using OpacMauiApp.Models;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace OpacMauiApp.Services;

public class ApiService
{
    private readonly HttpClient client;
    public static int SelectedLibId { get; set; }
    public static int SelectedDatabaseId { get; set; }
    //public static int SelectedDatabaseId;
    // GLOBAL TOKEN
    public static string Token = "";

    // GLOBAL SESSION
    public static string SessionId = "";

    public ApiService()
    {
        client = new HttpClient();

        client.BaseAddress =
            new Uri("https://libapi.mssplonline.com/api/");
            //("https://localhost:7296/api/");
            //("https://libapi.mssplonline.com/api/");
    }

    // ================= LOGIN =================

    public async Task<ReturnData<LoginResp>> Login(LoginCmd req)
    {
        var json =
            JsonSerializer.Serialize(req);

        var content =
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

        var response =
            await client.PostAsync(
                "Login/Login",
                content);

        var result =
            await response.Content
            .ReadAsStringAsync();

        return JsonSerializer.Deserialize<
           ReturnData<LoginResp>>(
               result,
               new JsonSerializerOptions
               {
                   PropertyNameCaseInsensitive = true
               });
    }
    // ================= SET LIBRARY =================
    public async Task<ReturnData<LoggedDetMod>>LoginSetLibrary(LoginSetLibDecrCmd req)
    {
        try
        {
            var json =
                JsonSerializer.Serialize(req);

            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            var response =
                await client.PostAsync(
                    "adm/LoginSetLibBl",
                    content);

            var result =
                await response.Content
                .ReadAsStringAsync();

            var data =
            JsonSerializer.Deserialize<
                ReturnData<LoggedDetMod>>(
                    result,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            // FINAL TOKEN SAVE

            if (data != null &&
                data.IsSuccess &&
                data.Data != null)
            {
                Token =
              data.Data
              .AuthTokenResponse
              .AccessToken;

                SessionId =
               data.Data
               .AuthTokenResponse
               .SessionId;

                //await SecureStorage.SetAsync(
                //    "token",
                //    Token);

                //await SecureStorage.SetAsync(
                //    "sessionid",
                //    SessionId);
            }

            return data;
        }
        catch (Exception ex)
        {
            return new ReturnData<LoggedDetMod>
            {
                IsSuccess = false,
                Mesg = ex.Message
            };
        }
    }

    // ================= BASIC SEARCH =================

    public async Task<ReturnData<List<BasicSearchOpacMod>>>BasicSearchAdo(BasicSearchReqAdo req)
    {
        try
        {
            // IMPORTANT
            req.DatabasesLibs =
            [
                new DbsLibs
            {
                DatabaseId =
                    ApiService.SelectedDatabaseId,

                libIds =
                [
                    ApiService.SelectedLibId
                ]
            }
            ];

            var json =JsonSerializer.Serialize(req);

            var content =new StringContent(json,Encoding.UTF8, "application/json");

            var request =new HttpRequestMessage(HttpMethod.Post,"OpacADONet/SearchBasic");

            request.Content = content;

            // BEARER TOKEN
            request.Headers.Authorization =new AuthenticationHeaderValue(  "Bearer", Token);

            // COOKIE TOKEN
            //request.Headers.Add("Cookie",$"ApiAuth={Token}"); 
            var response =await client.SendAsync(request);

            var result =await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<
                ReturnData<
                    List<BasicSearchOpacMod>>>(
                        result,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
        }
        catch (Exception ex)
        {
            return new ReturnData<List<BasicSearchOpacMod>>
            {
                IsSuccess = false,
                Mesg = ex.Message
            };
        }
    }
    public async Task<ReturnData<List<VJournalAccession>>> SearchJourAccession(
    AccnSearchAdv req)
    {
        try
        {
            var json =
                JsonSerializer.Serialize(req);

            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            var request =
                new HttpRequestMessage(
                    HttpMethod.Post,
                    "Opac/SearchJourAccession");

            request.Content = content;

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    Token);

            var response =
                await client.SendAsync(request);

            var result =
                await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<
                ReturnData<List<VJournalAccession>>>(
                    result,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
        }
        catch (Exception ex)
        {
            return new ReturnData<List<VJournalAccession>>
            {
                IsSuccess = false,
                Mesg = ex.Message
            };
        }
    }
    public async Task<ReturnData<List<OpacDbsMod>>> GetOpacDbs()
    {
        try
        {
            var request =
                new HttpRequestMessage(
                    HttpMethod.Get,
                    "OpacADONet/GetOpacDbs");

            // TOKEN
            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    Token);

            var response =
                await client.SendAsync(request);

            var result =
                await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<
                ReturnData<List<OpacDbsMod>>>(
                    result,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
        }
        catch (Exception ex)
        {
            return new ReturnData<List<OpacDbsMod>>
            {
                IsSuccess = false,
                Mesg = ex.Message
            };
        }
    }
    public async Task<ReturnData<List<JournalArrival>>> Getjournal_arrival_view()
    {
        try
        {
            var request =
                new HttpRequestMessage(
                    HttpMethod.Get,
                    "Basic/Getjournal_arrival_view");

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    Token);

            var response =
                await client.SendAsync(request);

            var result =
                await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<
                ReturnData<List<JournalArrival>>>(
                    result,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
        }
        catch (Exception ex)
        {
            return new ReturnData<List<JournalArrival>>
            {
                IsSuccess = false,
                Mesg = ex.Message
            };
        }
    }
    public async Task<ReturnData<List<JournalIssueMod>>>GetJournalIssue()
    {
        try
        {
            var request =
                new HttpRequestMessage(
                    HttpMethod.Get,
                    "Transaction/GetJournalIssue");

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    Token);

            var response =
                await client.SendAsync(request);

            var result =
                await response.Content
                .ReadAsStringAsync();

            return JsonSerializer.Deserialize<
                ReturnData<List<JournalIssueMod>>>(
                    result,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
        }
        catch (Exception ex)
        {
            return new ReturnData<List<JournalIssueMod>>
            {
                IsSuccess = false,
                Mesg = ex.Message
            };
        }
    }
    public async Task<ReturnData<List<CircUserMod>>> FindMember()
    {
        try
        {
            var request =
                new HttpRequestMessage(
                    HttpMethod.Get,
                    "Basic/GetCircUserManagement");

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    Token);

            var response =
                await client.SendAsync(request);

            var result =
                await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<
                ReturnData<List<CircUserMod>>>(
                    result,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
        }
        catch (Exception ex)
        {
            return new ReturnData<List<CircUserMod>>
            {
                IsSuccess = false,
                Mesg = ex.Message
            };
        }
    }
    public async Task<ReturnData<List<BookArrivalMod>>> GetBookArrival(
     ArrivalCmd requestData)
    {
        try
        {
            var request =
                new HttpRequestMessage(
                    HttpMethod.Post,
                    "Order/GetBookArrival");

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    Token);

            request.Content =
                JsonContent.Create(requestData);

            var response =
                await client.SendAsync(request);

            var result =
                await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<
                ReturnData<List<BookArrivalMod>>>(
                    result,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
        }
        catch (Exception ex)
        {
            return new ReturnData<List<BookArrivalMod>>
            {
                IsSuccess = false,
                Mesg = ex.Message
            };
        }
    }
    public async Task<ReturnData<List<DigitalContMod>>> SearchDigitalContent()
    {
        try
        {
            var request =
                new HttpRequestMessage(
                    HttpMethod.Get,
                    "Basic/GetDigitalContent");

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    Token);

            var response =
                await client.SendAsync(request);

            var result =
                await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<
                ReturnData<List<DigitalContMod>>>(
                    result,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
        }
        catch (Exception ex)
        {
            return new ReturnData<List<DigitalContMod>>
            {
                IsSuccess = false,
                Mesg = ex.Message
            };
        }
    }
    public async Task<ReturnData<List<IndentMod>>> CheckIndent()
    {
        try
        {
            var request =
                new HttpRequestMessage(
                    HttpMethod.Get,
                    "Indent/GetIndentMaster");

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    Token);

            var response =
                await client.SendAsync(request);

            var result =
                await response.Content.ReadAsStringAsync();

            var data =
                JsonSerializer.Deserialize<
                    ReturnData<List<IndentMod>>>(
                        result,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

            if (data == null)
            {
                return new ReturnData<List<IndentMod>>
                {
                    IsSuccess = false,
                    Mesg = "API returned null response."
                };
            }

            if (data.IsSuccess && data.Data == null)
            {
                return new ReturnData<List<IndentMod>>
                {
                    IsSuccess = false,
                    Mesg = "API returned success but Data is null."
                };
            }

            return data;
        }
        catch (Exception ex)
        {
            return new ReturnData<List<IndentMod>>
            {
                IsSuccess = false,
                Mesg = ex.InnerException != null
                    ? $"{ex.Message} | Inner: {ex.InnerException.Message}"
                    : ex.Message
            };
        }
    }
    public async Task<ReturnData<List<CircIssueTransactionMod>>> CheckIssueRetSearch()
    {
        try
        {
            var request =
                new HttpRequestMessage(
                    HttpMethod.Get,
                    "CircUser/GetCircIssueTransaction");

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    Token);

            var response =
                await client.SendAsync(request);

            var result =
                await response.Content.ReadAsStringAsync();

            var data =
                JsonSerializer.Deserialize<
                    ReturnData<List<CircIssueTransactionMod>>>(
                        result,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

            if (data == null)
            {
                return new ReturnData<List<CircIssueTransactionMod>>
                {
                    IsSuccess = false,
                    Mesg = "API returned null response."
                };
            }

            if (data.IsSuccess && data.Data == null)
            {
                return new ReturnData<List<CircIssueTransactionMod>>
                {
                    IsSuccess = false,
                    Mesg = "API returned success but Data is null."
                };
            }

            return data;
        }
        catch (Exception ex)
        {
            return new ReturnData<List<CircIssueTransactionMod>>
            {
                IsSuccess = false,
                Mesg = ex.InnerException != null
                    ? $"{ex.Message} | Inner: {ex.InnerException.Message}"
                    : ex.Message
            };
        }
    }
}
//public async Task<ReturnData<List<BasicSearchOpacMod>>>SearchTitleAuthAI( TitleAuthAICmd req)
//{
//    var json =
//        JsonSerializer.Serialize(req);

//    var content =
//        new StringContent(
//            json,
//            Encoding.UTF8,
//            "application/json");

//    var response =
//        await client.PostAsync(
//            "OpacADONet/SearchTitleAuthAI",
//            content);

//    var result =
//        await response.Content
//        .ReadAsStringAsync();

//    return JsonSerializer.Deserialize<
//        ReturnData<List<BasicSearchOpacMod>>>(
//            result,
//            new JsonSerializerOptions
//            {
//                PropertyNameCaseInsensitive = true
//            });
//}
//public async Task<ReturnData<List<BasicSearchOpacMod>>>BasicSearchAdo(BasicSearchReqAdo req)
//{
//    try
//    {
//        client.DefaultRequestHeaders.Clear();

//        client.DefaultRequestHeaders.Authorization =
//            new System.Net.Http.Headers
//            .AuthenticationHeaderValue(
//                "Bearer",
//                ApiService.Token);

//        var json =
//            JsonSerializer.Serialize(req);

//        var content =
//            new StringContent(
//                json,
//                Encoding.UTF8,
//                "application/json");

//        var response =
//            await client.PostAsync(
//                "OpacADONet/SearchBasic",
//                content);

//        var result =
//            await response.Content
//            .ReadAsStringAsync();

//        return JsonSerializer.Deserialize<
//            ReturnData<List<BasicSearchOpacMod>>>(
//                result,
//                new JsonSerializerOptions
//                {
//                    PropertyNameCaseInsensitive = true
//                });
//    }
//    catch (Exception ex)
//    {
//        return new ReturnData<
//            List<BasicSearchOpacMod>>
//        {
//            IsSuccess = false,
//            Mesg = ex.Message
//        };
//    }
//}
//public async Task<ReturnData<CommonDDlsMod>>GetDropDownsStatLang()
//{
//    var response =
//        await client.GetAsync(
//            "Basic/getDropDownsStatLang");

//    var result =
//        await response.Content
//        .ReadAsStringAsync();

//    return JsonSerializer.Deserialize<
//        ReturnData<CommonDDlsMod>>(
//            result,
//            new JsonSerializerOptions
//            {
//                PropertyNameCaseInsensitive = true
//            });
//}
//public async Task<ReturnData<List<KeywordListMod>>>GetKeywordsUser(string userid)
//{
//    var response =
//        await client.GetAsync(
//            $"Admin/GetKeywordsUser?UserId={userid}");

//    var result =
//        await response.Content
//        .ReadAsStringAsync();

//    return JsonSerializer.Deserialize<
//        ReturnData<List<KeywordListMod>>>(
//            result,
//            new JsonSerializerOptions
//            {
//                PropertyNameCaseInsensitive = true
//            });
//}
