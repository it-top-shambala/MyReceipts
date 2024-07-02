
using Microsoft.EntityFrameworkCore;

namespace Application
{
    internal static class DataBaseWorker
    {
        private static bool Equel(List<Ingredient> ingredientsSource, List<Ingredient> ingredientsComparable)
        {
            return ingredientsSource.OrderBy(e => e.Id).All(i => ingredientsComparable.OrderBy(e => e.Id).Any(j => j.Name == i.Name));
        }

        public static ICollection<Recipe> GetRecipes()
        {
            using (DBContext dbContext = new DBContext())
            {
                var CollectionOfRecipe = dbContext.Recipes.Include(i => i.Ingredients).ToList();
                return CollectionOfRecipe;
            }
        }
        public static void SaveRecipeDatabase(Recipe recipes)
        {
            using (DBContext dbContext = new DBContext())
            {
                dbContext.Recipes.Add(recipes);
                dbContext.SaveChanges();
            }
        }
        public static void SaveFavoriteRecipesToDataBase(List<Recipe> recipes)
        {
            using (DBContext dbContext = new DBContext())
            {
                dbContext.Recipes.AddRange(recipes);
                dbContext.SaveChanges();
            }
        }
        public static ICollection<Recipe> GetRecipesAccordingIngredients(string[] ingredients)
        {
            var temp = new List<Recipe>();
            var LenghtKey = ingredients.Length;

            using (DBContext dbContext = new DBContext())
            {
                var recipes = dbContext.Recipes.Include(i => i.Ingredients).ToList();
                foreach (var item in recipes)
                {
                    var ingredientsForRecipe = item.Ingredients.ToList();
                    var count = 0;
                    for (global::System.Int32 i = 0; i < LenghtKey; i++)
                    {
                        foreach (var item1 in ingredientsForRecipe)
                        {
                            if (item1.Name == ingredients[i])
                            {
                                count++;
                                break;
                            }
                        }
                    }
                    if (count == LenghtKey && LenghtKey == ingredientsForRecipe.Count)
                    {
                        temp.Add(item);
                    }
                }
            }
            return temp;
        }
        public static IEnumerable<Recipe> GetRecipesAccordingIngredients(List<Ingredient> ingredients)
        {

            using (DBContext dbContext = new DBContext())
            {
                var recipes = dbContext.Recipes.Include(i => i.Ingredients).ToList();
                var recipes1 = recipes.Where(i => Equel(i.Ingredients, ingredients)).ToList();
                return recipes1;
            }
        }
    }
}
