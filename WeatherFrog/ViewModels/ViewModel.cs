using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using WeatherFrog.Model;


namespace WeatherFrog.ViewModelNamespace
{
    public class ViewModel
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
            if(stations == null)
                stations = new ObservableCollection<Station>();
        }


        public void GetDefaultAccomplishments()
        {
            ObservableCollection<Station> a = new ObservableCollection<Station>();
          
            // Items to collect
            a.Add(new Station() { lat = "33", lon = "45" });
          
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
    }
}