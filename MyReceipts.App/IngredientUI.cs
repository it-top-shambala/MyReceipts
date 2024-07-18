using System.ComponentModel.DataAnnotations.Schema;

namespace MyReceipts.App;

[Table("table_ingredients")]

public class IngredientUI
{
    private const int ZERO = 0;

    /// <summary>
    /// Класс, описывающий сущность ингредиента
    /// </summary>

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

/// <summary>
/// Метод для осуществления построения связи между классами в БД при миграции
/// </summary>
    public List<RecipeUI>? Recipes { get; set; }
}