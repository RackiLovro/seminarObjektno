using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopTour.ViewModel
{
    class SearchViewModel
    {
        private readonly List<object> _tabViewModels;

        public SearchViewModel()
        {
            _tabViewModels = new List<object>();

            _tabViewModels.Add(new GroceriesSearchViewModel());
            _tabViewModels.Add(new TitleSearchViewModel());
            _tabViewModels.Add(new UserSearchViewModel());
        }

        public object GroceriesSearch
        {
            get { return _tabViewModels[0]; }
        }
        public object TitleSearch
        {
            get { return _tabViewModels[1]; }
        }

        public object UserSearch
        {
            get { return _tabViewModels[2]; }
        }
    }
}
