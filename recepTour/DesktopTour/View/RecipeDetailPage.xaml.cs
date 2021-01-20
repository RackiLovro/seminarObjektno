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
    /// Interaction logic for RecipeDetailPage.xaml
    /// </summary>
    public partial class RecipeDetailPage : Page
    {
        public RecipeDetailPage()
        {
            InitializeComponent();
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            RecipeFeedPage recipeFeedPage = new RecipeFeedPage();
            RecipeFeedViewModel recipeFeedViewModel = new RecipeFeedViewModel();
            recipeFeedPage.DataContext = recipeFeedViewModel;

            this.NavigationService.Navigate(recipeFeedPage);
        }
    }
}
