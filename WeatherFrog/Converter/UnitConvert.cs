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
    class UnitConvert
    {
    }
    public class DegreesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture){
            if (!(bool)IsolatedStorageSettings.ApplicationSettings["metricSetting"])
                return value + "°F";
            return  String.Format("{0:0.00}", (Double.Parse(value.ToString())-32)/1.81) + "°C";
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
                return value +"mp/h";
            return Double.Parse(value.ToString()) / 1.6 + "km/h";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }


    }
}
