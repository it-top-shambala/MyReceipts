using FoodApi;
using tui_netcore;
using MyRecipts.WebApiHelperLib.Models;

public class AppUI
{
    #region Props

    private static RecipeHelper _recipeHelper = new();
    private List<Recipe>? webApiRecipes;

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
            ingridientsSearchWindow.Body = "Введите ингредиент";
            IEnumerable<string> userIngridientsList = ingridientsSearchWindow.DrawInput()!.Split(',').ToList();
            webApiRecipes = _recipeHelper.GetRecipes(userIngridientsList);
            var foundRecipes = webApiRecipes //FIX ME
                .Where(r => r.UsedIngredients.Exists(i => userIngridientsList.Contains(i.Name))).ToList();
            //choice = RecipesSystem(foundRecipes);
            choice = FoundRecipesWindow(foundRecipes);

            if (choice == null)
                return null;
        }
        return choice;
    }

    private bool? RecipesSystem(List<Recipe> recipes) //FIX ME
    {
        return FoundRecipesWindow(recipes);
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
        recipeWindow.Body = $"\n\tНазвание: {recipe.Title}\n" +
                            $"\tИнгредиенты: {ShowIngridients(recipe)}\n" +
                            $"\tВернуться на экран рецептов?";
        return recipeWindow.DrawYesNo();
    }

    private string ShowIngridients(Recipe recipe) //FIX ME Переименовать название метода
    {
        string result = string.Join(", ", recipe.UsedIngredients.Select(i => i.Name));
        result += ", " + string.Join(", ", recipe.MissedIngredients.Select(i => i.Name));

        return result;
    }

    private static void ErrorWindow(Exception ex)
    {
        Tui errorWindow = new Tui();
        errorWindow.Title = "Ошибка";
        errorWindow.Body = ex.Message;
    }

    public List<Recipe> FavoriteAddMenu() //FIX ME Метод должен получать список для вывода
    {
        Tui favouriteMenuWindow = new Tui();
        favouriteMenuWindow.Title = "Добавление рецептов в список избранных";
        favouriteMenuWindow.Body = "*SPACE* выбор рецепта \n *ENTER* подтвердить";
        List<Tui.CheckBoxOption> options = new List<Tui.CheckBoxOption>();
        foreach (var recipe in webApiRecipes)
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
        List<Recipe> resultRecipes = webApiRecipes.Where(r => selectedRecipesNames.Any(n => r.Title == n)).ToList();
        return resultRecipes;
    }

    #endregion Methods
}