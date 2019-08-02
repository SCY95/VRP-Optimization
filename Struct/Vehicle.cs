using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest.Struct
{
    public class Vehicle
    {
        public List<Location> Route { get; set; }
        public long RouteDur { get; set; }
        public long Capacity { get; set; }


        public Vehicle()
        {
            Route = new List<Location>();

        }
    }
}
