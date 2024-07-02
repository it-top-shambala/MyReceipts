using System.Text;
using FoodApi;
using MyRecipts.WebApiHelperLib.Models;

namespace Application;

using tui_netcore;

public class AppUI
{
    #region Properties
    // TODO: Добавить БД
    private Recipe recipe; //Заглушка (Заменить на модель рецепта из WebApi);
    private List<Recipe> Recipes; //Заглушка(Заменить на рецепты из WebApi);
    public Cli Cli;

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


    public void SearchByIngridients()
    {
        Tui ingridientsSearchWindow = new Tui();
        ingridientsSearchWindow.Title = "Поиск по ингредиентам";
        ingridientsSearchWindow.Body = "Введите ингредиент";
        List<string> userIngridientsList = ingridientsSearchWindow.DrawInput().Split(',').ToList();
        if (Recipes != null)
        {
            ShowSearchResult(Recipes); //TODO: Заменить Recipes на список рецептов из WebApi
        }
    }

    private void ShowSearchResult(List<Recipe> recipes) //TODO: Заменить Recipes на список рецептов из WebApi
    {
        Tui recipesWindow = new Tui();
        recipesWindow.Title = "Найденные рецепты";
        recipesWindow.Body = "Рецепты: ";
        var choice = recipesWindow.DrawList(recipes.Select(r => r.Name).ToList()); //TODO: Заменить LINQ запрос на подходящий из модели рецепта WebApi
        if (choice != null)
        {
            Recipe recipe = recipes.First(r => r.Name == choice); //TODO: Заменить LINQ запрос на подходящий из модели рецепта WebApi
            ShowRecipeWindow(recipe); // Это временная ошибка она будет исправлена когда будет добавлены рецепты из WebApi
        }
    }

    private void ShowRecipeWindow(Recipe recipe)
    {
        Tui recipeWindow = new Tui();
        recipeWindow.Title = "Рецепт";
        recipeWindow.Body = $"\n\tНазвание: {recipe.Name}\n" +
                            $"\tИнгредиенты: {ShowIngridients(recipe)}";
        recipeWindow.DrawOk();
    }

    private string ShowIngridients(Recipe recipe)
    {
        StringBuilder sb = new StringBuilder();
        //TODO: Добавить foreach где stringBuilder будет создавать список ингредиентов из рецепта (пример ниже)
        /*foreach (var item in recipe.UsedIngredients)
        {
            sb.Append(item.Name).Append(" - ").Append(item.Amount).Append(" - ").Append(item.Unit);
        }*/

        return sb.ToString();
    }
    
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

    #endregion
}