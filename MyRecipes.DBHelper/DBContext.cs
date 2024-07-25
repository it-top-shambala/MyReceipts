using Microsoft.EntityFrameworkCore;

namespace MyReceipts.App;

public class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DbSet<RecipeUI> Recipes { get; set; }
    public DbSet<IngredientUI> Ingredients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=recipes.db");
    }
}