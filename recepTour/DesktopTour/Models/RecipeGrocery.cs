using System;
using System.Collections.Generic;

namespace DesktopTour
{
    public partial class RecipeGrocery
    {
        public int RecipeId { get; set; }
        public int GroceryId { get; set; }
        public string Amount { get; set; }
        public string Ingridient
        {
            get { return Grocery.Name; }
        }
        public virtual Grocery Grocery { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
