using Application;

var db = new DBContext();
var app = new Application.Application(db);
app.StartMenu();
var userChoice = app.ChoiceMenu();
while (userChoice != "3. Выход")
{
    switch (userChoice)
    {
        case "1":
            app.SearchRecipeByName(app.Database);
            break;
        case "2":
            app.SearchRecipeByIngridients(app.Database);
            break;
    }
    app.StartMenu();
    userChoice = Console.ReadLine();
}