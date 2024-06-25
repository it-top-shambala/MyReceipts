using Microsoft.EntityFrameworkCore;
namespace Application;

public class DBContext :DbContext
{
    public DbSet<Recipe> Recipes { get; set; }

    public DBContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("D://recipes.db");
    }
}