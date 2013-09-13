using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using WeatherFrog.Model;

namespace WeatherFrog
{
    public class TimeTicker : INotifyPropertyChanged
    {
        public TimeTicker()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, object e)
        {
          
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("localTime1"));
        }

        public string Now
        {
            get { return string.Format("{0:HH:mm:ss tt}", DateTime.Now); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
