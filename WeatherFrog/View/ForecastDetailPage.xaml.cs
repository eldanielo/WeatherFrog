using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;

namespace WeatherFrog.View
{
    public partial class ForecastDetailPage : PhoneApplicationPage
    {
        public ForecastDetailPage()
        {
            InitializeComponent();
          
            
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ViewModelNamespace.ViewModel.getInstance();
           /* if ((bool)IsolatedStorageSettings.ApplicationSettings["metricSetting"])
            {
                maxGraph.ValueMemberPath = "temperatureMaxCelsius";

                minGraph.ValueMemberPath = "temperatureMinCelsius";
            }*/
        }


    }
}