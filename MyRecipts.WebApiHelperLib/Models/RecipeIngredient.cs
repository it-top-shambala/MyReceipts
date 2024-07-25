using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipts.WebApiHelperLib.Models
{
    public class RecipeIngredient
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
    }
}