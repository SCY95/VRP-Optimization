using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest
{
    public interface IDataInput
    {
        List<string> ReadAddresses();
        int ReadVehicleNumber();
        int GetDepot();
        string GetAPI_key();
        int GetSolutionDuration();
        long[,] GetTimeWindows();
        bool GetTimeWindowActive();
        int GetMaxVisitsActive();
        long[] GetDemands();
        long[] GetVehicleCapacities(int VehicleNumber);
    }
}
