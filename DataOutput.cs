using System;
using System.Collections.Generic;
using System.Text;
using Google.OrTools.ConstraintSolver;
using System.Diagnostics;

namespace VrpTest
{
    public class DataOutput : IDataOutput
    {
        public void PrintSolution(in DataModel data,
            in RoutingModel routing,
            in RoutingIndexManager manager,
            in Assignment solution)
        {
            PrintToConsole(data, routing, manager, solution);
            ShowOnMap(data, routing, manager, solution);
        }

        public int PrintStatus(RoutingModel routing)
        {


            switch (routing.GetStatus())
            {
                case 0:
                    Console.WriteLine("Problem not solved yet.");
                    return 0;            
                case 1:
                    Console.WriteLine("Problem solved successfully.");
                    return 1;
                case 2:
                    Console.WriteLine("No solution found to the problem.");
                    return 2;
                case 3:
                    Console.WriteLine("Time limit reached before finding a solution.");
                    return 3;
                case 4:
                    Console.WriteLine("Model, model parameters, or flags are not valid.");
                    return 4;
                default:
                    return -1;                    
            }
            
        }

        void ShowOnMap(in DataModel data,
            in RoutingModel routing,
            in RoutingIndexManager manager,
            in Assignment solution)
        {
            List<List<int>> routes = new List<List<int>>();

            for (int i = 0; i < data.VehicleNumber; ++i)
            {
                var index = routing.Start(i);
                List<int> route = new List<int>();

                while (routing.IsEnd(index) == false)
                {
                    route.Add(manager.IndexToNode((int)index));
          

                    index = solution.Value(routing.NextVar(index));
                }
                route.Add(manager.IndexToNode((int)index));
                routes.Add(route);

            }




            for (int i = 0; i < routes.Count; i++)
            {
                string url = "https://www.google.com.tr/maps/dir/";
                foreach (var item in routes[i])
                {
                    url += data.addresses[item] + "/";
                }
                //url += data.addresses[data.Depot];
                Process process = new Process();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "chrome";
                process.StartInfo.Arguments = url;
                process.Start();

            }



        }

        public void PrintToConsole(
            in DataModel data,
            in RoutingModel routing,
            in RoutingIndexManager manager,
            in Assignment solution)
        {
            // Inspect solution.
            long maxRouteDistance = 0;
            for (int i = 0; i < data.VehicleNumber; i++)
            {
                Console.WriteLine("Route for Vehicle {0}:", i);
                long routeDistance = 0;

                var index = routing.Start(i);

                while (routing.IsEnd(index) == false)
                {
                    Console.Write("{0} -> ", manager.IndexToNode((int)index));
                    var previousIndex = index;

                    index = solution.Value(routing.NextVar(index));


                    routeDistance += routing.GetArcCostForVehicle(previousIndex, index, 0);
                }
                Console.WriteLine("{0}", manager.IndexToNode((int)index));
                Console.WriteLine("Duration of the route: {0}mins", routeDistance);
                maxRouteDistance = Math.Max(routeDistance, maxRouteDistance);
            }
            Console.WriteLine("Maximum distance of the routes: {0}mins", maxRouteDistance);
        }

      
    }
}
