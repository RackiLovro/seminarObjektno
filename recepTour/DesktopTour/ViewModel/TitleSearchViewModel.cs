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
    class TitleSearchViewModel: INotifyPropertyChanged
    {
        private string _searchText;
        private ObservableCollection<Recipe> _recipes;
        private readonly DesktopTourContext _context;
        public TitleSearchViewModel()
        {
            _context = new DesktopTourContext();
            _recipes = new ObservableCollection<Recipe>();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        private ICommand _titleSearch;
        public ICommand TitleSearch
        {
            get
            {
                if (_titleSearch == null) _titleSearch = new RelayCommand(o =>
                {
                var title = o as string;
                Recipes = new ObservableCollection<Recipe>(_context.Recipes.Where(r => r.Title.Contains(title)).ToList());
                },
                    o => true
                    );
                return _titleSearch;
            }
            set
            {
                _titleSearch = value;
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
