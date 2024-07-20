using Logger.File;
using MyRecipts.WebApiHelperLib.Exceptions;
using MyRecipts.WebApiHelperLib.Models;
using System.Text.Json;

namespace FoodApi;

/// <summary>
/// Представляет класс для работы с вебапи
/// </summary>
public class RecipeHelper
{
    private HttpHelper _httpHelper;
    private LogToFile _logger;
    private const string PATH_CFG = "Configs\\ConfigRecipeHelper.json";
    private ConfigRecipeHepler _config;

    public RecipeHelper()
    {
        InitConfig();
        _httpHelper = new HttpHelper(_config.BaseUri);
        _logger = new LogToFile(_config.PathToLogger);
    }

    /// <summary>
    /// Десериализация ответа от вебапи
    /// </summary>
    /// <param name="ingredients"></param>
    /// <returns>список рецептов</returns>
    public List<Recipe>? GetRecipes(IEnumerable<string> ingredients)
    {
        try
        {
            var jsonString = GetResponseJsonFromHttpCLient(ingredients);
            var res = JsonSerializer.Deserialize<List<Recipe>>(jsonString);
            return res;
        }
        catch (Exception ex)
        {
            _logger.Error($"Ошибка при десиреализации {typeof(Recipe)}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Получение ответа от вебапи в виде jsonstring
    /// </summary>
    /// <param name="ingredients"></param>
    /// <returns>ответ от вебапи</returns>
    /// <exception cref="ArgumentNullException"></exception>
    private string GetResponseJsonFromHttpCLient(IEnumerable<string> ingredients)
    {
        var requestUri = CreateRequiestUri(ingredients);
        var jsonString = _httpHelper.GetResponseBody(requestUri);
        if (String.IsNullOrEmpty(jsonString))
            throw new ArgumentNullException(nameof(jsonString));

        return jsonString;
    }

    /// <summary>
    /// Формирование строки запроса для вебапи
    /// </summary>
    /// <param name="ingredients"></param>
    /// <returns>сформированную строку</returns>
    private string CreateRequiestUri(IEnumerable<string> ingredients)
    {
        var ingredietsForm = String.Join(_config.Connector, ingredients);

        return $"{_config.RawRequestUri}{_config.ApiKeyConnector}{_config.ApiKey}{_config.IngredientsConnector}{ingredietsForm}";
    }

    /// <summary>
    /// Метод для получения данных из конфига и их десериализации
    /// </summary>
    /// <exception cref="InitConfigException"></exception>
    private void InitConfig()
    {
        try
        {
            var jsonString = File.ReadAllText(PATH_CFG);
            _config = JsonSerializer.Deserialize<ConfigRecipeHepler>(jsonString)!;
        }
        catch (Exception ex)
        {
            _logger.Error($"Ошибка при получение данных из конфига: {ex.Message}");
            throw new InitConfigException($"Ошибка при получение данных из конфига: {ex.Message}");
        }
    }
}