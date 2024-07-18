
using Logger.File;
using Microsoft.EntityFrameworkCore;
using MyRecipts.WebApiHelperLib.Models;

namespace MyReceipts.App
{
    /// <summary>
    /// Класс который работает непосредственно  с БД и выполняет преобразование классов 
    /// </summary>
    internal  class DataBaseWorker
    {
        private LogToFile _logger = new LogToFile();

        /// <summary>
        /// Приводит в действие алгоритм по вызову методов класса для преобразования
        /// и сохранение понравившихся рецептов
        /// </summary>
        /// <param name="recipes">
        /// Перечислитель рецептов которые получены с интернета
        /// </param>

        public void SaveFavoriteRecipes(List<Recipe> recipes)
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

        /// <summary>
        /// Преобразование классов Recipe в RecipeUI и инициализация списка ингредиентов
        /// </summary>
        /// <param name="recipe1">
        /// Исходный класс для метода
        /// </param>
        /// <returns>
        /// Возвращает итог преобразования. Класс для соохранения в БД
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Генерация исключения в результате пустого исходного класса(параметра метода)
        /// </exception>

        private RecipeUI Recipe1ConvertRecipe(Recipe recipe1)
        {
            if (recipe1 == null)
                throw new ArgumentNullException(nameof(recipe1));
            var recipe = new RecipeUI();
            recipe.Name = recipe1.Title;
            //            recipe.CalorieContent = 1;                                             //?unclear
            recipe.Ingredients = new List<IngredientUI>();
            return recipe;
        }

        /// <summary>
        /// Преобразование пропущенного ингредиента-webApi в класс пропущенного ингредиента-БД
        /// </summary>
        /// <param name="ingredientFromWebApi">
        /// пропущенный ингредиент  от webApi 
        /// </param>
        /// <returns>
        /// Итог преобразования пропущенного ингредиента для БД
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Генерация исключения в результате пустого исходного класса(параметра метода)
        /// </exception>

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

        /// <summary>
        /// Преобразование используемого ингредиента-webApi в класс используемого ингредиента-БД
        /// </summary>
        /// <param name="ingredientFromWebApi">
        /// Используемый ингредиент от webApi
        /// </param>
        /// <returns>
        /// Итог преобразования используемого ингредиента для БД
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Генерация исключения в результате пустого исходного класса(параметра метода)
        /// </exception>
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

        /// <summary>
        /// Получение всех рецептов которые хранятся в БД с загрузкой связных данных
        /// </summary>
        /// <returns>
        /// Последовательность классов из БД реализующая интерфейс  
        /// </returns>
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

        /// <summary>
        /// Логика сохранения рецептов в БД с обновлением
        /// </summary>
        /// <param name="recipes">
        /// Лист классов RecipeUI которые нужно сохранить в БД 
        /// </param>
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
