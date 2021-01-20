using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopTour.ViewModel
{
    class GroceryCheckBoxViewModel
    {
        public Grocery Grocery { get; set; }
        public bool IsSelected { get; set; }
        public GroceriesSearchViewModel Parent { get; set; }

        public GroceryCheckBoxViewModel(Grocery grocery, bool isSelected, GroceriesSearchViewModel parent)
        {
            Grocery = grocery;
            IsSelected = isSelected;
            Parent = parent;
        }
        public string Name
        {
            get { return Grocery.Name; }
        }

        public bool IncludeGrocery
        {
            get { return IsSelected; }
            set
            {
                IsSelected = value;
                Parent.IncludeGrocery = Grocery;
            }
        }
    }
}
