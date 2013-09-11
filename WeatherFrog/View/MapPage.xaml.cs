using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Info;
using System.IO.IsolatedStorage;
using System.IO;
using System.Diagnostics;

namespace WeatherFrog.View
{
    public partial class MapPage : PhoneApplicationPage
    {
        public MapPage()
        {
            InitializeComponent();
        }
        

        private void browser_ScriptNotify(object sender, NotifyEventArgs e)
        {
            Debug.WriteLine("scriptnotify");
    
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var response = new object[]
                       {
                           482083,
                           163731,
                       };
            browser.InvokeScript("setStats", response.Select(c => c.ToString()).ToArray());

        }

 
        private void ToggleSwitch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RainToggleClick(object sender, RoutedEventArgs e)
        {
            var response = new object[]
                       {
                           rainToggle.IsChecked
                       };
            browser.InvokeScript("setLayer");
        }
    }
}