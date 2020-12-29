using System;
using System.Collections.Generic;

#nullable disable

namespace recepTour.Models
{
    public partial class GroceryType
    {
        public GroceryType()
        {
            Groceries = new HashSet<Grocery>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Grocery> Groceries { get; set; }
    }
}
