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
using System.Collections;
using WeatherFrog.Model;

namespace WeatherFrog.View
{
    public partial class forecastView : UserControl
    {
       
        public forecastView()
        {
            InitializeComponent();
         
            this.DataContext = ViewModelNamespace.ViewModel.getInstance();
            forecastListBox.SelectedItem = null;
            
        }

        

        private void ListboxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IList selectedItems = e.AddedItems;
            Station val = selectedItems.OfType<Station>().FirstOrDefault();


            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/View/ForecastDetailPage.xaml", UriKind.Relative));
            
        }
    }

    public class DateConverter : IValueConverter
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
}
