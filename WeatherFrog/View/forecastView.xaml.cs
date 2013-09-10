using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Data;
using System.Globalization;

namespace WeatherFrog.View
{
    public partial class forecastView : UserControl
    {
        public forecastView()
        {
            InitializeComponent();
            this.DataContext = ViewModelNamespace.ViewModel.getInstance();
            
        }
    }

    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConverterService.getInstance().unixToDate(double.Parse(value.ToString())).DayOfWeek;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "bla";
        }
    }
    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "/Resources/Icons/" + ConverterService.getInstance().iconConvert(value.ToString());
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "bla";
        }

       
    }
}
