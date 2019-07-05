using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Google.OrTools.ConstraintSolver;
using System.Linq;
using Newtonsoft.Json;
using Google.Protobuf.WellKnownTypes;//Duration
using System.Diagnostics;

namespace VrpTest
{
    public partial class VrpTest
    {
        public static void Main(String[] args)
        {

            
           
            // Instantiate the data problem.
            DataInput dataInput = new DataInput();//Config interface
            DataOutput dataOutput = new DataOutput();//Output interface
            DataModel data = new DataModel(dataInput);

            DistanceMatrixInit(data);//Google Distance Matrix API (Duration matrix)

            


            // Create Routing Index Manager
            RoutingIndexManager manager = new RoutingIndexManager(
                data.DistanceMatrix.GetLength(0),
                data.VehicleNumber,
                data.Depot);


            // Create Routing Model.
            RoutingModel routing = new RoutingModel(manager);

            // Create and register a transit callback.
            int transitCallbackIndex = routing.RegisterTransitCallback(
                (long fromIndex, long toIndex) =>
                {
                // Convert from routing variable Index to distance matrix NodeIndex.
                var fromNode = manager.IndexToNode(fromIndex);
                    var toNode = manager.IndexToNode(toIndex);
                return data.DistanceMatrix[fromNode, toNode];
                }
                );

            // Define cost of each arc.
            routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);

            // Add Distance constraint.
            
            if (data.TimeWindowsActive != true)
            {
                routing.AddDimension(transitCallbackIndex, 0, 700000,
                                true,  // start cumul to zero
                                "Distance");
                RoutingDimension distanceDimension = routing.GetMutableDimension("Distance");
                distanceDimension.SetGlobalSpanCostCoefficient(100);
            }
            else
            {
                            routing.AddDimension(
               transitCallbackIndex, // transit callback
               1000,// allow waiting time
               1000, // vehicle maximum capacities
               false,  // start cumul to zero
               "Time");

                TimeWindowInit(data, routing, manager);//Set Time Window Constraints

            }
            if(data.MaxVisitsActive != 0)
            {
                            int demandCallbackIndex = routing.RegisterUnaryTransitCallback(
                   (long fromIndex) => {
                      // Convert from routing variable Index to demand NodeIndex.
                      var fromNode = manager.IndexToNode(fromIndex);
                       return data.Demands[fromNode];
                   }
                 );
                routing.AddDimensionWithVehicleCapacity(
                  demandCallbackIndex, 0,  // null capacity slack
                  data.VehicleCapacities,   // vehicle maximum capacities
                  true,                      // start cumul to zero
                  "Capacity");
            }
         




            // Setting first solution heuristic.
            RoutingSearchParameters searchParameters =
              operations_research_constraint_solver.DefaultRoutingSearchParameters();


            searchParameters.FirstSolutionStrategy =
              FirstSolutionStrategy.Types.Value.PathCheapestArc;

            //metaheuristic
            searchParameters.LocalSearchMetaheuristic = LocalSearchMetaheuristic.Types.Value.SimulatedAnnealing;
            searchParameters.TimeLimit = new Duration { Seconds = data.SolutionDuration };
            searchParameters.LogSearch = true;

            // Solve the problem.
            Assignment solution = routing.SolveWithParameters(searchParameters);
            
            // Print solution on console.
            dataOutput.PrintSolution(data, routing, manager, solution);





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