using Newtonsoft.Json;

using System.Net.Http.Headers;
using System.Text;

namespace OpacMauiApp.Services;

public static class msPostCall<S, T>
{
    public static async Task<
        ReturnData<T>>
        CallApi(
        HttpClient http,
        S Request,
        string ControllerAction,
        string token = "")
    {
        var sendd =
            JsonConvert.SerializeObject(
                Request);

        StringContent strCont =
            new StringContent(
                sendd,
                Encoding.UTF8,
                "application/json");

        try
        {
            var req =
                new HttpRequestMessage(
                    HttpMethod.Post,
                    ControllerAction)
                {
                    Content = strCont
                };

            // TOKEN

            if (!string.IsNullOrEmpty(token))
            {
                req.Headers.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        token);
            }

            var resp =
                await http.SendAsync(req);

            var ret =
                new ReturnData<T>();

            if (resp.StatusCode !=
                System.Net.HttpStatusCode.OK)
            {
                var stcode =
                    (int)resp.StatusCode;

                string responseText =
                    await resp.Content
                    .ReadAsStringAsync();

                ret.IsSuccess = false;

                ret.Mesg =
                    stcode +
                    " : " +
                    responseText;

                return ret;
            }

            var dret =
                await resp.Content
                .ReadAsStringAsync();

            var rd =
                JsonConvert.DeserializeObject
                <
                    ReturnData<T>
                >(dret);

            return rd;
        }
        catch (Exception ex)
        {
            return new ReturnData<T>
            {
                IsSuccess = false,
                Mesg = ex.Message
            };
        }
    }
}