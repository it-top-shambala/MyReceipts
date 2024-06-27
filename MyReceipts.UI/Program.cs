using Application;

var db = new DBContext();
var app = new Application.Application(db);
app.StartMenu();
var userChoice = app.ChoiceMenu();
while (userChoice != "2. Выход")
{
    switch (userChoice)
    {
        case "1. Поиск рецепта по ингредиентам":
            app.SearchRecipeByIngridients(app.Database);
            break;
        case "2. Выход":
            break;
    }
}