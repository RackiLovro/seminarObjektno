using System;
using System.Collections.Generic;

#nullable disable

namespace lovro.Models
{
    public partial class RecipeStep
    {
        public int Id { get; set; }
        public int? StepNumber { get; set; }
        public int? RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
