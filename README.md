```mermaid
---
title: MyReceipts
---
classDiagram

   class Ingredient{
        -const ZERO 
        +String Name
        +Int Quantity
        +String Unit
        +List <Recipe> Recipes
    }
    
   class Recipe{
        +String Name
        +Int CalorieContent
        +List <Ingredient>
        + ShowIngredient()
    }

 class DBContext{
        +DBSet <Recipes> Recipes
        +DbSet<Ingredient> Ingredients
        +DBContext()
        +void OnConfiguring
    }

 class Application {
    +DBContext Database
    +Cli Cli
    + Application()
    +void StartMenu()
    +String ChoiceMenu()
    +void SearchRecipeByIngridients()
 }
 
 class Cli {
    +void PrintLine()
    +void PrintError()
    +String LastPrint()
 }

 class RecipeHelper {
    -HttpHelper _httpHelper
    -LogToFile _logger
    -const string PATH_CFG = "Configs\\ConfigRecipeHelper.json"
    -ConfigRecipeHepler _config
    +RecipeHelper()
    +List<Recipe>? GetRecipes(IEnumerable<string> ingredients)
    -string GetResponseJsonFromHttpCLient(IEnumerable<string> ingredients)
    -string CreateRequiestUri(IEnumerable<string> ingredients)
    -void InitConfig()
 }

 class HttpHelper{
    -HttpClient _httpClient
    +HttpHelper(string baseUri)
    -const string PATH_CFG = "Configs\\ConfigHttpHelper.json"
    -ConfigHttpHelper _config
    -LogToFile _logger
    +string GetResponseBody(string requestUri)
    -HttpResponseMessage? GetResponse(string requiestUri)
    -void InitConfig()
 }

 RecipeHelper --* HttpHelper
 Application --* DBContext
 Application --* Cli
 Ingredient --> Recipe

 class ConfigRecipeHepler {
    +string ApiKey
    +string BaseUri
    +string RawRequestUri
    +string ApiKeyConnector
    +string IngredientsConnector
    +string Connector
    +string PathToLogger
 }

 class MissedIngredient{
    +int Id
    +double Amount
    +string Unit
    +string UnitLong
    +string UnitShort
    +string Aisle
    +string Name
    +string Original 
    +string OriginalName
    +List<string> Meta
    +string Image
    +string ExtendedName
 }

 class Recipe {
    +int Id
    +string Title
    +string Image
    +string ImageType
    +int UsedIngredientCount
    +int MissedIngredientCount
    +List<MissedIngredient> MissedIngredients
    +List<UsedIngredient> UsedIngredients
    +List<object> UnusedIngredients
    +int Likes
 }

 class UsedIngredient{
    +int Id
    +double Amount
    +string Unit
    +string UnitLong
    +string UnitShort
    +string Aisle
    +string Name
    +string Original
    +string OriginalName
    +List<string> Meta
    +string Image
    +string ExtendedName
 }

 class ConfighttpHelper {
    +string PathToLogger
 }

 ConfighttpHelper --* ConfigRecipeHepler
 MissedIngredient --> Recipe
 UsedIngredient --> Recipe

 class AppUI{
    -Recipe recipe
    -List<Recipe> Recipes
    +Cli Cli
    +void StartMenu()
    +string ChoiceMenu()
    +void SearchByIngridients()
    -void ShowSearchResult(List<Recipe> recipes)
    -void ShowRecipeWindow (Recipe recipe)
    -string ShowIngridients(Recipe recipe)
    +string FavouriteSystem()
    +void FavouriteWindow(string favSysChoice, string menuChoice)
 }

 class RecipeUI{
    -const int ZERO
    +int Id
    +string? Name
    +int CalorieContent
    +int IngredientId
    +List<Ingredient>? Ingredients
 }

 class IngredientUI {
    -const int ZERO
    +int Id
    +string? Name
    +int Quantity
    +string? Unit
    +int RecipeId
    +List<Recipe>? Recipes
 }

 Cli --> AppUI
 RecipeUI --> DBContext
 RecipeUI --o IngredientUI
 IngredientUI --o RecipeUI

 class DataBaseWorker{
    -LogToFile _logger = new LogToFile()
    +void SaveFavoriteRecipes(List<Recipe> recipes)
    -RecipeUI Recipe1ConvertRecipe(Recipe recipe1)
    -IngredientUI FusionIngredientsToMissed(MissedIngredient ingredientFromWebApi)
    -IngredientUI FusionIngredientsToUsed(UsedIngredient ingredientFromWebApi)
    +IEnumerable <RecipeUI> GetRecipes()
    -void SaveToDb(List <RecipeUI> recipes)
 }

 Recipe --> RecipeHelper
 DataBaseWorker --> DBContext
 AppUI --> RecipeHelper
 HttpHelper --> DataBaseWorker
    ```
