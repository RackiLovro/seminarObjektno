using System;
using System.Collections.Generic;

namespace recepTour
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
