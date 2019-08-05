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
            int day = 0;

            for (day = 0; day < Days.Count; day++)
            {
                if (Days[day].Locations.Count != 0)
                {
                    string DroppedNodes = "";
                    if (Days[day].DroppedLocations != null)
                    {
                        foreach (var item in Days[day].DroppedLocations)
                        {
                            DroppedNodes += item.Name + ", ";
                        }
                    }
                    AvgDurPerDay += Days[day].AvgDur;
                    AvgMaxDur += Days[day].MaxDur;
                    AvgMinDur += Days[day].MinDur;
                    AvgPersonel += Days[day].Vehicles.Count;

                    Console.WriteLine(
                        "\nDay " + (day + 1) + " :" +
                        "\nAvarage Duration : " + Days[day].AvgDur + "mins" +
                        "\nMaximum Duration : " + Days[day].MaxDur + "mins" +
                        "\nMinimum Duration : " + Days[day].MinDur + "mins" +
                        "\nVehicle Count : " + Days[day].Vehicles.Count +
                        "\nDropped Nodes : " + DroppedNodes +
                        "\n======================================================" +
                        "\n");
                    RequiredVehicleCount = Math.Max(RequiredVehicleCount, Days[day].Vehicles.Count);
                }
                else
                { 
                    Console.WriteLine("\nDay " + (day + 1) + " :");
                    Console.WriteLine("\nThis day is holiday");
                    Console.WriteLine("\n======================================================");
                }
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


