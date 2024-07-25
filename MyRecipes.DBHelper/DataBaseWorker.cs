
using Logger.File;
using Microsoft.EntityFrameworkCore;
using MyRecipts.WebApiHelperLib.Models;

namespace MyReceipts.App
{
    public class DataBaseWorker
    {
        private LogToFile _logger = new LogToFile();

        public void SaveFavoriteRecipes(List<RecipeJson> recipes)
        {
            RecipeUI recipeTemp = new RecipeUI();
            IngredientUI ingredientTemp = new IngredientUI();
            List<RecipeUI> recipesForDb = new List<RecipeUI>();
            try
            {
                if (recipes.Any() && recipes != null)
                {
                    foreach (var recipe in recipes)
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
                        recipesForDb.Add(recipeTemp);
                    }
                }
                SaveToDb(recipesForDb);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        private RecipeUI Recipe1ConvertRecipe(RecipeJson recipe1)
        {
            if (recipe1 == null)
                throw new ArgumentNullException(nameof(recipe1));
            var recipe = new RecipeUI();
            recipe.Name = recipe1.Title;
            //            recipe.CalorieContent = 1;                                             //?unclear
            recipe.Ingredients = new List<IngredientUI>();
            return recipe;
        }

        private IngredientUI FusionIngredientsToMissed(MissedIngredient ingredientFromWebApi)
        {
            if (ingredientFromWebApi == null)
                throw new ArgumentNullException(nameof(ingredientFromWebApi));
            var ingredient = new IngredientUI();
            ingredient.Name = ingredientFromWebApi.OriginalName;
            ingredient.Quantity = Convert.ToInt32(ingredientFromWebApi.Amount);
            ingredient.Unit = ingredientFromWebApi.Unit;
            return ingredient;
        }

        private IngredientUI FusionIngredientsToUsed(UsedIngredient ingredientFromWebApi)
        {
            if (ingredientFromWebApi == null)
                throw new ArgumentNullException(nameof(ingredientFromWebApi));
            var ingredient = new IngredientUI();
            ingredient.Name = ingredientFromWebApi.OriginalName;
            ingredient.Quantity = Convert.ToInt32(ingredientFromWebApi.Amount);
            ingredient.Unit = ingredientFromWebApi.Unit;
            return ingredient;
        }
        public  IEnumerable <RecipeUI> GetRecipes()
        {
            using (DBContext dbContext = new DBContext())
            {
                var recipes = new List<RecipeUI>();
                try
                {
                    recipes = dbContext.Recipes.Include(i => i.Ingredients).ToList();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.ToString());
                }
                return recipes;
            }
        }
        private  void SaveToDb(List <RecipeUI> recipes)
        {
            if (recipes.Any() && recipes != null) {  
                using (DBContext dbContext = new DBContext())
                {
                    dbContext.Recipes.AddRange(recipes);
                    dbContext.SaveChanges();
                } 
            } else
            {
                new ArgumentNullException("Список рецептов для сохранение в бд пуст");
            }
        }
    }
}
