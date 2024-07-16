using FoodApi;
using tui_netcore;
using MyRecipts.WebApiHelperLib.Models;

public class AppUI
{
    #region Props

    /// <summary>
    /// 
    /// </summary>

    private static RecipeHelper _recipeHelper;

    #endregion

    #region Methods

    /// <summary>
    /// 
    /// </summary>
    public void StartMenu()
    {
        Tui startWindow = new Tui();
        startWindow.Title = " Приветствие ";
        startWindow.Body = "Добро пожаловать в поиск рецептов";
        startWindow.DrawOk();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    public void SearchByIngridients()
    {
        bool choice = true;
        while (choice != false)
        {
            Tui ingridientsSearchWindow = new Tui();
            ingridientsSearchWindow.Title = "Поиск по ингредиентам";
            ingridientsSearchWindow.Body = "Введите ингредиент";
            IEnumerable<string> userIngridientsList = ingridientsSearchWindow.DrawInput()!.Split(',').ToList();
            List<Recipe>? webApiRecipes = _recipeHelper.GetRecipes(userIngridientsList);
            var foundRecipes = webApiRecipes
                .Where(r => r.UsedIngredients.Exists(i => userIngridientsList.Contains(i.Name))).ToList();
            choice = RecipesSystem(foundRecipes);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="recipes"></param>
    /// <returns></returns>
    private bool RecipesSystem(List<Recipe> recipes)
    {
        return FoundRecipesWindow(recipes);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="recipes"></param>
    /// <returns></returns>
    private bool FoundRecipesWindow(List<Recipe> recipes) 
    {
        Tui recipesWindow = new Tui();
        recipesWindow.Title = "Найденные рецепты";
        recipesWindow.Body = "Рецепты: ";
        var choice = recipesWindow.DrawList(recipes.Select<Recipe, string>(r => r.Title).ToList());
        var recipe = recipes.First(r => r.Title == choice); 
        return ShowRecipeWindow(recipe);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="recipe"></param>
    /// <returns></returns>
    private bool ShowRecipeWindow(Recipe recipe)
    {
        Tui recipeWindow = new Tui();
        recipeWindow.Title = "Рецепт";
        recipeWindow.Body = $"\n\tНазвание: {recipe.Title}\n" +
                            $"\tИнгредиенты: {ShowIngridients(recipe)}\n" +
                            $"\tВернуться на экран рецептов?";
        return recipeWindow.DrawYesNo();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="recipe"></param>
    /// <returns></returns>
    private string ShowIngridients(Recipe recipe)
    {
        string result = string.Join(", ", recipe.UsedIngredients.Select(i => i.Name));

        return result;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ex"></param>
    private static void ErrorWindow(Exception ex)
    {
        Tui errorWindow = new Tui();
        errorWindow.Title = "Ошибка";
        errorWindow.Body = ex.Message;
    }
    
    //TODO: Сделать реализацию системы избранных рецептов и отправку в ДБ

    #endregion
}