using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using Google.OrTools.ConstraintSolver;
using System.Linq;
using Newtonsoft.Json;
using Google.Protobuf.WellKnownTypes;//Duration
using System.Diagnostics;

namespace VrpTest
{
    public partial class VrpProblem
    {
        public DataModel data;
        public RoutingModel routing;
        public RoutingIndexManager manager;
        public Assignment solution;

        public void SolveVrpProblem(IDataInput dataInput, IDataOutput dataOutput)
        {
            data = new DataModel(dataInput);

            DistanceMatrixInit(data);//Google Distance Matrix API (Duration matrix)




            // Create Routing Index Manager
            manager = new RoutingIndexManager(
                data.DistanceMatrix.GetLength(0),
                data.VehicleNumber,
                data.Depot);


            // Create Routing Model.
            routing = new RoutingModel(manager);

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
            if (data.MaxVisitsActive != 0)
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
            searchParameters.LocalSearchMetaheuristic = LocalSearchMetaheuristic.Types.Value.GuidedLocalSearch;
            searchParameters.TimeLimit = new Duration { Seconds = data.SolutionDuration };
            searchParameters.LogSearch = true;

            // Solve the problem.
            solution = routing.SolveWithParameters(searchParameters);
            



        }
    }
}
