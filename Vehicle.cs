using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest
{
    public class Vehicle
    {
        public List<int> Route { get; set; }
        public long routeDistance { get; set; }

        public Vehicle()
        {
            Route = new List<int>();
        }
    }
}
