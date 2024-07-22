﻿using NewApiTest.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipts.WebApiHelperLib.Models;

public class RecipeStep
{
    public int Number {  get; set; }
    public string Description { get; set; }
    public List<Ingredient> Ingredients { get; set; }
}
