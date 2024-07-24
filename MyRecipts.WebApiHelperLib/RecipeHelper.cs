using Logger.File;
using MyRecipts.WebApiHelperLib;
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

    public List<Recipe>? GetRecipes(IEnumerable<string> ingredients)
    {
        var rawRecipes = GetRecipeWithInstruction(ingredients);
        if (rawRecipes is null)
        {
            _logger.Warning($"{typeof(RecipeJson)} не были инециализированы");
            return null;
        }

        var recipes = RecipeConverter.ConvertAllRecipesJsonToRecipes(rawRecipes);
        return recipes;
    }

    private Dictionary<RecipeJson, Instruction?>? GetRecipeWithInstruction(IEnumerable<string> ingredients)
    {
        var res = new Dictionary<RecipeJson, Instruction?>();

        var recipes = GetRecipesJson(ingredients);
        if (recipes is null)
            return null;

        foreach (var item in recipes)
        {
            var value = GetInstruction(item);
            res.Add(item, value);
        }
        return res;
    }

    public Instruction? GetInstruction(RecipeJson recipe)
    {
        var jsonStrings = GetInstructionForRecipe(recipe.Id);
        var res = new Instruction();

        try
        {
            if (String.IsNullOrWhiteSpace(jsonStrings))
                return null;

            var instruction = JsonSerializer.Deserialize<List<Instruction>>(jsonStrings);

            res = instruction?[0];
        }
        catch (Exception ex)
        {
            _logger.Error($"Ошибка при десиреализации {typeof(Instruction)}: {ex.Message}");
            res = null;
        }

        return res;
    }

    public List<RecipeJson>? GetRecipesJson(IEnumerable<string> ingredients)
    {
        try
        {
            var jsonString = GetResponseJsonFromHttpCLient(ingredients);
            var res = JsonSerializer.Deserialize<List<RecipeJson>>(jsonString);
            return res;
        }
        catch (Exception ex)
        {
            _logger.Error($"Ошибка при десиреализации {typeof(RecipeJson)}: {ex.Message}");
            return null;
        }
    }

    private string GetInstructionForRecipe(int id)
    {
        return _httpHelper.GetResponseBody($"/recipes/{id}/analyzedInstructions{_config.ApiKeyConnector}{_config.ApiKey}");
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