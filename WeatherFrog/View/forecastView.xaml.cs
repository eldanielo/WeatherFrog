﻿using System;
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
    public partial class forecastView : UserControl
    {
        public forecastView()
        {
            InitializeComponent();
            this.DataContext = ViewModelNamespace.ViewModel.getInstance();
            icn.Source = ";";
        }
    }
}
