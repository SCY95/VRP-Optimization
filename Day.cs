using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest
{
    public class Day
    {
        public List<Vehicle> Vehicles { get; set; }

        public Day()
        {
            Vehicles = new List<Vehicle>();
        }
    }
}
