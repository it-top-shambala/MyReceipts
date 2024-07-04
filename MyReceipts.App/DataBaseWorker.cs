
using Logger.File;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    internal  class DataBaseWorker
    {
        private LogToFile _logger = new LogToFile();
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
        public  void SaveFavoriteRecipes(List <RecipeUI> recipes)
        {
            if (recipes.Any() && recipes != null) {  
                using (DBContext dbContext = new DBContext())
                {
                    try
                    {
                        dbContext.Recipes.AddRange(recipes);
                        dbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.ToString());
                    }
                } 
            }
        }
    }
}
