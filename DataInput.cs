using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest
{
    public class DataInput : IDataInput
    {
        public DataInput()
        {
          
        }      

        public string GetAPI_key()
        {
            return "AIzaSyA6NWQNzTeP3SZ6riaW5BLUwcRcHxZLDW0";
        }

        public int GetSolutionDuration()
        {
            return 4;
        }

        public bool GetTimeWindowActive()
        {
            return true;
        }        

        public int GetMaxVisitsActive()
        {
            return 5;//5 yap
        }
    }
}




