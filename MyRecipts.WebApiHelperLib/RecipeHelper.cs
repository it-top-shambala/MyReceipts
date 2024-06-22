using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MyRecipts.WebApiHelperLib.Models;

namespace FoodApi;

public class RecipeHelper
{
    private HttpHelper _httpHelper;
    private const string _BASEURI = "https://api.spoonacular.com";
    private const string _BASEREQUIEST = "/recipes/findByIngredients?apiKey=78af57c24ce342e7bf49af1a2d07624d&ingredients=";

    public RecipeHelper()
    {
        _httpHelper = new(_BASEURI);
    }

    public List<Recipe>? GetRecipes(IEnumerable<string> ingredients)
    {
        var requestUri = FormRequiestUri(ingredients);
        var jsonString = _httpHelper.GetResponseBody(requestUri);
        return JsonSerializer.Deserialize<List<Recipe>>(jsonString);
    }

    private string FormRequiestUri(IEnumerable<string> ingredients)
    {
        var ingredietsForm = String.Join(",+", ingredients);
        return _BASEREQUIEST + ingredietsForm;
    }
}