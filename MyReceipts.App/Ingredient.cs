using System.ComponentModel.DataAnnotations.Schema;

namespace Application;

[Table("table_ingredients")]
public class Ingredient
{
    private const int ZERO = 0;

    [Column("id")] 
    public int Id { get; init; }

    [Column("ingredient_name")] 
    public string? Name { get; set; }


    [Column("quantity")] 
    public int Quantity { get; set; }


    [Column("unit")] 
    public string? Unit { get; set; }
    
    [Column("recipe_id")] 
    public int RecipeId { get; set; }


    public List<Recipe>? Recipes { get; init; }
}