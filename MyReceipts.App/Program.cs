using MyReceipts.App;
using MyReceipts.UI;
//TODO Добавить namespace для базы данных когда она будет создана и поправить ошибки если они будут в связи с этим


var app = new AppUI();
var dbWorker = new DataBaseWorker();
app.StartMenu();
var userChoice = app.ChoiceMenu();
while (userChoice != "2. Выход")
{
    switch (userChoice)
    {
        case "1. Поиск рецепта по ингредиентам":
            app.SearchByIngridients();
            var favouriteRecipes = app.FavoriteAddMenu();
            dbWorker.SaveFavoriteRecipes(favouriteRecipes);
            break;
        case "2. Выход":
            break;
    }
}