
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Net.Http;
using Windows.Devices.Geolocation;
using Newtonsoft.Json;
using System.ComponentModel;
using WeatherFrog.Model;


namespace WeatherFrog
{
    public class ForecastService
    {

        public static string apiKey = "622ceeb4a3ea05bb77fb208773cd945f";

        HttpClient client = new HttpClient();

        private readonly static ForecastService instance = new ForecastService();

        FlickrService flickrService = FlickrService.getInstance();

        private ForecastService() {
         
        }
     
        public static ForecastService getInstance()
        {
            return instance;
        }
       
        public async void getForecast(Station station) {
            Debug.WriteLine("gettting Forecast...");
     

            string url = "https://api.forecast.io/forecast/" + apiKey + 
                "/" + station.lat +
                "," + station.lon;


            string forecastResult = await client.GetStringAsync(url);
            Debug.WriteLine("forecast response: " + forecastResult);
        
         ForecastData data = JsonConvert.DeserializeObject<ForecastData>(removeFlags(forecastResult));

         station.forecast = data;
         flickrService.getImgUrl(station);
            
			
        }

      
        
        private string removeFlags(string response){
            string s  = response.Substring(0,response.IndexOf(",\"flags\":"));
            s += "}";
            Debug.WriteLine("removeFlags" + s );
            return s;
        
        }


    }


    public class Currently
    {
        public string time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public string precipIntensity { get; set; }
        public string precipProbability { get; set; }
        public string precipType { get; set; }
        public string temperature { get; set; }
        public string apparentTemperature { get; set; }
        public string dewPoint { get; set; }
        public string windSpeed { get; set; }
        public string windBearing { get; set; }
        public string cloudCover { get; set; }
        public string humidity { get; set; }
        public string pressure { get; set; }
        public string visibility { get; set; }
        public string ozone { get; set; }
    }

    public class Datum
    {
        public string time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public string precipIntensity { get; set; }
        public string precipProbability { get; set; }
        public string precipType { get; set; }
        public string temperature { get; set; }
        public string apparentTemperature { get; set; }
        public string dewPoint { get; set; }
        public string windSpeed { get; set; }
        public string windBearing { get; set; }
        public string cloudCover { get; set; }
        public string humidity { get; set; }
        public string pressure { get; set; }
        public string visibility { get; set; }
        public string ozone { get; set; }
    }

    public class Hourly
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<Datum> data { get; set; }
    }

    public class Datum2
    {
        public string time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public int sunriseTime { get; set; }
        public int sunsetTime { get; set; }
        public string precipIntensity { get; set; }
        public string precipIntensityMax { get; set; }
        public string precipIntensityMaxTime { get; set; }
        public string precipProbability { get; set; }
        public string precipType { get; set; }
        public string temperatureMin { get; set; }
        public string temperatureMinTime { get; set; }
        public string temperatureMax { get; set; }
        public string temperatureMaxTime { get; set; }
        public string apparentTemperatureMin { get; set; }
        public string apparentTemperatureMinTime { get; set; }
        public string apparentTemperatureMax { get; set; }
        public string apparentTemperatureMaxTime { get; set; }
        public string dewPoint { get; set; }
        public string windSpeed { get; set; }
        public string windBearing { get; set; }
        public string cloudCover { get; set; }
        public string humidity { get; set; }
        public string pressure { get; set; }
        public string visibility { get; set; }
        public string ozone { get; set; }
    }

    public class Daily
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<Datum2> data { get; set; }
    }

    public class ForecastData
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string timezone { get; set; }
        public string offset { get; set; }
        public Currently currently { get; set; }
        public Hourly hourly { get; set; }
        public Daily daily { get; set; }
    }
    
}
