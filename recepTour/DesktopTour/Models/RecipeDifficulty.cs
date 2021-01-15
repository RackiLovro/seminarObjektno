using System;
using System.Collections.Generic;

namespace DesktopTour
{
    public partial class RecipeDifficulty
    {
        public RecipeDifficulty()
        {
            Recipes = new HashSet<Recipe>();
        }

        public int DiffLevel { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
