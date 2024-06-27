using Microsoft.EntityFrameworkCore;
namespace Application;

public class DBContext :DbContext
    {
        public DBContext()
        {
        }
    
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingridient> Ingredients { get; set; }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=recipes.db");
        }
    }