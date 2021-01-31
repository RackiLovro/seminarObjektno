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

                        var ingredientsWeHave = new List<string>();
                        foreach (var ing in _includeGroceries)
                        {
                            var id = ing.Id;
                            ingredientsWeHave.Add(_context.Groceries.Find(id).Name);
                        }
                        Dictionary<int, List<string>> recipesGroceriesMap = new Dictionary<int, List<string>>();
                        var availableRecipes = new List<int>();
                        // Fill recipe IDs
                        foreach (var recipe in _context.Recipes.ToList())
                        {
                            recipesGroceriesMap.Add(recipe.Id, new List<string>());
                        }

                        // Fill groceries
                        foreach (var recipeGrocery in _context.RecipeGroceries.ToList())
                        {
                            var groceryName = (from rg in _context.RecipeGroceries join g in _context.Groceries on recipeGrocery.GroceryId equals g.Id select g.Name).FirstOrDefault();
                            recipesGroceriesMap[recipeGrocery.RecipeId].Add(groceryName);
                        }

                        foreach (var entry in recipesGroceriesMap)
                        {
                            bool enough = !entry.Value.Except(ingredientsWeHave).Any();
                            if (enough)
                            {
                                availableRecipes.Add(entry.Key);
                            }
                        }

                        var reps = from r in _context.Recipes
                                   where availableRecipes.Contains(r.Id)
                                   join ur in _context.UserRecipes on r.Id equals ur.RecipeId
                                   select r;

                        Recipes = new ObservableCollection<Recipe>(reps.Distinct().ToList());
                    }
                }
                else
                {
                    _includeGroceries.Add(value);

                    var ingredientsWeHave = new List<string>();
                    foreach (var ing in _includeGroceries)
                    {
                        var id = ing.Id;
                        ingredientsWeHave.Add(_context.Groceries.Find(id).Name);
                    }
                    Dictionary<int, List<string>> recipesGroceriesMap = new Dictionary<int, List<string>>();
                    var availableRecipes = new List<int>();
                    // Fill recipe IDs
                    foreach (var recipe in _context.Recipes.ToList())
                    {
                        recipesGroceriesMap.Add(recipe.Id, new List<string>());
                    }

                    // Fill groceries
                    foreach (var recipeGrocery in _context.RecipeGroceries.ToList())
                    {
                        var groceryName = (from rg in _context.RecipeGroceries join g in _context.Groceries on recipeGrocery.GroceryId equals g.Id select g.Name).FirstOrDefault();
                        recipesGroceriesMap[recipeGrocery.RecipeId].Add(groceryName);
                    }

                    foreach (var entry in recipesGroceriesMap)
                    {
                        bool enough = !entry.Value.Except(ingredientsWeHave).Any();
                        if (enough)
                        {
                            availableRecipes.Add(entry.Key);
                        }
                    }

                    var reps = from r in _context.Recipes
                               where availableRecipes.Contains(r.Id)
                               join ur in _context.UserRecipes on r.Id equals ur.RecipeId
                               select r;

                    Recipes = new ObservableCollection<Recipe>(reps.Distinct().ToList());
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

                _recipes.ToList().ForEach(r => r.UserRecipes = _context.UserRecipes.Where(ur => ur.RecipeId == r.Id).ToList());

                _recipes.ToList().ForEach(r => r.UserRecipes.First().User = _context.Users.Where(u => u.Id == r.UserRecipes.First().UserId).Single());

                OnPropertyChanged("Recipes");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
