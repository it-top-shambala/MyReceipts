using MyRecipts.WebApiHelperLib.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace FoodApi;

public class RecipeHelper
{
    private HttpHelper _httpHelper;
    private const string BASE_URI = "https://api.spoonacular.com";
    private const string PATH_CFG = "Config.json";
    private string _apiKey;
    public RecipeHelper()
    {
        _httpHelper = new(BASE_URI);
        GetApiKeyFromJson();
    }

    public List<Recipe>? GetRecipes(IEnumerable<string> ingredients)
    {
        var res = new List<Recipe>();
        try
        {
            var jsonString = GetResponseJsonFromHttpCLient(ingredients);
            res = JsonSerializer.Deserialize<List<Recipe>>(jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            res = null;
        }
        return res;
    }
    private string GetResponseJsonFromHttpCLient(IEnumerable<string> ingredients)
    {
        var requestUri = CreateRequiestUri(ingredients);
        var jsonString = _httpHelper.GetResponseBody(requestUri);

        return jsonString;
    }

    private string CreateRequiestUri(IEnumerable<string> ingredients)
    {
        const string RAW_REQUEST_URI = "/recipes/findByIngredients?apiKey=";
        const string INGREDIENTS_STR_FOR_URI = "&ingredients=";
        const string CONNECTOR = ",+";

        var res = RAW_REQUEST_URI + _apiKey + INGREDIENTS_STR_FOR_URI;
        var ingredietsForm = String.Join(CONNECTOR, ingredients);

        return res + ingredietsForm;
    }
    private void GetApiKeyFromJson()
    {
        const string JSON_KEY_FIELD_NAME = "apiKey";
        try
        {
            var jsonString = File.ReadAllText(PATH_CFG);
            var jsonNode = JsonNode.Parse(jsonString)!;
            _apiKey = jsonNode[JSON_KEY_FIELD_NAME]!.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}