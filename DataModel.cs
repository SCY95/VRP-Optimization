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

        public List<string> addresses;


        public DataModel(IDataInput dataInput)
        {
            addresses = dataInput.GetAddresses();
            VehicleNumber = dataInput.GetVehicleNumber();
            Depot = dataInput.GetDepot();
            API_key = dataInput.GetAPI_key();
            DistanceMatrix = new long[addresses.Count, addresses.Count];//TODO
            SolutionDuration = dataInput.GetSolutionDuration();
            TimeWindows = new long[addresses.Count, addresses.Count];
            TimeWindows = dataInput.GetTimeWindows(this);
            TimeWindowsActive = dataInput.GetTimeWindowActive();
            MaxVisitsActive = dataInput.GetMaxVisitsActive();
            Demands = dataInput.GetDemands();
            VehicleCapacities = dataInput.GetVehicleCapacities();
        }



    };
}
