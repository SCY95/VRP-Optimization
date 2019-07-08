using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest
{
    public interface IDataInput
    {
        List<string> GetAddresses();
        int GetVehicleNumber();
        int GetDayNumber();
        int GetDepot();
        string GetAPI_key();
        int GetSolutionDuration();
        long[,] GetTimeWindows(DataModel data);
        bool GetTimeWindowActive();
        int GetMaxVisitsActive();
        long[] GetDemands();
        long[] GetVehicleCapacities(int VehicleNumber);
    }
}
