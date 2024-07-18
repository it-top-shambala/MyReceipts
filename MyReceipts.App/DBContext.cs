using Microsoft.EntityFrameworkCore;

namespace MyReceipts.App;

/// <summary>
/// Класс для подключения к базе данных
/// </summary>
public class DBContext :DbContext
{
    public DBContext()
    {
    }
    
    public DbSet<RecipeUI> Recipes { get; set; }
    public DbSet<IngredientUI> Ingredients { get; set; }
    
    /// <summary>
    /// Переопределение стандартного метода с установкой места расположения БД
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=D:\\Study\\MyReceipts\\recipes.db");
    }
}