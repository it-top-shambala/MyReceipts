using FoodApi;
using MyRecipts.WebApiHelperLib.Models;
using NewApiTest.models;
using tui_netcore;

namespace MyReceipts.UI;

public class AppUI
{
    #region Props

    private static RecipeHelper _recipeHelper = new();
    private Dictionary<Recipe, Instruction> _recipesWithInstructions;

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

    public bool? SearchByIngridients(List<Recipe> recipes)
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
            recipes = _recipeHelper.GetRecipes(userIngridientsList);
            _recipesWithInstructions = _recipeHelper.GetRecipeWithInstruction(userIngridientsList);
            choice = FoundRecipesWindow(recipes);

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
        
        if (recipes != null && recipes.Count > 0)
        {
            var choice = recipesWindow.DrawList(recipes.Select<Recipe, string>(r => r.Title).ToList());
            var recipe = recipes.First(r => r.Title == choice);
            ShowRecipeWindow(recipe);
            return ShowRecipeStepsWindow(recipe);
        }
        else
        {
            recipesWindow.Body = "Ничего не найдено";
            recipesWindow.DrawOk();
            return null;
        }
    }

    private void ShowRecipeWindow(Recipe recipe)
    {
        Tui recipeWindow = new Tui();
        recipeWindow.Title = "Рецепт";
        recipeWindow.Body = $"\n\tНазвание: {recipe.Title}\n" +
                            $"\tИнгредиенты: {ShowRecipeIngridients(recipe)}\n" +
                            $"\n\tДля показа шагов приготовления нажмите любую клавишу\n";
                            recipeWindow.DrawOk();
    }

    private bool ShowRecipeStepsWindow(Recipe recipe)
    {
        Tui recipeStepsWindow = new Tui();
        recipeStepsWindow.Title = "Шаги приготовления";
        recipeStepsWindow.Body = $"{ShowInstructions(recipe, _recipesWithInstructions)} \n" +
                                 $"\tДобавить рецепт в избранное?";
        return recipeStepsWindow.DrawYesNo();
    }

    private string ShowInstructions(Recipe recipe, Dictionary<Recipe, Instruction> recipes)
    {
        string result = "";
        foreach (var item in recipes)
        {
            if (item.Key == recipe)
            {
                result = string.Join("\n", item.Value.Steps.Select(s => s.Number) + " - " + item.Value.Steps.Select(s => s.StepDiscription));
            }
            else
            {
                result = "К данному рецепту отсутствует подробный процесс приготовления";
            }
        }

        return result;
    }
    private string ShowRecipeIngridients(Recipe recipe)
    {
        string result = string.Join(", ", recipe.UsedIngredients.Select(i => i.Name));
        result += ", " + string.Join(", ", recipe.MissedIngredients.Select(i => i.Name));

        return result;
    }

    public List<Recipe> FavoriteAddMenu(List<Recipe> recipes)
    {
        Tui favouriteMenuWindow = new Tui();
        favouriteMenuWindow.Title = "Добавление рецептов в список избранных";
        favouriteMenuWindow.Body = "*SPACE* выбор рецепта \n *ENTER* подтвердить";
        List<Tui.CheckBoxOption> options = new List<Tui.CheckBoxOption>();
        foreach (var recipe in recipes)
        {
            options.Add(
                new Tui.CheckBoxOption()
                {
                    IsSelected = false,
                    Name = recipe.Title,
                }
            );
        }

        List<Tui.CheckBoxOption> selectedRecipes = favouriteMenuWindow.DrawCheckBox(options);
        List<string> selectedRecipesNames = selectedRecipes.Select(r => r.Name).ToList();
        List<Recipe> resultRecipes = recipes.Where(r => selectedRecipesNames.Any(n => r.Title == n)).ToList();
        return resultRecipes;
    }

    #endregion Methods
}