﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherFrog
{
    class StateUtilities
    {

        private static Boolean isLaunching;

        public static Boolean IsLaunching
        {
            get { return isLaunching; }
            set { isLaunching = value; }
        }
    }
}
