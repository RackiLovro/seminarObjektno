using DesktopTour.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopTour.View
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            SearchRecipesPage searchRecipesPage = new SearchRecipesPage();
            SearchViewModel searchViewModel = new SearchViewModel();
            searchRecipesPage.DataContext = searchViewModel;
            this.NavigationService.Navigate(searchRecipesPage);
        }

        private void Feed(object sender, RoutedEventArgs e)
        {
            RecipeFeedPage recipeFeedPage = new RecipeFeedPage();
            RecipeFeedViewModel recipeFeedViewModel = new RecipeFeedViewModel();
            recipeFeedPage.DataContext = recipeFeedViewModel;
            this.NavigationService.Navigate(recipeFeedPage);
        }
    }
}
