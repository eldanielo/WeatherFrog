using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherFrog
{
    class ConverterService
    {
        private readonly static ConverterService instance = new ConverterService();

        private ConverterService() { }

        public static ConverterService getInstance()
        {
            return instance;
        }
        public String timeConvert(double sunrise, double sunset) {
            int now  =DateTime.Now.Hour;
            if(DateTime.Now > unixToDate(sunset) && DateTime.Now < unixToDate(sunset).AddDays(1)){
                return "night";
            }
            else if(unixToDate(sunrise).Hour-2<=now && unixToDate(sunrise).Hour+2>=now){
                return "sunrise";
            }
            else if (unixToDate(sunset).Hour - 2 <= now && unixToDate(sunset).Hour + 2 >= now)
            {
                return "sunset";
            }
            else {
                return "day";
            }
        }

       

        public DateTime unixToDate(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public String tagConvert(string icon){
            switch (icon) {

                case "clear-day": return "clear" ;
                case "sleet" : return "rain";
                case "partly-cloudy-day":
                case "partly-cloudy-night": return "cloudy";
                case "wind": return "storm";
                default : return icon;
                

            }
        }

        public String iconConvert(String icon) { 
                switch (icon) {

                case "clear-day": return "sunny.png" ;
                case "sleet" : return "haze.png";
                case "rain": return "rain.png";
                case "snow": return "snow.png";
                case "fog": return "haze.png";
                case "cloudy": return "cloudy.png";
                case "partly-cloudy-day": 
                case "partly-cloudy-night": return "partly_cloudy.png";
                case "wind": return "thunderstorms.png";
                default : return icon;
                

            }
        }

        public String monthToSeason(int month) {
            if (month == 12 || month <= 2) {
                return "winter";
            }
            else if (month >= 3 && month <= 5)
            {
                return "spring";
            }
            else if (month >= 6 && month <= 8) {
                return "summer";
            }
            else if (month >= 9 && month <= 11)
            {
                return "autum";
            }
            return "error";
        }
       
    }
}
