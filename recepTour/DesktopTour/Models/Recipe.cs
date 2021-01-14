using System;
using System.Collections.Generic;

namespace recepTour
{
    public partial class Recipe
    {
        public Recipe()
        {
            Pictures = new HashSet<Picture>();
            RecipeGroceries = new HashSet<RecipeGrocery>();
            RecipeSteps = new HashSet<RecipeStep>();
            UserFavorites = new HashSet<UserFavorite>();
            UserRecipes = new HashSet<UserRecipe>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? DiffLevelId { get; set; }

        public virtual RecipeDifficulty DiffLevel { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<RecipeGrocery> RecipeGroceries { get; set; }
        public virtual ICollection<RecipeStep> RecipeSteps { get; set; }
        public virtual ICollection<UserFavorite> UserFavorites { get; set; }
        public virtual ICollection<UserRecipe> UserRecipes { get; set; }
    }
}
