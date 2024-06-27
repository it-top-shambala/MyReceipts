using System.Text;

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
            "1. Поиск рецепта по ингредиентам",
            "2. Выход"
        });
        return choice;
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
    
    private void ShowRecipeWindow(Recipe recipe)
    {
        Tui recipeWindow = new Tui();
        recipeWindow.Title = "Рецепт";
        recipeWindow.Body = $"\n\tНазвание: {recipe.Name}\n" +
                            $"\tКалорийность: {recipe.CalorieContent}\n" +
                            $"\tИнгредиенты: {ShowIngridients(recipe)}";
        recipeWindow.DrawOk();
        
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

    private void AddToFavorites(Recipe recipe, DBContext db)
    {
        Tui favoritesWindow = new Tui();
        favoritesWindow.Title = "Добавление в избранное";
        favoritesWindow.Body = "Добавить рецепт в избранное?";
        var choice = favoritesWindow.DrawYesNo();
        if (choice == true)
        {
            db.Recipes.Add(recipe);
            db.SaveChanges();
        }
        else
        {
            favoritesWindow.DrawOk();
        }
    }
    
    private string ShowIngridients(Recipe recipe)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in recipe.Ingridients)
        {
            sb.Append(item.Name).Append(" - ").Append(item.Quantity);
        }
        return sb.ToString();
    }

    #endregion
}