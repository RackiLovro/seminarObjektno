﻿using System;
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
using System.Windows.Shapes;
using DesktopTour.Models;

namespace DesktopTour.View
{
    /// <summary>
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class Homepage : Window
    {
        public Homepage()
        {
            InitializeComponent();
            lovroide();
        }

        public void lovroide()
        {
            var lovro = new DesktopTourContext();

            var luka = lovro.Recipes.ToList().Select(e => e.Title);

            var luka1 = "lovro";
        }
    }
}
