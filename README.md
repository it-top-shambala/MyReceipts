```mermaid
---
title: MyReceipts
---
classDiagram

   class Ingredient{
        +const ZERO 
        +String Name
        +Int Quantity
        +<List> Recipe 
    }
    
   class Recipe{
        +String Name
    }

class MisssedIngredients {

    }
    class DBContext{
        +DBSet <Recipe>
    }

    class RecipeHelper
    ```
