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

    public string GetResponseBody(string requestUri)
    {
        var response = GetResponse(requestUri);
        if (response is null)
            return String.Empty;

        var res = response.Content.ReadAsStringAsync().Result;
        return res;
    }

    private HttpResponseMessage? GetResponse(string requiestUri)
    {
        var response = new HttpResponseMessage();
        try
        {
            response = _httpClient.GetAsync(requiestUri).Result;
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            response = null;
        }
        return response;
    }
}