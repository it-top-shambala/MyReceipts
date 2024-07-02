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
        +DBSet <Recipes>
        +DBContext()
        +void OnConfiguring
    }

 class Application {
    +DBContext Database
    +Cli Cli
    + Application()
    +void StartMenu()
    +String ChoiceMenu()
    +void SearchRecipeByName()
    +void SearchRecipeByIngridients()
 }
 
 class Cli {
    +void PrintLine()
    +void PrintError()
    +String LastPrint()
 }

 class RecipeHelper {
    -HttpHelper _httpHelper
    -const string BASE_URI
    -const string PATH_CFG
    -String _apiKey
    +RecipeHelper()
    +List<Recipe>? GetRecipes
    -String GetResponseJsonFromHttpCLient()
    -string CreateRequiestUri()
    -void GetApiKeyFromJson()
 }

 class HttpHelper{
    -HttpClient _httpClient
    +HttpHelper()
    +string GetResponseBody()
    -HttpResponseMessage? GetResponse()
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
    ```
