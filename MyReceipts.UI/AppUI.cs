using System.Text;
using tui_netcore;

public class AppUI
{
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


    /*public void SearchByIngridientsWindow()
    {
        Tui ingridientsSearchWindow = new Tui();
        ingridientsSearchWindow.Title = "Поиск по ингредиентам";
        ingridientsSearchWindow.Body = "Введите ингредиент";
        List<string> userIngridientsList = ingridientsSearchWindow.DrawInput()!.Split(',').ToList();
        //TODO: Добавить лист рецептов из WebApi которые содержат ингридиенты
        try
        {
            ShowSearchResult(Recipes); //TODO: Заменить на WebApi
        }
        catch (Exception ex)
        {
            ErrorWindow(ex);
        }
        
    }

    private void ShowSearchResult(List<Recipe> recipes) //TODO: Заменить Recipes на список рецептов из WebApi
    {
        Tui recipesWindow = new Tui();
        recipesWindow.Title = "Найденные рецепты";
        recipesWindow.Body = "Рецепты: ";
        var choice = recipesWindow.DrawList(recipes.Select(r => r.Name).ToList()); //TODO: Заменить LINQ запрос на подходящий из модели рецепта WebApi
        
        try
        {
            Recipe recipe = recipes.First(r => r.Name == choice); //TODO: Заменить LINQ запрос на подходящий из модели рецепта WebApi
            ShowRecipeWindow(recipe); // Это временная ошибка она будет исправлена когда будет добавлены рецепты из WebApi
        }
        catch (Exception ex)
        {
          ErrorWindow(ex);
        }
    }

    private void ShowRecipeWindow(Recipe recipe) //TODO: Заменить Recipe на рецепт из WebApi
    {
        Tui recipeWindow = new Tui();
        recipeWindow.Title = "Рецепт";
        recipeWindow.Body = $"\n\tНазвание: {recipe.Name}\n" +
                            $"\tИнгредиенты: {ShowIngridients(recipe)}";
        recipeWindow.DrawOk();
    }

    private string ShowIngridients(Recipe recipe)
    {
        string result = null;
        foreach (var item in recipe.UsedIngredients)
        {
            
        }

        return result;
    }
    */
    
    public string FavouriteSystem()
    {
        Tui favouriteChoiceWindow = new Tui();
        favouriteChoiceWindow.Title = "Избранное?";
        favouriteChoiceWindow.Body = "Добавить рецепт в избранное?";
        var choice = favouriteChoiceWindow.DrawList(new List<string>()
        {
            "Да",
            "Нет"
        });
        return choice;
    }

    public void FavouriteWindow(string favSysChoice, string menuChoice)
    {
        if ( favSysChoice == "Да")
        {
            // TODO: добавление рецепта в избранное(БД)
            var addRecipeWindow  = new Tui();
            addRecipeWindow.Title  =  "Избранный рецепт";
            addRecipeWindow.Body =  "Рецепт был добавлен в избранное";
            addRecipeWindow.DrawOk();
        }
        else
        {
            menuChoice = ChoiceMenu();
        }
    }

    private void ErrorWindow(Exception ex)
    {
        Tui ErrorWindow = new Tui();
        ErrorWindow.Title = "Ошибка";
        ErrorWindow.Body =  ex.Message;
    }

    #endregion
}