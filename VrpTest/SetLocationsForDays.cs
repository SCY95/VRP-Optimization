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

            //for (int i = 0; i < 16; i++)
            //{
            //    locations.Add(LocationDB.Locations.ElementAt(i));
            //}
            //period.Days.ElementAt(0).SetDay(locations);



            //List<Location> locations1 = new List<Location>();


            //for (int i = 0; i < 16; i++)
            //{
            //    locations1.Add(LocationDB.Locations.ElementAt(i));
            //}

            //period.Days.ElementAt(0).SetDay(locations1);



            //List<Location> locations2 = new List<Location>();

            //for (int i = 16; i < LocationDB.Locations.Count; i++)
            //{
            //    locations2.Add(LocationDB.Locations.ElementAt(i));
            //}
            //period.Days.ElementAt(0).SetDay(locations2);




            

            for(int i = 1; i < 7; i++)
            {
                List<Location> locations = new List<Location>();
                //period.Days.ElementAt(i - 1).Locations
                List<Location> locations1 = new List<Location>();
                //period.Days.ElementAt((i - 1) + 7).Locations
                List<Location> locations2 = new List<Location>();


                locations = LocationDB.Locations.Where(d => d.VisitDay == i).ToList();

                int count = locations.Where(d => d.VisitPeriod == 14).Count();
                int j = 0;

                foreach (var location in locations)
                {
                    if(location.VisitPeriod == 7)
                    {
                        locations1.Add(location);
                        locations2.Add(location);
                    }
                    else if( j < count/2)
                    {
                        locations1.Add(location);
                        j++;
                    }
                    else
                    {
                        locations2.Add(location);
                    }
                }

                period.Days.ElementAt(i - 1).SetDay(locations1);
                period.Days.ElementAt(i - 1).DayNum = i;
                period.Days.ElementAt((i - 1) + 7).SetDay(locations2);
                period.Days.ElementAt((i - 1) + 7).DayNum = i + 7;
                

            }



        }

    }

}