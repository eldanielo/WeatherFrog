using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WeatherFrog.Model;

namespace WeatherFrog.View
{
    public partial class precipitationView : UserControl
    {
        public precipitationView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(precipitationView_Loaded);
        }

        private void precipitationView_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        
    }
}
