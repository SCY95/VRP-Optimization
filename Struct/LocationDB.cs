using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest.Struct
{
    public class LocationDB
    {
        public List<Location> Locations { get; set; }

        public LocationDB()
        {
            Locations = new List<Location>();

            Location location = new Location("3610+Hacks+Cross+Rd+Memphis+TN", 0, 200, true);//depot
            Locations.Add(location);
        
            Location location1 = new Location("Piperton+Tennessee", 40, 56, false);
            Locations.Add(location1);

            Location location2 = new Location("205+Windbrook+Dr+Collierville+TN", 30, 45, false);
            Locations.Add(location2);

            Location location3 = new Location("55+Ballard+Rd+Collierville+TN", 55, 70, false);
            Locations.Add(location3);

            Location location4 = new Location("1532+Madison+Ave+Memphis+TN", 65, 88, false);
            Locations.Add(location4);

            Location location5 = new Location("Pleasant+Hill+Mississippi+38654", 40, 65, false);
            Locations.Add(location5);

            Location location6 = new Location("3641+Central+Ave+Memphis+TN", 25, 50, false);
            Locations.Add(location6);

            Location location7 = new Location("Lewisburg+Mississippi+38654", 0, 35, false);
            Locations.Add(location7);

            Location location8 = new Location("4339+Park+Ave+Memphis+TN", 25, 54, false);
            Locations.Add(location8);

            Location location9 = new Location("600+Goodwyn+St+Memphis+TN", 30, 55, false);
            Locations.Add(location9);

            Location location10 = new Location("2000+North+Pkwy+Memphis+TN", 100, 120, false);
            Locations.Add(location10);

            Location location11 = new Location("262+Danny+Thomas+Pl+Memphis+TN", 50, 65, false);
            Locations.Add(location11);

            Location location12 = new Location("125+N+Front+St+Memphis+TN", 65, 80, false);
            Locations.Add(location12);

            Location location13 = new Location("5959+Park+Ave+Memphis+TN", 15, 35, false);
            Locations.Add(location13);

            Location location14 = new Location("814+Scott+St+Memphis+TN", 45, 80, false);
            Locations.Add(location14);

            Location location15 = new Location("1005+Tillman+St+Memphis+TN", 55, 100, false);
            Locations.Add(location15);
        }
    }
}
