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
      
        private GooglePlacesService googlePlacesService;
        ForecastService forecastService = ForecastService.getInstance();
        ViewModel vm = ViewModel.getInstance();
        public NewStationChooser()
        {
            InitializeComponent();
           
            googlePlacesService = GooglePlacesService.getInstance();
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
           p = (((ListBox)sender).SelectedItem) as Prediction;
            googlePlacesService.getDetail(p.reference);
            googlePlacesService.gotPlaceDetail += new GooglePlacesService.GotPlaceDetail(addStation);
   
            
        }

        private void addStation(string lat, string lon, string sanityReference)
        {
            if (p.reference != sanityReference)
                return;

            Station station = new Station();
            station.lat = lat;
            station.lon = lon;
           
            station.name = p.terms.First<Term>().value;
            vm.stations.Add(station);
            forecastService.getForecast(station);
            NavigationService.Navigate(new Uri("/View/MainPage.xaml", UriKind.Relative));

        }

       
    }
}