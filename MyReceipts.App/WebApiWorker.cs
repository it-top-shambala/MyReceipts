
using Application;
using FoodApi;
using Logger.File;
using MyRecipts.WebApiHelperLib.Models;

namespace MyReceipts.App
{
    internal class WebApiWorker
    {
        private RecipeHelper _recipeHelper = new RecipeHelper();
        private LogToFile _logger = new LogToFile();
        public List<RecipeUI> GetRecipesForUi(IEnumerable<string> ingredients)
        {
            RecipeUI recipeTemp = new RecipeUI();
            Ingredient ingredientTemp = new Ingredient();
            IEnumerable<Recipe> recipesFromWebApi = new List<Recipe>();
            List<RecipeUI> recipesForUi = new List<RecipeUI>();
            try
            {
                recipesFromWebApi = _recipeHelper.GetRecipes(ingredients);
                if (recipesFromWebApi.Any() && recipesFromWebApi != null)
                {
                    foreach (var recipe in recipesFromWebApi)
                    {
                        recipeTemp = Recipe1ConvertRecipe(recipe);
                        foreach (var ingredient in recipe.MissedIngredients)
                        {
                            recipeTemp.Ingredients.Add(FusionIngredientsToMissed(ingredient));
                        }
                        foreach (var ingredient in recipe.UsedIngredients)
                        {
                            recipeTemp.Ingredients.Add(FusionIngredientsToUsed(ingredient));
                        }
                        recipesForUi.Add(recipeTemp);
                    }
                }
                return recipesForUi;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return null;
            }
        }

        private RecipeUI Recipe1ConvertRecipe(Recipe recipe1)
        {
            if (recipe1 == null)
                throw new ArgumentNullException(nameof(recipe1));
            var recipe = new RecipeUI();
                recipe.Name = recipe1.Title;
            //            recipe.CalorieContent = 1;                                             //?unclear     
                return recipe;
        }

        private Ingredient FusionIngredientsToMissed(MissedIngredient ingredientFromWebApi)
        {
            if (ingredientFromWebApi == null)
                throw new ArgumentNullException(nameof(ingredientFromWebApi));
            var ingredient = new Ingredient();
            ingredient.Name = ingredientFromWebApi.OriginalName;
            ingredient.Quantity = Convert.ToInt32(ingredientFromWebApi.Amount);
            ingredient.Unit = ingredientFromWebApi.Unit;   
            return ingredient;
        }

        private Ingredient FusionIngredientsToUsed(UsedIngredient ingredientFromWebApi)
        {
            if (ingredientFromWebApi == null)
                throw new ArgumentNullException(nameof(ingredientFromWebApi));
            var ingredient = new Ingredient();
            ingredient.Name = ingredientFromWebApi.OriginalName;
            ingredient.Quantity = Convert.ToInt32(ingredientFromWebApi.Amount);
            ingredient.Unit = ingredientFromWebApi.Unit;
            return ingredient;
        }



    }
}
