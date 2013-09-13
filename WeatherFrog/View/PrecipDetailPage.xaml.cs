using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WeatherFrog.View
{
    public partial class PrecipDetailPage : PhoneApplicationPage
    {
        public PrecipDetailPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ViewModelNamespace.ViewModel.getInstance();
          
        }
    }
}