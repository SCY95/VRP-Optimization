using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest
{
    public class Day
    {
        public List<Location> Locations { get; set; }
        public List<string> Addresses { get; set; }
        public long[,] TimeMatrix;                           //Duration 
        public long[,] TimeWindows { get; set; }
        public int Depot { get; set; }
        public long Penalty { get; set; }
        public bool LocationDropped { get; set; }
        public long[] Demands { get; set; }
        public long[] VehicleCapacities { get; set; }
        public bool TimeWindowsActive { get; set; }
        public int MaxVisitsActive { get; set; }

        public List<Vehicle> Vehicles { get; set; }
        public List<Location> DroppedLocations { get; set; }
        public long TotalDur { get; set; }                   //Dur: Duration
        public long MaxDur { get; set; }
        public long MinDur { get; set; }
        public long AvgDur { get; set; }        


        public Day()
        {
            Vehicles = new List<Vehicle>();
        }

        

        public void SetDay(List<Location> Locations)
        {
            this.TimeMatrix = new long[Locations.Count,Locations.Count];
            this.Locations = Locations;
            this.TimeWindows = new long[Locations.Count, 2];
            this.Addresses = new List<string>();
            this.Demands = new long[Locations.Count];
            this.DroppedLocations = new List<Location>();

            int i = 0;

            foreach (var item in Locations)
            {
                if (item.IsDepot == true)
                {
                    this.Depot = i;
                }

                this.Addresses.Add(item.Address);
                this.TimeWindows[i, 0] = item.TWLower;
                this.TimeWindows[i, 1] = item.TWUpper;
                this.Demands[i] = item.Demand;
                i++;
            }
        }

        public void SetVehicleNumber(int VehicleNumber)
        {
            VehicleCapacities = new long[VehicleNumber];
            Vehicles = new List<Vehicle>();
            for (int i = 0; i < VehicleNumber; i++)
            {
                Vehicle vehicle = new Vehicle();
                vehicle.Capacity = 10 * MaxVisitsActive;
                Vehicles.Add(vehicle);
                VehicleCapacities[i] = vehicle.Capacity;
            }
        }

        public void ResetResults()
        {
            this.TotalDur = 0;                 
            this.MaxDur = 0;
            this.MinDur = 0;
            this.AvgDur = 0;
        }

}
}
