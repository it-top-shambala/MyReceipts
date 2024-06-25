namespace Application;
using tui_netcore;

public class Application
{
    #region Properties

    public readonly DBContext Database;
    public Cli Cli;
    
    #endregion

    #region Constructors

    public Application(DBContext database)
    {
        this.Database = database;
    }

    #endregion

    #region Methods

    public void StartMenu()
    {
        Tui startWindow = new Tui();
        startWindow.Title = " Приветствие ";
        startWindow.Body = "Добро пожаловать в поиск";
        startWindow.DrawOk();
    }

    public string ChoiceMenu()
    {
        Tui choiceWindow = new Tui();
        choiceWindow.Title = "Меню";
        choiceWindow.Body = "Выберите что вы хотите сделать";
        string choice = choiceWindow.DrawList(new List<string>()
        {
            "1. Поиск рецепта по названию",
            "2. Поиск рецепта по ингредиентам",
            "3. Выход"
        });
        return choice;
    }

    public void SearchRecipeByName(DBContext db)
    {
        Tui titleSearchWindow = new Tui();
        titleSearchWindow.Title = "Поиск по названию";
        titleSearchWindow.Body = "Введите название рецепта";
        string userRecipeName = titleSearchWindow.DrawInput();
        var dbRecipes = db.Recipes.ToList().Where(r => r.Name == userRecipeName);
        foreach (var recipe in dbRecipes)
        {
            Console.WriteLine($"Название: {recipe.Name}");
            Console.WriteLine($"Ингредиенты:");
            recipe.ShowIngridients(recipe.Ingridients);
            Console.WriteLine("************************");
        }
    }

    public void SearchRecipeByIngridients(DBContext db)
    {
        Tui ingridientsSearchWindow = new Tui();
        ingridientsSearchWindow.Title  =  "Поиск по ингредиентам";
        ingridientsSearchWindow.Body  =  "Введите ингредиент";
        List<string> userIngridientsList = ingridientsSearchWindow.DrawInput().Split(',').ToList();
        Tui ingWindow  = new Tui();
        ingWindow.Title  =   "Проверка ингридиентов";
        ingWindow.Body = $"Введённые ингредиенты: {Cli.ListPrint(userIngridientsList)}";
        var ynChoice = ingWindow.DrawYesNo();
        if (ynChoice = true)
        {
            var dbRecipes = db.Recipes.Where(r => r.Ingridients.Any(i => userIngridientsList.Contains(i.Name)));
            foreach (var recipe in dbRecipes)
            {
                var recipeWindow = new Tui();
                recipeWindow.Title = $"Рецепт {recipe.Name}";
                recipeWindow.Body = $"Ингредиенты: {recipe.ShowIngridients(recipe.Ingridients)}";
                
            }
        }
        
    }
    #endregion
}