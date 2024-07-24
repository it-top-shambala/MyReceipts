using MyRecipts.WebApiHelperLib.Models;
using NewApiTest.models;

namespace MyRecipts.WebApiHelperLib
{
    public static class RecipeConverter
    {
        public static List<Recipe> ConvertAllRecipesJsonToRecipes(Dictionary<RecipeJson, Instruction> recipes)
        {
            var res = new List<Recipe>();

            foreach (var recipe in recipes)
            {
                var newRecipe = ConvertRecipeJsonToRecipe(recipe.Key, recipe.Value);
                res.Add(newRecipe);
            }

            return res;
        }

        public static Recipe ConvertRecipeJsonToRecipe(RecipeJson recipe, Instruction instruction)
        {
            var steps = instruction.Steps.Select(i => new RecipeStep()
            {
                Description = i.StepDiscription,
                Number = i.Number,
                Ingredients = i.Ingredients
            }).ToList();

            var ingredients = recipe.UsedIngredients.Select(ing => new RecipeIngredient()
            {
                Name = ing.Name,
                Amount = ing.Amount,
                Unit = ing.Unit,
            }).ToList();

            var moreIng = recipe.MissedIngredients.Select(ing => new RecipeIngredient()
            {
                Name = ing.Name,
                Amount = ing.Amount,
                Unit = ing.Unit,
            }).ToList();

            ingredients.AddRange(moreIng);

            var res = new Recipe()
            {
                Name = recipe.Title,
                Steps = steps,
                Ingredients = ingredients
            };
            return res;
        }
    }
}