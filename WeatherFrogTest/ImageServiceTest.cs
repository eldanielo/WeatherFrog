using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using WeatherFrog;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Diagnostics;
using System.Windows.Threading;
using WeatherFrog.Model;

namespace WeatherFrogTest
{
    [TestClass]
    public class ImageServiceTest
    {
        public delegate void ForecastComplete(ForecastData data, Station station);
        public event ForecastComplete forecastComplete;
        [TestMethod]
        public void ad()
        {
            ForecastService forecastService = ForecastService.getInstance();
            Station s = new Station();
            s.lat = "37";
            s.lon =  "-122";
            forecastService.getForecast(s);

            forecastComplete += new ForecastComplete(gotForecast);
        }

        private void gotForecast(ForecastData data, Station station)
        {
            Debug.WriteLine(data);
        }
      
        [TestMethod]
         public async void savePicTest()
        {/*
            System.Diagnostics.Debug.WriteLine("test");
            BitmapImage im = null;
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                 
                 ImageService s = ImageService.getInstance();
                s.downloadImg("http://farm9.staticflickr.com/8317/8065539051_ca711f5b04_o.jpg");
           
             
            });

         
           // imgService.writeImg("test", new WriteableBitmap(bmp));
          *   */
        }
          

       
    }
}
