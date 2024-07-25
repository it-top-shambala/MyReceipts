using FoodApi;
using MyRecipts.WebApiHelperLib.Models;
using tui_netcore;

namespace MyReceipts.UI;

public class AppUI
{
    #region Props

    private static RecipeHelper _recipeHelper = new();
    private static List<RecipeJson>? _webApiRecipes;
    private static List<Recipe>? _recipes;
    #endregion Props

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

    public bool? SearchByIngridients()
    {
        bool? choice = true;
        while (choice != false)
        {
            Tui ingridientsSearchWindow = new Tui();
            ingridientsSearchWindow.Title = "Поиск по ингредиентам";
            ingridientsSearchWindow.Body = "Введите ингредиенты";
            
            IEnumerable<string> userIngridientsList = ingridientsSearchWindow.DrawInput()!.Split(',', ' ', ';','.').ToList();
            var filteredUserIngridientsList = userIngridientsList.Where(item => item != "").ToList();
            userIngridientsList = filteredUserIngridientsList;
            _recipes = _recipeHelper.GetRecipes(userIngridientsList);
            _webApiRecipes = _recipeHelper.GetRecipesJson(userIngridientsList);
            choice = FoundRecipesWindow(_recipes);

            if (choice == null)
                return null;
        }
        return choice;
    }
    
    private bool? FoundRecipesWindow(List<Recipe> recipes)
    {
        Tui recipesWindow = new Tui();
        recipesWindow.Title = "Найденные рецепты";
        recipesWindow.Body = "Рецепты: ";
        
        if (recipes.Count > 0)
        {
            var choice = recipesWindow.DrawList(recipes.Select<Recipe, string>(r => r.Name).ToList());
            var recipe = recipes.First(r => r.Name == choice);
            return ShowRecipeWindow(recipe);
        }
        else
        {
            recipesWindow.Body = "Ничего не найдено";
            recipesWindow.DrawOk();
            return null;
        }
    }

    private bool ShowRecipeWindow(Recipe recipe)
    {
        Tui recipeWindow = new Tui();
        recipeWindow.Title = "Рецепт";
        recipeWindow.Body = $"\n\tНазвание: {recipe.Name}\n" +
                            $"\tИнгредиенты: {ShowRecipeIngridients(recipe)}\n" +
                            $"\tШаги приготовления: \n{ShowInstructions(recipe)}\n" +
                            $"\tДобавить рецепт в избранное?";
                            return recipeWindow.DrawYesNo();
    }
    
    private string ShowInstructions(Recipe recipe)
    {
        string result = string.Join(", ", recipe.Steps.Select(s => "\n" + s.Number + "\n" + s.Description));
        if (result == "")
        {
            result = "К данному рецепту отсутствует подробный процесс приготовления";
        }
        return result;
    }
    private string ShowRecipeIngridients(Recipe recipe)
    {
        string result = string.Join(", ", recipe.Ingredients.Select(i => i.Name + " - " + i.Amount + " " + i.Unit));
        return result;
    }

    public List<RecipeJson> FavoriteAddMenu()
    {
        Tui favouriteMenuWindow = new Tui();
        favouriteMenuWindow.Title = "Добавление рецептов в список избранных";
        favouriteMenuWindow.Body = "*SPACE* выбор рецепта \n *ENTER* подтвердить";
        List<Tui.CheckBoxOption> options = new List<Tui.CheckBoxOption>();
        foreach (var recipe in _recipes)
        {
            options.Add(
                new Tui.CheckBoxOption()
                {
                    IsSelected = false,
                    Name = recipe.Name,
                }
            );
        }

        List<Tui.CheckBoxOption> selectedRecipes = favouriteMenuWindow.DrawCheckBox(options);
        List<string> selectedRecipesNames = selectedRecipes.Select(r => r.Name).ToList();
        List<RecipeJson> resultRecipes = _webApiRecipes.Where(r => selectedRecipesNames.Exists(n => r.Title == n)).ToList();
        return resultRecipes;
    }

    #endregion Methods
}