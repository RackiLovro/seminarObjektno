﻿using DesktopTour.ViewModel;
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
    /// Interaction logic for RecipeFeedPage.xaml
    /// </summary>
    public partial class RecipeFeedPage : Page
    {
        public RecipeFeedPage()
        {
            InitializeComponent();
        }

        private void Details(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            RecipeDetailPage recipeDetailPage = new RecipeDetailPage();
            recipeDetailPage.DataContext = new RecipeDetailsViewModel(button.Tag);

            this.NavigationService.Navigate(recipeDetailPage);
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            HomePage homePage = new HomePage();

            this.NavigationService.Navigate(homePage);
        }
    }
}
