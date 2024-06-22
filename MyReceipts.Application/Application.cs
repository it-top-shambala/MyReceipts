namespace Application;

public class Application
{
    #region Properties

    public DBContext database;
    public Cli Cli;
    
    #endregion

    #region Constructors

    public Application(DBContext database)
    {
        this.database = database;
    }

    #endregion

    #region Methods

    public void StartMenu()
    {
        Console.WriteLine("************************");
        Cli.PrintLine("ДОБРО ПОЖАЛОВАТЬ В ПОИСК");
        Console.WriteLine("------------------------");
        Console.WriteLine("1. Поиск рецепта по названию");
        Console.WriteLine("2. Поиск рецепта по ингредиентам");
        Console.WriteLine("3. Выход");
        Console.WriteLine("************************");
        Console.Write("Выберите пункт меню: ");
    }

    public void SearchRecipeByName(DBContext db)
    {
        Console.WriteLine("Введите название рецепта: ");
        string userRecipeName = Console.ReadLine();
        var dbRecipes = db.Recipes.ToList().Where(r => r.Name == userRecipeName);
        foreach (var recipe in dbRecipes)
        {
            Console.WriteLine($"Название: {recipe.Name}");
            Console.WriteLine($"Ингредиенты:");
            recipe.ShowIngridients();
            Console.WriteLine("************************");
        }
    }

    public void SearchRecipeByIngridients(DBContext db)
    {
        Console.WriteLine("Введите ингредиенты через запятую: ");
        List<string> userIngridientsList = Console.ReadLine().Split(',').ToList();
        var dbRecipes = db.Recipes.Where(r => r.Ingridients.Any(i => userIngridientsList.Contains(i.Name)));
        foreach (var recipe in dbRecipes)
        {
            Console.WriteLine($"Название: {recipe.Name}");
            Console.WriteLine($"Ингредиенты:");
            recipe.ShowIngridients();
            Console.WriteLine("************************");
        }
    }

    #endregion
}