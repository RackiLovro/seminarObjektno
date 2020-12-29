using System;
using System.Collections.Generic;

#nullable disable

namespace recepTour.Models
{
    public partial class RecipeStep
    {
        public int Id { get; set; }
        public int? StepNumber { get; set; }
        public int? RecipeId { get; set; }
        public string Description { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
