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
    public partial class ForecastDetailPage : PhoneApplicationPage
    {
        public ForecastDetailPage()
        {
            InitializeComponent();
            this.DataContext = ViewModelNamespace.ViewModel.getInstance();
          
        }
    }
}