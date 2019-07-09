using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest
{
    public class Day
    {
        public List<Vehicle> Vehicles { get; set; }
        public List<string> Addresses { get; set; }
        public long[,] TimeWindows { get; set; }
        public int Depot { get; set; }
        public long TotalDur { get; set; }                   //Dur: Duration
        public long MaxDur { get; set; }
        public long MinDur { get; set; }
        public long AvgDur { get; set; }

        public Day(List<string> addresses)
        {
            Vehicles = new List<Vehicle>();
            Addresses = addresses;
        }

        public Day()
        {
            Vehicles = new List<Vehicle>();
        }
    }
}
