using DesktopTour.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopTour.ViewModel
{
    class RecipeDetailsViewModel : INotifyPropertyChanged
    {
        private readonly Recipe _recipe;
        private readonly DesktopTourContext _context = new DesktopTourContext();

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public RecipeDetailsViewModel(object recipe)
        {
            _recipe = recipe as Recipe;

            _recipe.RecipeSteps = _context.RecipeSteps.Where(rs => rs.RecipeId == _recipe.Id).ToList();

            _recipe.RecipeGroceries = _context.RecipeGroceries.Where(rg => rg.RecipeId == _recipe.Id).ToList();
            _recipe.RecipeGroceries.ToList().ForEach(rg => rg.Grocery = _context.Groceries.Where(g => g.Id == rg.GroceryId).Single());

            _recipe.Pictures = _context.Pictures.Where(p => p.RecipeId == _recipe.Id).ToList();
        }

        public List<Picture> Pictures
        {
            get { return _recipe.Pictures.ToList(); }
        }

        public string Title
        {
            get { return _recipe.Title; }
        }

        public string DifficultyDescription
        {
            get { return _recipe.DifficultyDescription; }
        }

        public List<RecipeStep> RecipeSteps
        {
            get { return _recipe.RecipeSteps.ToList(); }
        }

        public List<RecipeGrocery> RecipeGroceries
        {
            get { return _recipe.RecipeGroceries.ToList(); }
        }
    }
}
