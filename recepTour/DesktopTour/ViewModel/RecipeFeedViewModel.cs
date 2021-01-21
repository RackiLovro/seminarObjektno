using DesktopTour.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopTour.ViewModel
{
    class RecipeFeedViewModel : INotifyPropertyChanged
    {
        private readonly List<Recipe> _recipes;
        private readonly DesktopTourContext _context = new DesktopTourContext();

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public RecipeFeedViewModel()
        {
            _recipes = _context.Recipes.ToList();
            _recipes.ForEach(r => r.DiffLevel = _context.RecipeDifficulties.Where(rd => rd.DiffLevel == r.DiffLevelId).Single());

            _recipes.ForEach(r => r.UserRecipes = _context.UserRecipes.Where(ur => ur.RecipeId == r.Id).ToList());

            _recipes.ForEach(r => r.UserRecipes.First().User = _context.Users.Where(u => u.Id == r.UserRecipes.First().UserId).Single());
        }

        public List<Recipe> Recipes
        {
            get { return _recipes; }
        }
    }
}
