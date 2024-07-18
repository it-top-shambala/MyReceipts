using MyReceipts.App;

var app = new AppUI();
DataBaseWorker DBWorker = new DataBaseWorker();
app.StartMenu();
//var userChoice = app.ChoiceMenu();
string? userChoice = "";
while (userChoice != "2. Выход")
{
    userChoice = app.ChoiceMenu();
    switch (userChoice)
    {
        case "1. Поиск рецепта по ингредиентам":
            if (app.SearchByIngridients() != null)
            {
                var favouriteRecipes = app.FavoriteAddMenu();
                DBWorker.SaveFavoriteRecipes(favouriteRecipes);
            }
            break;

        case "2. Выход":
            break;
    }
}