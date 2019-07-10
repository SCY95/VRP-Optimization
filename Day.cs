using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest
{
    public class Day
    {
        public List<string> Addresses { get; set; }
        public long[,] TimeMatrix;//Duration 
        public long[,] TimeWindows { get; set; }
        public int Depot { get; set; }
        public long Penalty { get; set; }
        public bool LocationDropped { get; set; }
        public long[] Demands { get; set; }
        public long[] VehicleCapacities { get; set; }
        public bool TimeWindowsActive { get; set; }
        public int MaxVisitsActive { get; set; }

        public List<Vehicle> Vehicles { get; set; }
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

        public void SetVehicleNumber(int VehicleNumber)
        {
            for (int i = 0; i < VehicleNumber; i++)
            {
                Vehicle vehicle = new Vehicle();
                vehicle.Capacity = 10 * MaxVisitsActive;
                Vehicles.Add(vehicle);
            }
        }

    }
}
