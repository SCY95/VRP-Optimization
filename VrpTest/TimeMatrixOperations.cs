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