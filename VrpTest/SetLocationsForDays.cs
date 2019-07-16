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
            
            //List<Location> locations = new List<Location>();

            //for (int i = 0; i < LocationDB.Locations.Count; i++)
            //{
            //    locations.Add(LocationDB.Locations.ElementAt(i));
            //}
            //period.Days.ElementAt(0).SetDay(locations);

            //List<Location> locations1 = new List<Location>();

            //for (int i = 0; i < LocationDB.Locations.Count / 2; i++)
            //{
            //    locations1.Add(LocationDB.Locations.ElementAt(i));
            //}
            //period.Days.ElementAt(1).SetDay(locations1);

            List<Location> locations2 = new List<Location>();

            for (int i = 16; i < LocationDB.Locations.Count; i++)
            {
                locations2.Add(LocationDB.Locations.ElementAt(i));
            }
            period.Days.ElementAt(0).SetDay(locations2);
        }

    }

}