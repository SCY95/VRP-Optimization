using System;
using System.Collections.Generic;
using System.Text;
using VrpTest.Struct;


namespace VrpTest
{
    public partial class VrpTest
    {
        public static void GetInput(IDataInput dataInput, ConfigParams cfg, Day day)
        {
            cfg.API_key = dataInput.GetAPI_key();
            cfg.SolutionDuration = dataInput.GetSolutionDuration();
          

            //day.Addresses = dataInput.ReadAddresses();
            //day.Depot = dataInput.GetDepot();
            //day.TimeMatrix = new long[day.Addresses.Count, day.Addresses.Count];//TODO
            //day.TimeWindows = new long[day.Addresses.Count, 2];
            //day.TimeWindows = dataInput.GetTimeWindows();
            //day.Demands = dataInput.GetDemands();
            day.LocationDropped = true;
            day.TimeWindowsActive = dataInput.GetTimeWindowActive();
            day.MaxVisitsActive = dataInput.GetMaxVisitsActive();
            day.SetVehicleNumber(1);
        }



    }
}