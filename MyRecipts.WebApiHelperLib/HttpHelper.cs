using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApi;

public class HttpHelper
{
    private HttpClient _httpClient;

    public HttpHelper(string baseUri)
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri(baseUri)
        };
    }

    public string GetResponseBody(string requiestUri)
    {
        var response = GetResponse(requiestUri);
        var res = response.Content.ReadAsStringAsync().Result;
        return res;
    }

    private HttpResponseMessage? GetResponse(string requiestUri)
    {
        HttpResponseMessage response = _httpClient.GetAsync(requiestUri).Result;
        response.EnsureSuccessStatusCode();
        return response;
    }
}