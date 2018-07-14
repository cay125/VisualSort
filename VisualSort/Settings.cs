using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSort
{
    static class Settings
    {
        public static int TotalNums = 12;
        public static double Gap = 1;
        public static int TimeSpanMs = 300;
        public static double InitTimeSpan = 0.5;
        public static double TimeSpanS { get { return ((double)(TimeSpanMs)) / 1000; } }        
    }
}
