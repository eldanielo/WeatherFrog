using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Windows.Media;

namespace WeatherFrog.Model
{
    public class Station : INotifyPropertyChanged
    {
        private ConverterService converter = ConverterService.getInstance();
        private static int count = 0;
        public string id { get; private set; }
        public Station() {
            id = "" + count++;
            locSetting = "exact";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }




        private String _icon;
        public String icon
        {
            get { return _icon; }
            set
            {
                if (value != _icon)
                {
                    _icon = "Resources/Icons/"+value;
                    //this.PropertyChanged(this, new PropertyChangedEventArgs("icon"));
                }
            }
        }
        

        public String locSetting { get; set; }
        public String WoeId { get; set; }
        private String _lat;
        public String lat
        {
            get { return _lat; }
            set
            {
                if (value != _lat)
                {
                    _lat = value;
                   // this.PropertyChanged(this, new PropertyChangedEventArgs("lat"));
                }
            }
        }
        private String _lon;
        public String lon
        {
            get { return _lon; }
            set
            {
                if (value != _lon)
                {
                    _lon = value;
              //      this.PropertyChanged(this, new PropertyChangedEventArgs("lon"));
                }
            }
        }
        private String _name;
        public String name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    //this.PropertyChanged(this, new PropertyChangedEventArgs("name"));
                }
            }
        }
        public String woid { get; set; }

        private ForecastData _forecast;
        public ForecastData forecast
        {
            get { return _forecast; }
            set
            {
                if (value != _forecast)
                {
                    _forecast = value;
                    icon = converter.iconConvert(forecast.currently.icon);
                    Debug.WriteLine("ICON" + icon);
                    this.RaisePropertyChanged("forecast");
                }
            }
        }
       
        private ImageBrush _backDropImg;
        public ImageBrush backDropImg
        {
            get { return _backDropImg; }
            set
            {
                if (value != _backDropImg)
                {
                    _backDropImg = value;
                    this.RaisePropertyChanged("backDropImg");
                }
            }
        }
        private String _picUrl;
        public String picUrl
        {
            get { return _picUrl; }
            set
            {
                if (value != _picUrl)
                {
                    _picUrl = value;
                    this.RaisePropertyChanged("picUrl");
                }
            }
        }

        public String ToString() {
            return name + "  " + lat + " " + lon;
        }
        public Station GetCopy()
        {
            Station copy = (Station)this.MemberwiseClone();
            return copy;
        }

        
    }
}
