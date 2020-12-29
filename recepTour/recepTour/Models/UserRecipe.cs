using System;
using System.Collections.Generic;

#nullable disable

namespace recepTour.Models
{
    public partial class UserRecipe
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual User User { get; set; }
    }
}
