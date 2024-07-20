using MyReceipts.App;
using MyReceipts.UI;
//TODO Добавить namespace для базы данных когда она будет создана и поправить ошибки если они будут в связи с этим


var app = new AppUI();
var dbWorker = new DataBaseWorker();
app.StartMenu();
//var userChoice = app.ChoiceMenu();
string? userChoice = "";
while (userChoice != "2. Выход")
{
    userChoice = app.ChoiceMenu();
    switch (userChoice)
    {
        case "1. Поиск рецепта по ингредиентам":
<<<<<<< HEAD
            app.SearchByIngridients();
            var favouriteRecipes = app.FavoriteAddMenu();
            dbWorker.SaveFavoriteRecipes(favouriteRecipes);
=======
            if (app.SearchByIngridients() != null)
            {
                var favouriteRecipes = app.FavoriteAddMenu();
                DBWorker.SaveFavoriteRecipes(favouriteRecipes);
            }
>>>>>>> 9d292a106ae13daec9cb520932e38cd2348b9d7f
            break;

        case "2. Выход":
            break;
    }
}