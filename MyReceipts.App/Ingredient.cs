using System.ComponentModel.DataAnnotations.Schema;

namespace Application;

public class Ingredient
{
    private const int ZERO = 0;

    [Column("id")] 
    public int Id { get; set; }

    [Column("ingredient_name")]
    public string Name
    {
        get => Name;
        set => Name = (string.IsNullOrEmpty(value) 
            ? throw new ArgumentNullException() 
            : value);
    }
    
    [Column("quantity")]
    public int Quantity
    {
        get => Quantity;
        set => Quantity = value <= ZERO
            ? throw new ArgumentOutOfRangeException(nameof(Quantity))
            : value;
    }
    
    [Column("unit")]
    public string Unit
    {
        get => Unit;
        set => Unit = (string.IsNullOrEmpty(value) 
            ? throw new ArgumentNullException() 
            : value);
    }

    public List<Recipe> Recipes { get; set; }
}