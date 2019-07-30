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
            long AvgDurPerDay = 0;
            long AvgMinDur = 0;
            long AvgMaxDur = 0;
            int AvgPersonel = 0;
            int Holidays = 2;

            for (int i = 0; i < Days.Count; i++)
            {
                string DroppedNodes = "";
                if(Days[i].DroppedLocations != null)
                {
                    foreach (var item in Days[i].DroppedLocations)
                    {
                        DroppedNodes += item.Name + ", ";
                    }
                }
                AvgDurPerDay += Days[i].AvgDur;
                AvgMaxDur += Days[i].MaxDur;
                AvgMinDur += Days[i].MinDur;
                AvgPersonel += Days[i].Vehicles.Count;

                Console.WriteLine(
                    "\nDay " + (i + 1) + " :" +
                    "\nAvarage Duration : " + Days[i].AvgDur + "mins" +
                    "\nMaximum Duration : " + Days[i].MaxDur + "mins" +
                    "\nMinimum Duration : " + Days[i].MinDur + "mins" +
                    "\nVehicle Count : " + Days[i].Vehicles.Count + 
                    "\nDropped Nodes : " +  DroppedNodes +
                    "\n======================================================" +
                    "\n");
                RequiredVehicleCount = Math.Max(RequiredVehicleCount, Days[i].Vehicles.Count);
                
            }
            AvgDurPerDay = AvgDurPerDay / (this.Days.Count - Holidays);
            AvgMaxDur = AvgMaxDur / (this.Days.Count - Holidays);
            AvgMinDur = AvgMinDur / (this.Days.Count - Holidays);
            AvgPersonel = AvgPersonel / (this.Days.Count - Holidays);

            Console.WriteLine("\nMinimimum Required Personnel : " + RequiredVehicleCount);
            Console.WriteLine("\nAverage Required Personnel Per Day : " + AvgPersonel);
            Console.WriteLine("\nAvarage Duration Per Day : " + AvgDurPerDay + "mins");
            Console.WriteLine("\nAverage Min Durations : " + AvgMinDur + "mins");
            Console.WriteLine("\nAverage Max Durations : " + AvgMaxDur + "mins");


        }

    }
}


