using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest
{
    public interface IDataInput
    {  
        string GetAPI_key();
        int GetSolutionDuration();
        bool GetTimeWindowActive();
        int GetMaxVisitsActive();
        void GetVCMinMax(int[] VCMinMax);
    }
}
