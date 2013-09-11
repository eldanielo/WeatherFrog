using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WeatherFrog.Resources;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;
using System.IO.IsolatedStorage;
using WeatherFrog.Model;
using WeatherFrog.ViewModelNamespace;

namespace WeatherFrog.View
{
    
    public partial class MainPage : PhoneApplicationPage
    {
     
        FlickrService flickrService;
        LocationService locationService;
        ForecastService forecastService;
        private ViewModel vm = ViewModel.getInstance();

        
         // Constructor
        public MainPage()
        {
        
            InitializeComponent();

            this.DataContext = vm;
            this.mainPivot.ItemsSource = vm.stations;
              this.Loaded += new RoutedEventHandler(MainPage_Loaded);          
            flickrService = FlickrService.getInstance();
            locationService = LocationService.getInstance();
            forecastService = ForecastService.getInstance();
           
            if ((bool)IsolatedStorageSettings.ApplicationSettings["initstart"] == true)
            {

            
                IsolatedStorageSettings.ApplicationSettings["initstart"] = false;
                
            }
            flickrService.updateBg += new FlickrService.UpdateBg(updateBackGround);

        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("mainpage loaded");
          


        }
        private void refreshStation(Station station) {
            
            forecastService.getForecast(station);
         }

        private void locationComplete(Windows.Devices.Geolocation.Geoposition position, Station station)
        {
            station.lat = position.Coordinate.Latitude.ToString().Replace(",", ".");
            station.lon = position.Coordinate.Longitude.ToString().Replace(",", ".");
            station.name = "Siegendorf";
           
            forecastService.getForecast(station);
        }

      

      /* private void ContentPanel_LayoutUpdated(object sender, EventArgs e)
        {
            if (mainScroll.VerticalOffset > 600) {
                TransparncyRect.Opacity = 0.4;
            }
            if (mainScroll.VerticalOffset < 600)
            {
                TransparncyRect.Opacity = 0.0;
            }
            Debug.WriteLine(mainScroll.VerticalOffset);
        }*/

        // Load data for the ViewModel Items
        private void updateBackGround() {
            //FadeCanvas.Opacity = 1;
            /*
            Debug.WriteLine("updating background");
            if (vm.stations.Count != 0)
            {
                ImageBrush imageBrush = new ImageBrush
                {
                 //   ImageSource = ImageService.getInstance().openImage(mvm.Items.ElementAt<Station>(mainPivot.SelectedIndex))
                };
                LayoutRoot.Background = imageBrush;
                    //.backDropImg;
            }
             * */
            fadeIn.Begin();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            vm.GetAccomplishments();


            // There are two different views, but only one view model.
            // So, use LINQ queries to populate the views.

            // Set the data context for the Item view.


          
            
            updateBackGround();
            /* ^1
            foreach (Station s in stationFeed) {
               
                refreshStation(s);
            }*/
          
        }
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
         
        }
     


  
    private void addStationClick(object sender, EventArgs e)
    {
        NavigationService.Navigate(new Uri("/View/NewStationChooser.xaml", UriKind.Relative));

            


    }

    private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        vm.currentStation = vm.stations.ElementAt<Station>(mainPivot.SelectedIndex);
        fadeOut.Begin();

        

    }

    private void fadeOut_Completed(object sender, EventArgs e)
    {
        LayoutRoot.Background = vm.stations.ElementAt<Station>(mainPivot.SelectedIndex).backDropImg;
        fadeIn.Begin();
    }

    private void settingsClick(object sender, EventArgs e)
    {
        NavigationService.Navigate(new Uri("/View/settings.xaml", UriKind.Relative));

            

    }

    
    
     



        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}