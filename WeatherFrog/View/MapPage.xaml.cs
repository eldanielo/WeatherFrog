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
using WeatherFrog.Model;

namespace WeatherFrog.View
{
    public partial class MapPage : PhoneApplicationPage
    {
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
       
        public MapPage()
        {
            InitializeComponent();
             tempToggle.IsChecked = (bool)settings["tempLayer"];
               windToggle.IsChecked = (bool)settings["windLayer"];
                pressureToggle.IsChecked = (bool)settings["pressureLayer"];
                pressureToggle.IsChecked = (bool)settings["precipitationLayer"];
               stationsToggle.IsChecked = (bool)settings["stationsLayer"];
               cloudToggle.IsChecked = (bool)settings["cloudLayer"];
            this.Loaded += new RoutedEventHandler(MapPage_Loaded);   
        }
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            settings["tempLayer"] = tempToggle.IsChecked;
            settings["windLayer"] = windToggle.IsChecked;
            settings["pressureLayer"] = pressureToggle.IsChecked;
         settings["precipitationLayer"] =   pressureToggle.IsChecked ;
           settings["stationsLayer"] =stationsToggle.IsChecked ;
          settings["cloudLayer"] = cloudToggle.IsChecked;
        }

        private void MapPage_Loaded(object sender, RoutedEventArgs e)
        {
           
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
            browser.InvokeScript("setPos", new string[] { "31.2000", "121.5000" });

        }

 
        private void ToggleSwitch_Click(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
          
        }

      

        private void windToogleClick(object sender, RoutedEventArgs e)
        {
            if ((bool)windToggle.IsChecked)
                browser.InvokeScript("toggleWindOn");
            else
                browser.InvokeScript("toggleWindOff");
        }

        private void pressureToggleClick(object sender, RoutedEventArgs e)
        {
            if ((bool)pressureToggle.IsChecked)
                browser.InvokeScript("togglePressureOn");
            else
                browser.InvokeScript("togglePressureOff");
        }

        private void cloudToggleClick(object sender, RoutedEventArgs e)
        {
            if ((bool)cloudToggle.IsChecked)
                browser.InvokeScript("toggleCloudOn");
            else
                browser.InvokeScript("toggleCloudOff");
        }

        private void tempToggleClick(object sender, RoutedEventArgs e)
        {
            if ((bool)tempToggle.IsChecked)
                browser.InvokeScript("toggleTempOn");
            else
                browser.InvokeScript("toggleTempOff");
        }

        private void stationsToggleClick(object sender, RoutedEventArgs e)
        {
            if ((bool)stationsToggle.IsChecked)
                browser.InvokeScript("toggleStationsOn");
            else
                browser.InvokeScript("toggleStationsOff");
        }

        private void precipitationToggleClick(object sender, RoutedEventArgs e)
        {
            if ((bool)precipitationToggle.IsChecked)
                browser.InvokeScript("togglePrecipitationOn");
            else
                browser.InvokeScript("togglePrecipitationOff");
        }

        private void browser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            Station curr = ViewModelNamespace.ViewModel.getInstance().currentStation;
            browser.InvokeScript("setPos", new string[] { curr.lat, curr.lon });
        }
    }
}