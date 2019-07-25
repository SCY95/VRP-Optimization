using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Google.OrTools.ConstraintSolver;
using System.Linq;
using Newtonsoft.Json;
using Google.Protobuf.WellKnownTypes;//Duration
using System.Diagnostics;
using VrpTest.Struct;

namespace VrpTest
{
    public partial class VrpTest
    {



        static void CalculateTMWithHaversineFormula(Day day)
        {
            int x = -1, y = 0;
            day.Penalty = 9999999999999;

            foreach (var item in day.Locations)
            {
                y = 0;
                x++;
                foreach (var item2 in day.Locations)
                {
                    day.TimeMatrix[x, y] = Convert.ToInt32(HaversineDistance(new LatLng(item.Position.x_, item.Position.y_),
                        new LatLng(item2.Position.x_, item2.Position.y_),
                        DistanceUnit.Kilometers) / 50 * 60 *1.55) + 10; // /Avg Car speed * 60 Minutes) + Service Time
                    y++;
                }
            }

        }





        public static double HaversineDistance(LatLng pos1, LatLng pos2, DistanceUnit unit)
        {
            double R = (unit == DistanceUnit.Miles) ? 3960 : ((unit == DistanceUnit.Kilometers) ? 6371 : 6371000);
            var lat = ConvertDegreesToRadians(pos2.Latitude - pos1.Latitude);
            var lng = ConvertDegreesToRadians(pos2.Longitude - pos1.Longitude);
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(ConvertDegreesToRadians(pos1.Latitude)) * Math.Cos(ConvertDegreesToRadians(pos2.Latitude)) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return R * h2;
        }

        public static double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return radians;
        }

        public enum DistanceUnit { Miles, Kilometers, Meters };


        public class LatLng
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }

            public LatLng()
            {
            }

            public LatLng(double lat, double lng)
            {
                this.Latitude = lat;
                this.Longitude = lng;
            }
        }






        //TODO
        static void SaveTimeMatrix(Day day)
        {

            string[] RawData = File.ReadAllLines(@"..\..\..\Docs\TimeMatrixLG.json");

            string[] Formatted = RawData;
            int j = 0;
            int k = 0;
            for (int i = 0; i < RawData.Length; i++)
            {
                if (RawData[i].Contains("elements"))
                {
                    k = 0;
                    Formatted[i - 1] += "\n" +
                         "         \"client_ref\" : " + day.Locations.ElementAt(j).ClientRef.ToString() + ",";
                    j++;
                }
                else if(RawData[i].Contains("distance"))
                {
                    Formatted[i - 1] += "\n" +
                         "               \"client_ref\" : " + day.Locations.ElementAt(k).ClientRef.ToString() + ",";
                    k++;
                }
            }
            System.IO.File.WriteAllText(@"..\..\..\Docs\TimeMatrixLG.json", string.Empty);
            System.IO.File.WriteAllLines(@"..\..\..\Docs\TimeMatrixLG.json", Formatted);
        }

        static void LoadTimeMatrix(Day day)
        {
            List<int> clientRefs = new List<int>();
            foreach (var item in day.Locations)
            {
                clientRefs.Add(item.ClientRef);
            }
            bool isElement = false;
            string[] Data = File.ReadAllLines(@"..\..\..\Docs\TimeMatrixLG.json");
            string clientRef = "";
            string duration = "";
            int x = -1, y = 0;

            //TODO TEMP
            day.Penalty = 9999999999999;

            for (int i = 0; i < Data.Length; i++)
            {

                string line = "";
                clientRef = "";
                duration = "";

                if (Data[i].Contains("distance") && isElement)
                {
                    line = Data[i - 1];
                    for (int j = 30; j < line.Length - 1; j++)
                    {
                        clientRef += line[j];
                           
                    }

                    if (clientRefs.Contains(Convert.ToInt32(clientRef)))
                    {
                        line = Data[i + 6];
                        for (int k = 28; k < line.Length; k++)
                        {
                            duration += line[k];                            
                        }
                        Console.WriteLine(duration);
                        day.TimeMatrix[x, y] = Convert.ToInt32(duration)/60;
                        y++;

                    }
                }
                else if (Data[i].Contains("elements"))
                {
                    line = Data[i - 1];
                    for (int j = 24; j < line.Length - 1; j++)
                    {
                        clientRef += line[j];
                       
                    }
                    if (clientRefs.Contains(Convert.ToInt32(clientRef)))
                    {                       
                        x++;
                        y = 0;
                        isElement = true;

                    }
                    else
                    {
                        isElement = false;
                    }

                }


            }

        }
    }
}