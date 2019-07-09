using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest
{
    public class DataModel
    {
        public long[,] DistanceMatrix;//Duration 
        public long[,] TimeWindows;
        public int VehicleNumber;
        public int Depot;
        public string API_key;
        public int SolutionDuration;
        public bool TimeWindowsActive;
        public int MaxVisitsActive;
        public long[] Demands;
        public long[] VehicleCapacities;
        public bool locationDropped;
        public long penalty;
        List<List<List<int>>> days;

        IDataInput dataInput;

        public List<string> addresses;


        public DataModel(IDataInput dataInput)
        {
            this.dataInput = dataInput;
            addresses = this.dataInput.GetAddresses();
            VehicleNumber = this.dataInput.ReadVehicleNumber();
            Depot = this.dataInput.GetDepot();
            API_key = this.dataInput.GetAPI_key();
            DistanceMatrix = new long[addresses.Count, addresses.Count];//TODO
            SolutionDuration = this.dataInput.GetSolutionDuration();
            TimeWindows = new long[addresses.Count, addresses.Count];
            TimeWindows = this.dataInput.GetTimeWindows(this);
            TimeWindowsActive = this.dataInput.GetTimeWindowActive();
            MaxVisitsActive = this.dataInput.GetMaxVisitsActive();
            Demands = this.dataInput.GetDemands();
            VehicleCapacities = this.dataInput.GetVehicleCapacities(VehicleNumber);
            locationDropped = true;
        }

        public void SetVehicleNumber(int VehicleNumber)
        {
            this.VehicleNumber = VehicleNumber;
            this.VehicleCapacities = dataInput.GetVehicleCapacities(VehicleNumber);
        }



    };
}
