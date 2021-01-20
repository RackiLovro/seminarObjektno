using DesktopTour.Commands;
using DesktopTour.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopTour.ViewModel
{
    class UserSearchViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private ObservableCollection<Recipe> _recipes;
        private readonly DesktopTourContext _context = new DesktopTourContext();

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        private ICommand _userSearch;
        public ICommand UserSearch
        {
            get
            {
                if (_userSearch == null) _userSearch = new RelayCommand(o =>
                {
                    var username = o as string;

                    var recipes = from r in _context.Recipes
                                  join ur in _context.UserRecipes on r.Id equals ur.RecipeId
                                  where ur.User.Nickname.ToLower().Contains(username.ToLower())
                                  select new Recipe
                                  {
                                      Id = r.Id,
                                      Title = r.Title,
                                      DiffLevelId = r.DiffLevelId
                                  };

                    Recipes = new ObservableCollection<Recipe>(recipes.ToList());

                },
                    o => true
                    );
                return _userSearch;
            }
            set
            {
                _userSearch = value;
            }
        }
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
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
    }
}
