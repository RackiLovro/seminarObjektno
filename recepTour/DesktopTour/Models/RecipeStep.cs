using System;
using System.Collections.Generic;

namespace recepTour
{
    public partial class RecipeStep
    {
        public int Id { get; set; }
        public int? StepNumber { get; set; }
        public string Description { get; set; }
        public int? RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
