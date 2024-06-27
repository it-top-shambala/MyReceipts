using System.Text;
using FoodApi;
using MyRecipts.WebApiHelperLib.Models;

namespace Application;

using tui_netcore;

public class AppUI
{
    #region Properties

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
        List<MyRecipts.WebApiHelperLib.Models.Recipe>? webAPIRecipes =
            new RecipeHelper().GetRecipes(userIngridientsList);
        if (webAPIRecipes != null)
        {
            ShowSearchResult(webAPIRecipes);
        }
    }

    private void ShowSearchResult(List<MyRecipts.WebApiHelperLib.Models.Recipe> recipes)
    {
        Tui recipesWindow = new Tui();
        recipesWindow.Title = "Найденные рецепты";
        recipesWindow.Body = "Рецепты: ";
        var choice = recipesWindow.DrawList(recipes.Select(r => r.Title).ToList());
        if (choice != null)
        {
            MyRecipts.WebApiHelperLib.Models.Recipe recipe = recipes.First(r => r.Title == choice);
            ShowRecipeWindow(recipe);
        }
    }

    private void ShowRecipeWindow(MyRecipts.WebApiHelperLib.Models.Recipe recipe)
    {
        Tui recipeWindow = new Tui();
        recipeWindow.Title = "Рецепт";
        recipeWindow.Body = $"\n\tНазвание: {recipe.Title}\n" +
                            $"\tИнгредиенты: {ShowIngridients(recipe)}";
        recipeWindow.DrawOk();
    }

    private string ShowIngridients(MyRecipts.WebApiHelperLib.Models.Recipe recipe)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in recipe.UsedIngredients)
        {
            sb.Append(item.Name).Append(" - ").Append(item.Amount).Append(" - ").Append(item.Unit);
        }

        return sb.ToString();
    }

    #endregion
}