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
using System.Diagnostics;
using WeatherFrog.Model;
using WeatherFrog.ViewModelNamespace;


namespace WeatherFrog.View
{
    public partial class NewStationChooser : PhoneApplicationPage
    {
        public ObservableCollection<Prediction> locations { get; set; }

        private GooglePlacesService googlePlacesService = GooglePlacesService.getInstance();
        ForecastService forecastService = ForecastService.getInstance();
        ViewModel vm = ViewModel.getInstance();
        
        public NewStationChooser()
        {
            InitializeComponent();
            googlePlacesService.gotPlaceDetail -= new GooglePlacesService.GotPlaceDetail(addStation);
            googlePlacesService.gotPlaceDetail += new GooglePlacesService.GotPlaceDetail(addStation);
   
           
            locations = new ObservableCollection<Prediction>();
            locations.Add(new Prediction());
            this.DataContext = this;
        }

        private void textChanged(object sender, TextChangedEventArgs e)
        {
            googlePlacesService.foundPrediction += new GooglePlacesService.FoundPrediction(updatePredictions);
            googlePlacesService.getPrediction(inputText.Text);
        }

        private void updatePredictions(List<Prediction> predictions)
        {
      
            locations.Clear();
            foreach(Prediction p in predictions){
                locations.Add(p);
            }
        }
        Prediction p;
        private void locationSelected(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("locatonSelected");
             p = (((LongListSelector)sender).SelectedItem) as Prediction;
             if (p == null)
                 return;
            googlePlacesService.getDetail(p.reference);
          
            
        }

        private void addStation(string lat, string lon, string name,string sanityReference)
        {
            Debug.WriteLine("adding Station");
            if (p.reference != sanityReference) {
                Debug.WriteLine("sanitize");
                return;
            }
          
            Station station = new Station();
            station.lat = lat;
            station.lon = lon;

            station.name = name;
            vm.stations.Add(station);
            forecastService.getForecast(station);
            NavigationService.Navigate(new Uri("/View/MainPage.xaml", UriKind.Relative));

        }

       
    }
}