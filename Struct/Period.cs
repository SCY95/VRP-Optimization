using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest.Struct
{
    public class Period
    {
        public List<Day> Days { get; set; }
        public int RequiredVehicleCount { get; set; }

        public Period(int count)
        {
            Days = new List<Day>();

            for (int i = 0; i < count; i++)
            {
                Day day = new Day();
                Days.Add(day);
            }
        }
        
        public void PrintSummary()
        {
            for (int i = 0; i < Days.Count; i++)
            {
                string DroppedNodes = "";
                if(Days[i].DroppedLocations != null)
                {
                    foreach (var item in Days[i].DroppedLocations)
                    {
                        DroppedNodes += item.Name;
                    }
                }
                

                Console.WriteLine(
                    "\nDay " + (i + 1) + " :" +
                    "\nAvarage Duration : " + Days[i].AvgDur +
                    "\nMaximum Duration : " + Days[i].MaxDur +
                    "\nMinimum Duration : " + Days[i].MinDur +
                    "\nVehicle Count : " + Days[i].Vehicles.Count +
                    "\nDropped Nodes : " +  DroppedNodes+
                    "\n======================================================" +
                    "\n");
                RequiredVehicleCount = Math.Max(RequiredVehicleCount, Days[i].Vehicles.Count);
                
            }

            Console.WriteLine("\nMinimimum required vehicle : " + RequiredVehicleCount);
        }

    }
}
