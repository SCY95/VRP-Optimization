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
            return 4;
        }

        public bool GetTimeWindowActive()
        {                              
            return true;
        }        

        public int GetMaxVisitsActive()
        {
            return 200;//5 yap
        }

        public void GetVCMinMax(int[] VCMinMax)
        {
            Console.WriteLine("Vehicle Count Min, Max : ");
            var MinMax = Console.ReadLine().Split(',');
            int i = 0;

            foreach (var item in MinMax)
            {
                if(i < 2)
                {
                    VCMinMax[i] = Convert.ToInt32(item);
                    Console.WriteLine(Convert.ToInt32(item));
                    i++;
                }
                
            }            
        }


    }
}




