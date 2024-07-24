using MyReceipts.UI;
using MyReceipts.App;
using MyRecipts.WebApiHelperLib.Models;
using FoodApi;

var app = new AppUI();
var dbWorker = new DataBaseWorker();
app.StartMenu();
string? userChoice = "";
while (userChoice != "2. Выход")
{
    userChoice = app.ChoiceMenu();
    switch (userChoice)
    {
        case "1. Поиск рецепта по ингредиентам":
            app.SearchByIngridients();
            if (app.SearchByIngridients() != null)
            {
                var favouriteRecipes = app.FavoriteAddMenu();
                dbWorker.SaveFavoriteRecipes(favouriteRecipes);
            }
            break;
        case "2. Выход":
            break;
    }
}
/*
var helper = new RecipeHelper();
var test = helper.GetRecipes(new List<string> { "fish" });
for (int i = 0; i < test.Count; i++)
{
    Console.WriteLine(test[i].Name);
    Console.WriteLine("Игредиенты:");
    foreach (var ing in test[i].Ingredients)
    {
        Console.WriteLine($"\t{ing.Name}: {ing.Amount} {ing.Unit}");
    }
    Console.WriteLine("Recipe");
    foreach (var step in test[i].Steps)
    {
        Console.WriteLine($"Шаг {step.Number}\nСписок ингредиентов\n\t{String.Join(' ',step.Ingredients.Select(s => new string(s.Name)).ToList())}\n\t {step.Description}");
        
    }
    Console.WriteLine("============================");
}
*/
