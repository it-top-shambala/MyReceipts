using Logger.File;
using MyRecipts.WebApiHelperLib.Exceptions;
using MyRecipts.WebApiHelperLib.Models;
using System.Text.Json;

namespace FoodApi;

public class HttpHelper
{
    private HttpClient _httpClient;
    private const string PATH_CFG = "Configs\\ConfigHttpHelper.json";
    private ConfigHttpHelper _config;
    private LogToFile _logger;

    public HttpHelper(string baseUri)
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri(baseUri)
        };
        InitConfig();
        _logger = new LogToFile(_config.PathToLogger);
    }

    public string GetResponseBody(string requestUri)
    {
        var response = GetResponse(requestUri);
        if (response is null)
            return String.Empty;
        try
        {
            var res = response.Content.ReadAsStringAsync().Result;
            return res;
        }
        catch (Exception ex)
        {
            _logger.Error($"ошибка при чтение ответа http запроса: {ex.Message}");
            return String.Empty;
        }
    }

    private HttpResponseMessage? GetResponse(string requiestUri)
    {
        try
        {
            var response = _httpClient.GetAsync(requiestUri).Result;
            response.EnsureSuccessStatusCode();
            return response;
        }
        catch (Exception ex)
        {
            _logger.Error($"ошибка при получение ответа на http запрос: {ex.Message}");
            return null;
        }
    }

    private void InitConfig()
    {
        try
        {
            var jsonString = File.ReadAllText(PATH_CFG);
            _config = JsonSerializer.Deserialize<ConfigHttpHelper>(jsonString)!;
        }
        catch (Exception ex)
        {
            _logger.Error($"Ошибка при получение данных из конфига: {ex.Message}");
            throw new InitConfigException($"Ошибка при получение данных из конфига: {ex.Message}");
        }
    }
}