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
            int ObjectCount = day.Locations.Count * day.Locations.Count;

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
                         "         client_ref : " + day.Locations.ElementAt(j).ClientRef.ToString() + ",";
                    j++;
                }
                else if(RawData[i].Contains("distance"))
                {
                    Formatted[i - 1] += "\n" +
                         "               client_ref : " + day.Locations.ElementAt(k).ClientRef.ToString() + ",";
                    k++;
                }
            }
            System.IO.File.WriteAllText(@"..\..\..\Docs\TimeMatrixLG.json", string.Empty);
            System.IO.File.WriteAllLines(@"..\..\..\Docs\TimeMatrixLG.json", Formatted);



        }
    }
}