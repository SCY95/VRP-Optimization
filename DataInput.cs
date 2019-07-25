using System;
using System.Collections.Generic;
using System.Text;
using VrpTest.Struct;

namespace VrpTest
{
    public class DataInput : IDataInput
    {
        public DataInput()
        {
          
        }      

        public string GetAPI_key()
        {
            return "AIzaSyD8pSdYUKWghe4pUK2EG_HniwVOt_Mu2jw";
        }

        public int GetSolutionDuration()
        {
            return 3;
        }

        public bool GetTimeWindowActive()
        {
            return true;
        }        

        public int GetMaxVisitsActive()
        {
            return 23;//5 yap
        }

       
    }
}




