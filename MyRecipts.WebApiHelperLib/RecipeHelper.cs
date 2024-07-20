using Logger.File;
using MyRecipts.WebApiHelperLib.Exceptions;
using MyRecipts.WebApiHelperLib.Models;
using NewApiTest.models;
using System.Text.Json;

namespace FoodApi;

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

    public List<Instruction?> GetInstructions(List<Recipe> recipes)
    {
        var jsonStrings = GetInstructionForEachRecipe(recipes);
        var res = new List<Instruction?>();

        foreach (var item in jsonStrings)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(item))
                    continue;

                var instruction = JsonSerializer.Deserialize<List<Instruction>>(item);

                res.Add(instruction?[0]);
            }
            catch (Exception ex)
            {
                _logger.Error($"Ошибка при десиреализации {typeof(Instruction)}: {ex.Message}");
                res.Add(null);
            }
        }
        return res;
    }

    public List<string> GetInstructionForEachRecipe(List<Recipe> recipes)
    {
        var res = new List<string>();

        foreach (var item in recipes)
        {
            res.Add(GetInstructionForRecipe(item.Id));
        }

        return res;
    }

    public string GetInstructionForRecipe(int id)
    {
        return _httpHelper.GetResponseBody($"/recipes/{id}/analyzedInstructions{_config.ApiKeyConnector}{_config.ApiKey}");
    }

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

    private string GetResponseJsonFromHttpCLient(IEnumerable<string> ingredients)
    {
        var requestUri = CreateRequiestUriForRecipe(ingredients);
        var jsonString = _httpHelper.GetResponseBody(requestUri);
        if (String.IsNullOrEmpty(jsonString))
            throw new ArgumentNullException(nameof(jsonString));

        return jsonString;
    }

    private string CreateRequiestUriForRecipe(IEnumerable<string> ingredients)
    {
        var ingredietsForm = String.Join(_config.Connector, ingredients);

        return $"{_config.RawRequestUri}{_config.ApiKeyConnector}{_config.ApiKey}{_config.IngredientsConnector}{ingredietsForm}";
    }

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