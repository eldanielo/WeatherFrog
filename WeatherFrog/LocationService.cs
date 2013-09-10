
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Net.Http;
using Windows.Devices.Geolocation;
using WeatherFrog.Model;


namespace WeatherFrog
{
    class LocationService
    {

        private Geolocator geolocator = new Geolocator();
        private Geoposition position;
        
        private readonly static LocationService instance = new LocationService();
        
        private LocationService() {
            geolocator.DesiredAccuracyInMeters = 100;

        }

        public static LocationService getInstance()
        {
            return instance;
        }

        public delegate void LocationComplete(Geoposition position, Station station);
        public event LocationComplete loctionComplete;

        public async void requestLocation(Station station) {
            loctionComplete += new LocationService.LocationComplete(gotLocation);
            Debug.WriteLine("getting Pos...");
            position = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1)
                , TimeSpan.FromSeconds(30));


            Debug.WriteLine("lat: " + position.Coordinate.Latitude + " lon " + position.Coordinate.Longitude);
            loctionComplete(position, station);
        }


        private void gotLocation(Geoposition position, Station station)
        {
            station.lat = position.Coordinate.Latitude.ToString().Replace(",", ".");
            station.lon = position.Coordinate.Longitude.ToString().Replace(",", ".");
            station.name = "Siegendorf";

        }

    





        
    }
  
}
