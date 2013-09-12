using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using WeatherFrog.Model;
using System.ComponentModel;


namespace WeatherFrog.ViewModelNamespace
{
    public class ViewModel:INotifyPropertyChanged

    {
        public ObservableCollection<Station> stations { get; set; }

       
        private readonly static ViewModel instance = new ViewModel();

        private ViewModel()
        {
            
        }

        public static ViewModel getInstance()
        {
            return instance;
        }


        public void GetAccomplishments()
        {
            if (stations == null)
            {
                stations = new ObservableCollection<Station>();
              
            }
            addTestStations();
        }

        public void addTestStations() {
            /*
            Station station = new Station { lat = "48.2083", lon = "16.3731"  ,name="Vienna"};
            stations.Add(station);
            ForecastService.getInstance().getForecast(station);
             * */
        }


    

        public void GetSavedStation()
        {
            ObservableCollection<Station> a = new ObservableCollection<Station>();

            foreach (Object o in IsolatedStorageSettings.ApplicationSettings.Values)
            {
                
                //a.Add((Station)o);
            }

            stations = a;
            //MessageBox.Show("Got accomplishments from storage");
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private Station _currentStation;
        public Station currentStation
        {
            get { return _currentStation; }
            set
            {
                if (value != _currentStation)
                {
                    _currentStation = value;
                    this.RaisePropertyChanged("currentStation");
                }
            }
        }
        

    }
}