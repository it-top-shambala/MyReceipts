using System.ComponentModel.DataAnnotations.Schema;

namespace Application;

[Table("table_recipes")]
public class Recipe
{
    private const int ZERO = 0;
    [Column("id")]
    public int Id { get; init; }

    [Column("name")] 
    public string? Name { get; set; }

    [Column("calories")] 
    public int CalorieContent { get; set; }

    [Column("ingredient_id")] 
    public int IngredientId { get; set; }

    public List<Ingredient>? Ingredients { get; init; }
}