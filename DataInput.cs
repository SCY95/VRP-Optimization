using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest
{
    public class DataInput : IDataInput
    {
        List<string> addresses = new List<string> {
                "3610+Hacks+Cross+Rd+Memphis+TN", // depot
                       "Piperton+Tennessee",//3,7
                       "205+Windbrook+Dr+Collierville+TN",//11,14
                       "55+Ballard+Rd+Collierville+TN",//7,10
                       "1532+Madison+Ave+Memphis+TN",//0,6
                       "Pleasant+Hill+Mississippi+38654",//5,8
                       "3641+Central+Ave+Memphis+TN",//10,15

                        "Lewisburg+Mississippi+38654",//2,6
                       "4339+Park+Ave+Memphis+TN",//7,12
                       "600+Goodwyn+St+Memphis+TN",//16,18
                       "2000+North+Pkwy+Memphis+TN",//20,23
                       "262+Danny+Thomas+Pl+Memphis+TN",//4,8
                       "125+N+Front+St+Memphis+TN",//6,10
                       "5959+Park+Ave+Memphis+TN",//0,5
                       "814+Scott+St+Memphis+TN",//15,19
                       "1005+Tillman+St+Memphis+TN"//16,18

                };


        public List<string> GetAddresses()
        {
            
            return addresses;
        }

        public int GetVehicleNumber()
        {
            return 1;
        }

        public int GetDayNumber()
        {
            return 2;
        }

        public int GetDepot()
        {
            return 0;
        }
        public string GetAPI_key()
        {
            return "AIzaSyBe5wHtu7fTIbEfls4Z-8FCkfCJcf41Udc";
        }
        public int GetSolutionDuration()
        {
            return 4;
        }
        public bool GetTimeWindowActive()
        {
            return true;
        }

        public long[,] GetTimeWindows(DataModel data)
        {
            long[,] _timeWindows = {
                {0, 200},    // depot
                {40, 56},   // 1
                {30, 45},  // 2
                {55, 70},   // 3
                {65, 88},   // 4
                {40, 65},    // 5
                {25, 50},   // 6
                {0, 35},   // 7
                {25, 54},   // 8
                {30, 55},    // 9
                {100, 120},  // 10
                {50, 65},  // 11
                {65, 80},    // 12
                {15, 35},   // 13
                {45, 80 },     // 14
                {55, 100},   // 15
              
            };



            return _timeWindows;           
        }

        public int GetMaxVisitsActive()
        {
            return 5;//5 yap
        }
        public long[] GetDemands()
        {
            long[] Demands = { 0, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

            return Demands;
        }

        public long[] GetVehicleCapacities(int VehicleNumber)
        {
            long[] VehicleCapacities = new long[VehicleNumber];


            for (int i = 0; i < VehicleNumber; i++)
            {
                VehicleCapacities[i] = GetMaxVisitsActive() * 10;
            }
            return VehicleCapacities;
        }




    }
}



/*
 "3610+Hacks+Cross+Rd+Memphis+TN", // depot
                       "1921+Elvis+Presley+Blvd+Memphis+TN",
                       "149+Union+Avenue+Memphis+TN",
                       "1034+Audubon+Drive+Memphis+TN",
                       "1532+Madison+Ave+Memphis+TN",
                       "706+Union+Ave+Memphis+TN",
                       "3641+Central+Ave+Memphis+TN",

                        "926+E+McLemore+Ave+Memphis+TN",
                       "4339+Park+Ave+Memphis+TN",
                       "600+Goodwyn+St+Memphis+TN",
                       "2000+North+Pkwy+Memphis+TN",
                       "262+Danny+Thomas+Pl+Memphis+TN",
                       "125+N+Front+St+Memphis+TN",
                       "5959+Park+Ave+Memphis+TN",
                       "814+Scott+St+Memphis+TN",
                       "1005+Tillman+St+Memphis+TN"
     
     */



