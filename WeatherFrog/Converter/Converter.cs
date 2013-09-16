using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherFrog.Converter
{

    public class IconConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "/Resources/Icons/" + ConverterService.getInstance().iconConvert(value.ToString());
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }


    }

    public class PrecipConverter : System.Windows.Data.IValueConverter
    {

        ConverterService converterService = ConverterService.getInstance();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<Datum> shortPrecip = new List<Datum>();
            for (int i = 0; i < ((List<Datum>)value).Count; i += 5)
            {
                shortPrecip.Add(((List<Datum>)value)[i]);
            }
            return shortPrecip;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

    public class DegreesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(bool)IsolatedStorageSettings.ApplicationSettings["metricSetting"])
                return value + "°F";
            return String.Format("{0:0.00}", (Double.Parse(value.ToString()) - 32) / 1.81) + "°C";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }


    }
    public class SpeedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(bool)IsolatedStorageSettings.ApplicationSettings["metricSetting"])
                return value + "mp/h";
            return Double.Parse(value.ToString()) / 1.6 + "km/h";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }


    }

    public class UnixToDayConvert : System.Windows.Data.IValueConverter
    {

        ConverterService converterService = ConverterService.getInstance();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (converterService.unixToDate(double.Parse(value.ToString())).Date == DateTime.Now.Date)
            {
                return "Today";
            }
            return converterService.unixToDate(double.Parse(value.ToString())).DayOfWeek;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

    public class UnixToTime : System.Windows.Data.IValueConverter
    {

        ConverterService converterService = ConverterService.getInstance();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           int hour = converterService.unixToDate(double.Parse(value.ToString())).Hour ;
           if (converterService.unixToDate(double.Parse(value.ToString())).Hour == DateTime.Now.Hour)
               return "now";
            if((bool)IsolatedStorageSettings.ApplicationSettings["metricSetting"])
                return hour + ":00";
            else {
                if (hour > 12)
                {
                    return hour - 12 + " pm";
                }
                else {
                    return hour + " am";
                }
               }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

    public class DoubleToPercent : System.Windows.Data.IValueConverter
    {

        ConverterService converterService = ConverterService.getInstance();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //@TODO
            return value;
           // return Double.Parse(value.ToString()) * 100 + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

    public class LocalTimeConvert : System.Windows.Data.IValueConverter
    {

        ConverterService converterService = ConverterService.getInstance();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime time = (DateTime)value;
            if ((bool)IsolatedStorageSettings.ApplicationSettings["metricSetting"])
            {
                return time.Hour + ":" + time.Minute;
            }
            else {
                return converterService.timeToImperial(time);
            }

           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }
}
