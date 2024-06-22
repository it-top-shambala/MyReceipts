using Application;

var db = new DBContext();
var App = new Application.Application(db);
App.StartMenu();
var userChoice = Console.ReadLine();
while (userChoice != "3")
{
    switch (userChoice)
    {
        case "1":
            App.SearchRecipeByName(App.database);
            break;
        case "2":
            App.SearchRecipeByIngridients(App.database);
            break;
        default:
            Cli.PrintError("Неверный ввод");
            break;
    }
    App.StartMenu();
    userChoice = Console.ReadLine();
}