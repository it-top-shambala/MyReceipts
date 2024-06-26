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
        startWindow.Body = "Добро пожаловать в поиск рецептов";
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
    
    private void ShowRecipeWindow(Recipe recipe)
    {
        Tui recipeWindow = new Tui();
        recipeWindow.Title = "Рецепт";
        recipeWindow.Body = $"\n\tНазвание: {recipe.Name}\n" +
                            $"\tКалорийность: {recipe.CalorieContent}\n" +
                            $"\tИнгредиенты: {recipe.ShowIngridients(recipe.Ingridients)}";
        recipeWindow.DrawOk();
    }
    
    private void ShowIngridientsSearchResult(IEnumerable<Recipe> recipes)
    {
        Tui recipesWindow = new Tui();
        recipesWindow.Title  = "Найденные рецепты";
        recipesWindow.Body = "Рецепты: ";
        var choice = recipesWindow.DrawList(recipes.Select(r  => r.Name).ToList());
        if (choice != null)
        {
            var recipe = recipes.First(r => r.Name == choice);
            ShowRecipeWindow(recipe);
        }
    }

    public void SearchRecipeByName(DBContext db)
    {
        Tui titleSearchWindow = new Tui();
        titleSearchWindow.Title = "Поиск по названию";
        titleSearchWindow.Body = "Введите название рецепта";
        string userRecipeName = titleSearchWindow.DrawInput();
        var dbRecipes = db.Recipes.ToList().Where(r => r.Name == userRecipeName);
        if (dbRecipes.Any(r => r.Name == userRecipeName))
        {
            var recipe = dbRecipes.First(r => r.Name == userRecipeName);
            ShowRecipeWindow(recipe);
        }
    }

    public void SearchRecipeByIngridients(DBContext db)
    {
        Tui ingridientsSearchWindow = new Tui();
        ingridientsSearchWindow.Title = "Поиск по ингредиентам";
        ingridientsSearchWindow.Body = "Введите ингредиент";
        List<string> userIngridientsList = ingridientsSearchWindow.DrawInput().Split(',').ToList();
        var dbRecipes = db.Recipes.
            ToList()
                .Where(r => r.Ingridients.Any(i => userIngridientsList.Contains(i.Name)));
        if (dbRecipes.Any(r  => r.Ingridients.Any(i  => userIngridientsList.Contains(i.Name))));
        {
            ShowIngridientsSearchResult(dbRecipes);
        }
    }
    
    

    #endregion
}