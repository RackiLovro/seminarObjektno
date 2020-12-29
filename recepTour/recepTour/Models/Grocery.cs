using System;
using System.Collections.Generic;

#nullable disable

namespace recepTour.Models
{
    public partial class Grocery
    {
        public Grocery()
        {
            RecipeGroceries = new HashSet<RecipeGrocery>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? TypeId { get; set; }

        public virtual GroceryType Type { get; set; }
        public virtual ICollection<RecipeGrocery> RecipeGroceries { get; set; }
    }
}
