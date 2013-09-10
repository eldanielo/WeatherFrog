using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace WeatherFrog.View
{
    public partial class Settings : PhoneApplicationPage
    {
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
       
        public Settings()
        {
            InitializeComponent();
            switch ((string)settings["locSetting"])
            {
                case "exact": locExactRadio.IsChecked = true; break;
                case "approx": locApproxRadio.IsChecked = true; break;
                case "worldwide": locWorldwideRadio.IsChecked = true; break;

            }
            seasionCB.IsChecked = (bool)settings["seasonSetting"];
            daytimeCB.IsChecked = (bool)settings["daytimeSetting"];
            weatherconditionCB.IsChecked = (bool)settings["weatherconditionSetting"];

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
           settings["seasonSetting"] =  seasionCB.IsChecked ;
          settings["daytimeSetting"] =   daytimeCB.IsChecked;
           settings["weatherconditionSetting"] = weatherconditionCB.IsChecked ;
           if ((bool)locExactRadio.IsChecked) {
               settings["locSetting"] = "exact";
           }
            if((bool)locApproxRadio.IsChecked){
                settings["locSetting"] = "approx";
            }
            if((bool)locWorldwideRadio.IsChecked){
                settings["locSetting"] = "worldwide";
            }
            
        }
    }
}