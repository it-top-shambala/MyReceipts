using System.ComponentModel.DataAnnotations.Schema;

namespace MyReceipts.App;

[Table("table_recipes")]
public class RecipeUI
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

    public List<IngredientUI>? Ingredients { get; set; }
}