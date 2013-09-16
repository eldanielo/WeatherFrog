using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading;
using System.Windows.Threading;
using WeatherFrog.Model;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WeatherFrog.View
{
    public partial class sunView : UserControl
    {
        ConverterService converter = ConverterService.getInstance();
        public sunView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.Now > converter.unixToDate(((Station)DataContext).forecast.daily.data.First<Datum2>().sunsetTime)) {
                var uriSource = new Uri(@"/WeatherFrog;component/Resources/Icons/moon.png", UriKind.Relative);
                image.Source = new BitmapImage(uriSource);

            }
            sunStoryboard.Begin();
            DispatcherTimer timer = new DispatcherTimer();

            Debug.WriteLine("sunpos :" + getRelativeSunPos() + " animtime  : " + getRelativeSunPos() * 5.8);
            timer.Interval = TimeSpan.FromSeconds(getRelativeSunPos()*5.8);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ((DispatcherTimer)sender).Stop();
            sunStoryboard.Pause();
        }
        public double getRelativeSunPos() {
            Station current = ((Station)DataContext);
           
            double sunriseTime = converter.unixToDate(current.forecast.daily.data.First<Datum2>().sunriseTime).Hour;
            double sunset = converter.unixToDate(current.forecast.daily.data.First<Datum2>().sunsetTime).Hour;
            return current.localTime.Hour / (sunriseTime + sunset);
        }
    }
}
