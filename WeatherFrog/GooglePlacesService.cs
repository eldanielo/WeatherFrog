
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


namespace WeatherFrog
{
    class GooglePlacesService
    {
        private readonly String apiKey = "AIzaSyAQJWqdpvY0G83UXyoRa9aXot__VBuQkI0";
        
        private readonly static GooglePlacesService instance = new GooglePlacesService();

        private GooglePlacesService()
        {
         
        }

        public static GooglePlacesService getInstance()
        {
            return instance;
        }

        public delegate void FoundPrediction(List<Prediction> predictions);
        public event FoundPrediction foundPrediction;
        public async void getPrediction(String query)
        {
            HttpClient client = new HttpClient();
            Debug.WriteLine("finding place ... " + query);
          
            string request = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=" +
                query +
                "&sensor=false"+
                "&key=" + apiKey;
            Debug.WriteLine("request " + request);
            string googleResult = await client.GetStringAsync(request);
             GoogleData data = JsonConvert.DeserializeObject<GoogleData>(googleResult);
            if (data.status == "OK") {
                foundPrediction(data.predictions);
            }
        else{
            Debug.WriteLine("ERROR finding place");
            }
       

        }
        public delegate void GotPlaceDetail(string lat, string lon, string name, string sanityReference);
        public event GotPlaceDetail gotPlaceDetail;
        public async void getDetail(String reference)
        {
            HttpClient client = new HttpClient();
            Debug.WriteLine("getting place detail ... " + reference);
            string request = "https://maps.googleapis.com/maps/api/place/details/json?reference=" +
                reference +
                "&sensor=false" +
                "&key=" + apiKey;
            Debug.WriteLine("request " + request);
            string googleResult = await client.GetStringAsync(request);
             GoogleDetail data = JsonConvert.DeserializeObject<GoogleDetail>(googleResult);
            if (data.status == "OK")
            {
                gotPlaceDetail(data.result.geometry.location.lat.ToString().Replace(",", "."), data.result.geometry.location.lng.ToString().Replace(",", "."), data.result.address_components.First<AddressComponent>().long_name, reference); 
            }
            else
            {
                Debug.WriteLine("ERROR getting detail");
            }
          
        

        }



     
        
    }

    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Event
    {
        public string event_id { get; set; }
        public int start_time { get; set; }
        public string summary { get; set; }
        public string url { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
    }

    public class Aspect
    {
        public int rating { get; set; }
        public string type { get; set; }
    }

    public class Review
    {
        public List<Aspect> aspects { get; set; }
        public string author_name { get; set; }
        public string author_url { get; set; }
        public string text { get; set; }
        public object time { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public List<Event> events { get; set; }
        public string formatted_address { get; set; }
        public string formatted_phone_number { get; set; }
        public Geometry geometry { get; set; }
        public string icon { get; set; }
        public string id { get; set; }
        public string international_phone_number { get; set; }
        public string name { get; set; }
        public double rating { get; set; }
        public string reference { get; set; }
        public List<Review> reviews { get; set; }
        public List<string> types { get; set; }
        public string url { get; set; }
        public string vicinity { get; set; }
        public string website { get; set; }
    }

    public class GoogleDetail
    {
        public List<object> html_attributions { get; set; }
        public Result result { get; set; }
        public string status { get; set; }
    }

    public class MatchedSubstring
    {
        public int length { get; set; }
        public int offset { get; set; }
    }

    public class Term
    {
        public int offset { get; set; }
        public string value { get; set; }
    }

    public class Prediction
    {
        public string description { get; set; }
        public string id { get; set; }
        public List<MatchedSubstring> matched_substrings { get; set; }
        public string reference { get; set; }
        public List<Term> terms { get; set; }
        public List<string> types { get; set; }
    }

    public class GoogleData
    {
        public List<Prediction> predictions { get; set; }
        public string status { get; set; }
    }
  
}
