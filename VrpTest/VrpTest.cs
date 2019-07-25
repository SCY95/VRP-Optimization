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
        public static void Main(String[] args)
        {
            Period period = new Period(14);

            SetLocationsForDays(period);            


            for (int i = 0; i < period.Days.Count; i++)
            {
                // Instantiate the data problem.
                DataInput dataInput = new DataInput();//Config interface
                DataOutput dataOutput = new DataOutput();//Output interface
                VrpProblem vrpProblem = new VrpProblem();

                //Period(x) => period for x days     
                ConfigParams cfg = new ConfigParams();
                GetInput(dataInput, cfg, period.Days.ElementAt(i));

                SolveForDay(dataInput, dataOutput, vrpProblem, period.Days.ElementAt(i), cfg);
            }

            period.PrintSummary();
             
            Console.ReadLine();
            return;
        }                                   
    }
}
















/*static void Main()
{


    //Pass request to google api with orgin and destination details
    HttpWebRequest request =
        (HttpWebRequest)WebRequest.Create("https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins=Washington,DC&destinations=New+York+City,NY&key=AIzaSyBe5wHtu7fTIbEfls4Z-8FCkfCJcf41Udc");

    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    using (var streamReader = new StreamReader(response.GetResponseStream()))
    {
        var result = streamReader.ReadToEnd();

        if (!string.IsNullOrEmpty(result))
        {
            Console.WriteLine(result);
        }
    }

    Console.ReadLine();
    return;
}*/




//  searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;
//  searchParameters.LocalSearchMetaheuristic = LocalSearchMetaheuristic.Types.Value.GuidedLocalSearch;
//  searchParameters.TimeLimit = new Duration { Seconds = 7};
//  searchParameters.LogSearch = true;



//var rowCount = data.DistanceMatrix.GetLength(0);
//var colCount = data.DistanceMatrix.GetLength(1);
//for (int row = 0; row < rowCount; row++)
//{
//    for (int col = 0; col < colCount; col++)
//    {
//        Console.Write(string.Format("{0} ", data.DistanceMatrix[row, col]));

//    }
//    Console.Write(Environment.NewLine + Environment.NewLine);


//}