
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
using System.IO.IsolatedStorage;


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
            string forecastResult =null;
            try
            {
                forecastResult = await client.GetStringAsync(url);
            }
            catch (HttpRequestException ex) {
                Debug.WriteLine(ex.Message);
                return;
            }
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
        
        public List<Datum> data{get;set;}
       
    }

    public class Datum2
    {
        private string _time;
        public string time
        {
            get { return _time; }
            set
            {
                if (value != _time)
                {
                    _time = value;
                    dayOfWeek = ConverterService.getInstance().unixToDate(double.Parse(value.ToString())).DayOfWeek.ToString().Substring(0,2);
        
                }
            }
        }

        public string dayOfWeek { get; set; }

        public string summary { get; set; }
        public string icon { get; set; }
        public int sunriseTime { get; set; }
        public int sunsetTime { get; set; }
        public string precipIntensity { get; set; }
        public string precipIntensityMax { get; set; }
        public string precipIntensityMaxTime { get; set; }
        public string precipProbability { get; set; }
        public string precipType { get; set; }


        private string _temperatureMin;
        public string temperatureMin
        {
            get { return _temperatureMin; }
            set
            {
                if (value != _temperatureMin)
                {
                    _temperatureMin = value;
                    //@TODO
                 //   temperatureMinCelsius = String.Format("{0:0.00}", (Double.Parse(value.ToString()) - 32) / 1.8) + "°C";
                   
                }
            }
        }

        public string temperatureMinCelsius { get; set; }

       
        public string temperatureMinTime { get; set; }


        private string _temperatureMax;
        public string temperatureMax
        {
            get { return _temperatureMax; }
            set
            {
                if (value != _temperatureMax)
                {
                    _temperatureMax = value;
                   // temperatureMaxCelsius = String.Format("{0:0.00}", (Double.Parse(value.ToString()) - 32) / 1.8) + "°C";

                }
            }
        }
        public string temperatureMaxCelsius;

        
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
        public double offset { get; set; }
        public Currently currently { get; set; }
        public Hourly hourly { get; set; }
        public Daily daily { get; set; }
    }
    
}
