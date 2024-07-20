using MyReceipts.UI;
using MyReceipts.App;
using MyRecipts.WebApiHelperLib.Models;

var app = new AppUI();
var dbWorker = new DataBaseWorker();
var webApiRecipes = new List<Recipe>();
app.StartMenu();
string? userChoice = "";
while (userChoice != "2. Выход")
{
    userChoice = app.ChoiceMenu();
    switch (userChoice)
    {
        case "1. Поиск рецепта по ингредиентам":
            app.SearchByIngridients(webApiRecipes);
            if (app.SearchByIngridients(webApiRecipes) != null)
            {
                var favouriteRecipes = app.FavoriteAddMenu(webApiRecipes);
                dbWorker.SaveFavoriteRecipes(favouriteRecipes);
            }
            break;
        case "2. Выход":
            break;
    }
}