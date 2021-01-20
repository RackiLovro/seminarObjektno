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
    class GroceriesSearchViewModel : INotifyPropertyChanged
    {
        private readonly List<GroceryCheckBoxViewModel> _groceries;
        private readonly List<Grocery> _includeGroceries;
        private ObservableCollection<Recipe> _recipes;
        private readonly DesktopTourContext _context = new DesktopTourContext();

        public GroceriesSearchViewModel()
        {
            _groceries = new List<GroceryCheckBoxViewModel>();
            _includeGroceries = new List<Grocery>();
            _context.Groceries.ToList().ForEach(g => _groceries.Add(new GroceryCheckBoxViewModel(g, false, this)));
            Recipes = new ObservableCollection<Recipe>(_context.Recipes.ToList());

        }
        private void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public List<GroceryCheckBoxViewModel> Groceries
        {
            get { return _groceries; }
        }

        public Grocery IncludeGrocery
        {
            get { return _includeGroceries[0]; }
            set
            {
                if (_includeGroceries.Contains(value))
                {
                    var grocery = _includeGroceries.Single(g => g.Id == value.Id);
                    _includeGroceries.Remove(grocery);

                    if (!_includeGroceries.Any())
                    {
                        Recipes = new ObservableCollection<Recipe>(_context.Recipes.ToList());
                    }
                    else
                    {
                        var recipes = from r in _context.Recipes
                                      join rg in _context.RecipeGroceries on r.Id equals rg.RecipeId
                                      join g in _context.Groceries on rg.GroceryId equals g.Id
                                      where _includeGroceries.Contains(g)
                                      select new Recipe
                                      {
                                          Id = r.Id,
                                          Title = r.Title,
                                          DiffLevelId = r.DiffLevelId
                                      };

                        Recipes = new ObservableCollection<Recipe>(recipes.ToList());
                    }
                }
                else
                {
                    _includeGroceries.Add(value);

                    var recipes = from r in _context.Recipes
                                  join rg in _context.RecipeGroceries on r.Id equals rg.RecipeId
                                  join g in _context.Groceries on rg.GroceryId equals g.Id
                                  where _includeGroceries.Contains(g)
                                  select new Recipe
                                  {
                                      Id = r.Id,
                                      Title = r.Title,
                                      DiffLevelId = r.DiffLevelId
                                  };

                    Recipes = new ObservableCollection<Recipe>(recipes.ToList());
                }
            }
        }

        public ObservableCollection<Recipe> Recipes
        {
            get { return _recipes; }
            set
            {
                _recipes = value;
                _recipes.ToList().ForEach(r => r.DiffLevel = _context.RecipeDifficulties.Where(rd => rd.DiffLevel == r.DiffLevelId).Single());
                OnPropertyChanged("Recipes");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
