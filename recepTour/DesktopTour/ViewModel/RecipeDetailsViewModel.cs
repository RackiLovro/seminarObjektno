﻿using DesktopTour.Models;
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
        private readonly DesktopTourContext _context;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public RecipeDetailsViewModel(Recipe recipe)
        {
            _context = new DesktopTourContext();
            _recipe = recipe;
            _recipe.RecipeSteps = _context.RecipeSteps.Where(rs => rs.RecipeId == _recipe.Id).ToList();
            _recipe.RecipeGroceries = _context.RecipeGroceries.Where(rg => rg.RecipeId == recipe.Id).ToList();
            _recipe.RecipeGroceries.ToList().ForEach(rg => rg.Grocery = _context.Groceries.Where(g => g.Id == rg.GroceryId).Single());
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
