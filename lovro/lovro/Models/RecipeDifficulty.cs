using System;
using System.Collections.Generic;

#nullable disable

namespace lovro.Models
{
    public partial class RecipeDifficulty
    {
        public RecipeDifficulty()
        {
            Recipes = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public string DiffLevel { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
