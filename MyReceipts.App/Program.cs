using MyReceipts.App;
using MyReceipts.UI;
using MyRecipts.WebApiHelperLib.Models;

//TODO Добавить namespace для базы данных когда она будет создана и поправить ошибки если они будут в связи с этим


var app = new AppUI();
var dbWorker = new DataBaseWorker();
var webApiRecipes = new List<Recipe>();
app.StartMenu();
//var userChoice = app.ChoiceMenu();
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