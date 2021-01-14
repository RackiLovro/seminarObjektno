using System;
using System.Collections.Generic;

namespace recepTour
{
    public partial class Picture
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
