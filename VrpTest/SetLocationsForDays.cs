using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Google.OrTools.ConstraintSolver;
using System.Linq;
using Newtonsoft.Json;
using Google.Protobuf.WellKnownTypes;//Duration
using System.Diagnostics;
using VrpTest.Struct;

namespace VrpTest
{
    public partial class VrpTest
    {
        public static void SetLocationsForDays(Period period)
        {
            LocationDB locationDB = new LocationDB();
            
            List<Location> locations = new List<Location>();

            for (int i = 0; i < locationDB.Locations.Count; i++)
            {
                locations.Add(locationDB.Locations.ElementAt(i));
            }
            period.Days.ElementAt(0).SetDay(locations);

            List<Location> locations1 = new List<Location>();

            for (int i = 0; i < locationDB.Locations.Count / 2; i++)
            {
                locations1.Add(locationDB.Locations.ElementAt(i));
            }
            period.Days.ElementAt(1).SetDay(locations1);
        }

    }

}