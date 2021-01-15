using System;
using System.Collections.Generic;

namespace DesktopTour
{
    public partial class User
    {
        public User()
        {
            UserFavorites = new HashSet<UserFavorite>();
            UserRecipes = new HashSet<UserRecipe>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string PasswordHash { get; set; }
        public string Status { get; set; }
        public bool? EmailConfirmed { get; set; }
        public int? UserTypeId { get; set; }

        public virtual UserType UserType { get; set; }
        public virtual ICollection<UserFavorite> UserFavorites { get; set; }
        public virtual ICollection<UserRecipe> UserRecipes { get; set; }
    }
}
