using System;
using System.Collections.Generic;

#nullable disable

namespace lovro.Models
{
    public partial class Grocery
    {
        public Grocery()
        {
            RecipeGroceries = new HashSet<RecipeGrocery>();
        }

        public int Id { get; set; }
        public string GroceryName { get; set; }
        public int? GroceryTypeId { get; set; }

        public virtual GroceryType GroceryType { get; set; }
        public virtual ICollection<RecipeGrocery> RecipeGroceries { get; set; }
    }
}
