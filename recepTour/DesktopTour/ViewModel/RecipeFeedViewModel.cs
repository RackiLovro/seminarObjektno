﻿using DesktopTour.Models;
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
        private readonly DesktopTourContext _context;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public RecipeFeedViewModel()
        {
            _context = new DesktopTourContext();
            _recipes = _context.Recipes.ToList();
            _recipes.ForEach(r => r.DiffLevel = _context.RecipeDifficulties.Where(rd => rd.DiffLevel == r.DiffLevelId).Single());
        }
        public List<Recipe> Recipes
        {
            get { return _recipes; }
        }
    }
}
