// See https://aka.ms/new-console-template for more information
using FoodApi;

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
