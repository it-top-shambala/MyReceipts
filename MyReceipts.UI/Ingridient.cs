namespace Application;

public class Ingridient
{
    private const int ZERO = 0;
    
    public string Name
    {
        get => Name;
        set => Name = (!string.IsNullOrEmpty(value) 
            ? throw new ArgumentNullException() 
            : null)!;
    }
    public int Quantity
    {
        get => Quantity;
        set => Quantity = value <= ZERO
            ? throw new ArgumentOutOfRangeException(nameof(Quantity))
            : value;
    }
}