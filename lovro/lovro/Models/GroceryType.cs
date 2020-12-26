using System;
using System.Collections.Generic;

#nullable disable

namespace lovro.Models
{
    public partial class GroceryType
    {
        public GroceryType()
        {
            Groceries = new HashSet<Grocery>();
        }

        public int Id { get; set; }
        public string GroceryType1 { get; set; }

        public virtual ICollection<Grocery> Groceries { get; set; }
    }
}
