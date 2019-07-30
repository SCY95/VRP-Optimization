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
using VrpTest.Struct;


namespace VrpTest
{
    public partial class VrpProblem
    {
        public Day day;
        public ConfigParams cfg;
        public RoutingModel routing;
        public RoutingIndexManager manager;
        public Assignment solution;
        public Solver solver;
        IntVar x;

        public void SolveVrpProblem(Day day, ConfigParams cfg, VrpProblem vrpProblem, DataOutput dataOutput)
        {   
            this.day = day;
            this.cfg = cfg;
            //Google Distance Matrix API (Duration matrix)


            // Create Routing Index Manager
            manager = new RoutingIndexManager(
                day.TimeMatrix.GetLength(0),
                day.Vehicles.Count,
                day.Depot);


            // Create Routing Model.
            routing = new RoutingModel(manager);
            
            // Create and register a transit callback.
            int transitCallbackIndex = routing.RegisterTransitCallback(
                (long fromIndex, long toIndex) =>
                    {
                        // Convert from routing variable Index to distance matrix NodeIndex.
                        var fromNode = manager.IndexToNode(fromIndex);
                        var toNode = manager.IndexToNode(toIndex);
                        return day.TimeMatrix[fromNode, toNode];
                    }
                );

            // Define cost of each arc.
            routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);

            // Add Distance constraint.

            if (day.TimeWindowsActive != true)
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
               600, // vehicle maximum capacities
               false,  // start cumul to zero
               "Time");

                TimeWindowInit(day, routing, manager);//Set Time Window Constraints

            }
            if (day.MaxVisitsActive != 0)
            {
                 int demandCallbackIndex = routing.RegisterUnaryTransitCallback(
                    (long fromIndex) => {
                                    // Convert from routing variable Index to demand NodeIndex.
                                    var fromNode = manager.IndexToNode(fromIndex);
                        return day.Demands[fromNode];
                    }
                    );

                routing.AddDimensionWithVehicleCapacity(
                    demandCallbackIndex, 0,  // null capacity slack
                    day.VehicleCapacities,   // vehicle maximum capacities
                    true,                      // start cumul to zero
                    "Capacity");
            }

            // Allow to drop nodes.
            for (int i = 1; i < day.TimeMatrix.GetLength(0); ++i)
            {
                routing.AddDisjunction(
                    new long[] { manager.NodeToIndex(i) }, day.Penalty);
            }

            // Setting first solution heuristic.
            RoutingSearchParameters searchParameters =
              operations_research_constraint_solver.DefaultRoutingSearchParameters();
            

            searchParameters.FirstSolutionStrategy =
              FirstSolutionStrategy.Types.Value.PathMostConstrainedArc;

            //metaheuristic
            searchParameters.LocalSearchMetaheuristic = LocalSearchMetaheuristic.Types.Value.GuidedLocalSearch;
            searchParameters.TimeLimit = new Duration { Seconds = cfg.SolutionDuration };
            searchParameters.LogSearch = true;

            solver = routing.solver();

            //TODO
            //Some location must be on same route.
            //solver.Add(routing.VehicleVar(manager.NodeToIndex(2)) == routing.VehicleVar(manager.NodeToIndex(5)));
            //One node takes precedence over the another.
            //routing.AddPickupAndDelivery(manager.NodeToIndex(2), manager.NodeToIndex(5));

            //Constraint variable
            x = solver.MakeIntVar(day.Vehicles.Count, day.Vehicles.Count, "x");
            //Number of vehicle restriction
            solver.Add(x <= 120);


            // Solve the problem.
            solution = routing.SolveWithParameters(searchParameters);


            day.LocationDropped = false;

            // Display dropped nodes.
            List<int> droppedNodes = new List<int>();

            for (int index = 0; index < routing.Size(); ++index)
            {
                if (routing.IsStart(index) || routing.IsEnd(index))
                {
                    continue;
                }
                if (solution.Value(routing.NextVar(index)) == index)
                {
                    droppedNodes.Add(manager.IndexToNode((int)index)); 
                    day.LocationDropped = true;
                }
            }
            day.DroppedLocations.Clear();
            if (droppedNodes != null)
            {
                foreach (var item in droppedNodes)
                {
                    Location location = LocationDB.Locations.Where(d => d.Position.strPos_ == day.Addresses[item]).ToList().ElementAt(0);
                    
                    if(location != null)
                    {                        
                        Console.WriteLine(location.Name);
                        day.DroppedLocations.Add(location);
                    }
                }
            }


            //Inspect Infeasable Nodes
            for (int i = 0; i < day.Vehicles.Count; i++)
            {        
                var index = routing.Start(i);
                int j = 0;
                while (routing.IsEnd(index) == false )
                {
                    j++;

                    index = solution.Value(routing.NextVar(index));

                }
                if(j == 1)
                {
                    day.InfeasibleNodes = true;

                    day.SetVehicleNumber(day.Vehicles.Count - 1);
                    day.ResetResults();

                    vrpProblem.SolveVrpProblem(day, cfg, vrpProblem, dataOutput);
                    
                }
            }

            // Inspect solution.
            day.TotalDur = 0;
            day.MinDur = 100000;
            for (int i = 0; i < day.Vehicles.Count; i++)
            {
                long routeDuration = 0;

                var index = routing.Start(i);

                while (routing.IsEnd(index) == false)
                {
                    var previousIndex = index;

                    index = solution.Value(routing.NextVar(index));

                    routeDuration += routing.GetArcCostForVehicle(previousIndex, index, 0);
                }
                day.TotalDur += routeDuration;
                day.MaxDur = Math.Max(routeDuration, day.MaxDur);                
                day.MinDur = Math.Min(routeDuration, day.MinDur);
                
            }
            day.AvgDur = day.TotalDur / day.Vehicles.Count;

        }
    }
}
