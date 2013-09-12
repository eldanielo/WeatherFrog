using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;
using WeatherFrog.ViewModelNamespace;
using WeatherFrog.Model;
using System.IO.IsolatedStorage;

namespace WeatherFrog.View
{
    public partial class MapView : UserControl
    {
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        ViewModel vm;
        public MapView()
        {
            InitializeComponent();
          //  vm = ViewModel.getInstance();
           
          //  this.Loaded += new RoutedEventHandler(MapView_Loaded);
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
        }



     

        private void LayoutRoot_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/View/MapPage.xaml", UriKind.Relative));
        }

  
        private void browser_LoadCompleted(object sender, NavigationEventArgs e)
        {
          //  Debug.WriteLine("browser loaded complete");
        //    Station curr = ViewModelNamespace.ViewModel.getInstance().currentStation;
     //       browser.InvokeScript("setPos", new string[] { curr.lat, curr.lon });


        }

    }
}
