using System.ComponentModel.DataAnnotations.Schema;

namespace Application;

public class Ingredient
{
    private const int ZERO = 0;
    
    [Column("ingredient_name")]
    public string Name
    {
        get => Name;
        set => Name = (!string.IsNullOrEmpty(value) 
            ? throw new ArgumentNullException() 
            : null)!;
    }
    
    [Column("quantity")]
    public int Quantity
    {
        get => Quantity;
        set => Quantity = value <= ZERO
            ? throw new ArgumentOutOfRangeException(nameof(Quantity))
            : value;
    }

    public List<Recipe> Recipes { get; set; }
}