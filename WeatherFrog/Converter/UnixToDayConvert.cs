using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherFrog.Converter
{
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
}
