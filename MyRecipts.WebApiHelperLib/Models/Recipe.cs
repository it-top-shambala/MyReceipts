using NewApiTest.models;

namespace MyRecipts.WebApiHelperLib.Models;

public class Recipe
{
    public string Name { get; set; }
    public List<RecipeStep>? Steps { get; set; }
    public List<RecipeIngredient> Ingredients { get; set; }
}