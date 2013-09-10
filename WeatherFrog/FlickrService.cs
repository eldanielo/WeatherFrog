
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.Globalization;
using WeatherFrog.Model;
using System.Net;
using System.Windows.Resources;
using System.Windows;


namespace WeatherFrog
{
    class FlickrService
    {
        private ConverterService converter = ConverterService.getInstance();
        private ImageService imageService = ImageService.getInstance();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static string apiKey = "368539382a7037e88f69eaa1523fb059";
        public static string apiSecret = "4c839a8ecdcdf8ac";
        HttpClient client = new HttpClient();
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
       


        private readonly static FlickrService instance = new FlickrService();

       

        public static FlickrService getInstance()
        {
            return instance;
        }


        public async void getImgUrl(Station station) {
         
            Debug.WriteLine("flickrgetimg loc  " +settings["locSetting"]);
            string baseRequest = "http://api.flickr.com/services/rest" +
                "?method=flickr.photos.search" +
                "&api_key=" + apiKey +
                "&group_id=1463451%40N25" +
                   "&format=json" +
                "&nojsoncallback=1"+
                "&per_page=500"+
                "&extras=date_taken";
            if(((string)settings["locSetting"])=="exact"){
                if (station.locSetting == "approx") {
                    if (station.woid == null)
                    {
                        string response = await getWoeid(station);
                        Debug.WriteLine("approxresponse " + response);
                        PlaceData x = JsonConvert.DeserializeObject<PlaceData>(response);
                        station.woid = x.places.place.First<Place>().woeid;
                    }
                    baseRequest += "&woeid=" + station.woid;
                }
                else if (station.locSetting == "exact")
                {
                    baseRequest += "&lat=" + station.lat + "&lon=" + station.lon + "&radius=32";
                }
                

            }
            else if ((string)settings["locSetting"] == "approx" || station.locSetting=="approx")
            {
                if (station.locSetting == "exact" || station.locSetting == "approx")
                {
                    if (station.woid == null)
                    {
                        string response = await getWoeid(station);
                        Debug.WriteLine("approxresponse " + response);
                        PlaceData x = JsonConvert.DeserializeObject<PlaceData>(response);
                        station.woid = x.places.place.First<Place>().woeid;
                    }
                    baseRequest += "&woeid=" + station.woid;
                }
          
            }
            else if ((string)settings["locSetting"] == "worldwide") {
               
            }
            if ((bool)settings["weatherconditionSetting"]) {
                baseRequest += "&tags=" + converter.tagConvert(station.forecast.currently.icon);
            }
            if ((bool)settings["daytimeSetting"]) {
                if (!baseRequest.Contains("&tags="))
                {
                    baseRequest += "&tags=";
                }
                else {
                    baseRequest += ",";
                }
                baseRequest += converter.timeConvert(station.forecast.daily.data.First().sunriseTime, station.forecast.daily.data.First().sunsetTime); 

            }


            
             

            Debug.WriteLine("downloading img..." + station.ToString());

            Debug.WriteLine("flickrRequest " + baseRequest);


            string flickrResult = await client.GetStringAsync(baseRequest);
            Debug.WriteLine("flickr response: " + flickrResult);
            FlickrData data = JsonConvert.DeserializeObject<FlickrData>(flickrResult);
          
            if (data.stat == "ok") {
                if (data.photos.photo.Count == 0) {
                    if (station.locSetting == "exact") {
                        station.locSetting = "approx";
                        Debug.WriteLine("no exact photo");
                    }
                    else if (station.locSetting == "approx") {
                        station.locSetting = "worldwide";
                        Debug.WriteLine("no approx photo");
                    }
                    getImgUrl(station);
                    return;
                }
                List<String> urls = new List<String>();
                 foreach(Photo p in data.photos.photo){
                     if (!checkSeason(p)) {
                         continue;
                     }
                     string photoUrl = "http://farm{0}.staticflickr.com/{1}/{2}_{3}_b.jpg";
                     string basePhotoUrl = string.Format(photoUrl,
                         p.farm,
                         p.server,
                         p.id,
                         p.secret);
                     urls.Add(basePhotoUrl);
                 
                   
                }
                Random rnd = new Random();
                int r = rnd.Next(urls.Count);
                station.picUrl = (string)urls[0];
                Debug.WriteLine("setting BackDrop");

                ImageBrush imgbrush = new ImageBrush();
                imgbrush.ImageSource = new BitmapImage(new Uri(station.picUrl));
                imgbrush.Stretch = Stretch.UniformToFill;
                station.backDropImg = imgbrush;
                updateBg();
              //   gotImgURL((string)urls[r], station);
              //  imageService.DownloadImagefromServer((string)urls[r], station);
                   
            
            }

        }

