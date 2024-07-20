var app = new AppUI();
app.StartMenu();
var userChoice = app.ChoiceMenu();
while (userChoice != "2. Выход")
{
    switch (userChoice)
    {
        case "1. Поиск рецепта по ингредиентам":
            app.SearchByIngridients();
            break;
        case "2. Выход":
            break;
    }
}