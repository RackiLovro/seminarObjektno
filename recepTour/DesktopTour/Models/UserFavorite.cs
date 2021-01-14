using System;
using System.Collections.Generic;

namespace recepTour
{
    public partial class UserFavorite
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual User User { get; set; }
    }
}