        private bool checkSeason(Photo p) {
            return converter.monthToSeason(DateTime.Now.Month) == converter.monthToSeason(p.datetaken.Month); 
        }

        private async Task<String> getWoeid(Station station){
            string request = "  http://api.flickr.com/services/rest/?method=flickr.places.findByLatLon" +
                   "&api_key=" + apiKey +
                   "&lat=" + station.lat+
                   "&lon="+station.lon + 
                   "&accuracy="+10+
                   "&format=json&nojsoncallback=1";
            
            String flickrResult = await client.GetStringAsync(request);
            Debug.WriteLine("getWOEID flickr response: " + flickrResult);
            return flickrResult;
            

        }

        public async void findPlace(String query){
            Debug.WriteLine("finding place ... " + query);

            string request = "  http://api.flickr.com/services/rest/?method=flickr.places.find" +
                "&api_key="+ apiKey +
                "&query=" + query +
                "&format=json&nojsoncallback=1";

            string flickrResult = await client.GetStringAsync(request);
            Debug.WriteLine("flickr response: " + flickrResult);

         
        }
        public delegate void UpdateBg();

        public event UpdateBg updateBg;

        private void  setBackDropUrl(string url, Station station)
        {

     
            Debug.WriteLine("setting BackDrop");

            ImageBrush imgbrush = new ImageBrush();
            imgbrush.ImageSource = new BitmapImage(new Uri(url));
            imgbrush.Stretch = Stretch.UniformToFill;
            station.backDropImg = imgbrush;
            updateBg();

            
        }


    


   }
    public class Place
    {
        public string place_id { get; set; }
        public string woeid { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string place_url { get; set; }
        public string place_type { get; set; }
        public string place_type_id { get; set; }
        public string timezone { get; set; }
        public string name { get; set; }
        public string woe_name { get; set; }
    }

    public class Places
    {
        public List<Place> place { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string accuracy { get; set; }
        public int total { get; set; }
    }

    public class PlaceData
    {
        public Places places { get; set; }
        public string stat { get; set; }
    }
    /*
    public class Place
    {
        public string place_id { get; set; }
        public string woeid { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string place_url { get; set; }
        public string place_type { get; set; }
        public string place_type_id { get; set; }
        public string timezone { get; set; }
        public string _content { get; set; }
        public string woe_name { get; set; }
    }

    public class Places
    {
        public List<Place> place { get; set; }
        public string query { get; set; }
        public int total { get; set; }
    }

    public class PlaceData
    {
        public Places places { get; set; }
        public string stat { get; set; }
    }

    */

    public class Photo
    {
        public string id { get; set; }
        public string owner { get; set; }
        public string secret { get; set; }
        public string server { get; set; }
        public int farm { get; set; }
        public string title { get; set; }
        public int ispublic { get; set; }
        public int isfriend { get; set; }
        public int isfamily { get; set; }
        public DateTime datetaken { get; set; }
    }

    public class Photos
    {
        public int page { get; set; }
        public int pages { get; set; }
        public int perpage { get; set; }
        public string total { get; set; }
        public List<Photo> photo { get; set; }
    }

    public class FlickrData
    {
        public FlickrData() { }
        public Photos photos { get; set; }
        public string stat { get; set; }
    }
  
}
