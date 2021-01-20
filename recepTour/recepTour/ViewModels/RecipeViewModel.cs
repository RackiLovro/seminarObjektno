using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recepTour.ViewModels
{
    public class RecipeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DiffLevelName { get; set; }
        public string User { get; set; }
    }
}
