```mermaid
---
title: MyReceipts
---
classDiagram

   class Ingredient{
        +const ZERO 
        +String Name
        +Int Quantity
        +List <Recipe>
    }
    
   class Recipe{
        +String Name
        +Int CalorieContent
        +List <Ingredient>
        + ShowIngredient()
    }

class MisssedIngredients {

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
    +HttpHelper _httpHelper
    +const string BASE_URI
    +const string PATH_CFG
    +String _apiKey
    +RecipeHelper()
    +List<Recipe>? GetRecipes
    +String GetResponseJsonFromHttpCLient()
    +string CreateRequiestUri()
    +void GetApiKeyFromJson()
 }

 class HttpHelper{
    +HttpClient _httpClient
    +HttpHelper()
    +string GetResponseBody()
    +HttpResponseMessage? GetResponse()
 }
    ```
