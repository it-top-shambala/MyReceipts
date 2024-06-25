using System.ComponentModel.DataAnnotations.Schema;

namespace Application;

public class Recipe
{
    private const int ZERO = 0;
    [Column("id")]
    public int Id { get; set; }
    [Column("Name")]
    public string Name
    {
        get => Name;
        set => Name = (!string.IsNullOrEmpty(value) 
            ? throw new ArgumentNullException() 
            : null)!;
    }
    [Column("calories")]
    public int CalorieContent
    {
        get => CalorieContent;
        set => CalorieContent = value <= ZERO
            ? throw new ArgumentOutOfRangeException(nameof(CalorieContent))
            : value;
    }
    public Ingredient Ingredient { get; set; }
}