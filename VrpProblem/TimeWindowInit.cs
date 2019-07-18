using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Google.OrTools.ConstraintSolver;
using System.Linq;
using Newtonsoft.Json;
using Google.Protobuf.WellKnownTypes;//Duration
using VrpTest.Struct;

namespace VrpTest
{
    public partial class VrpProblem
    {
        static void TimeWindowInit(in Day day,
            in RoutingModel routing,
            in RoutingIndexManager manager)
        {
            RoutingDimension timeDimension = routing.GetMutableDimension("Time");
            timeDimension.SetGlobalSpanCostCoefficient(100);

            // Add time window constraints for each location except depot.
            for (int i = 1; i < day.TimeWindows.GetLength(0); i++)
            {
                long index = manager.NodeToIndex(i);
                timeDimension.CumulVar(index).SetRange(
                    day.TimeWindows[i, 0],
                    day.TimeWindows[i, 1]);
            }

            // Add time window constraints for each vehicle start node.
            for (int i = 0; i < day.Vehicles.Count; i++)
            {
                long index = routing.Start(i);
                timeDimension.CumulVar(index).SetRange(
                    day.TimeWindows[0, 0],
                    day.TimeWindows[0, 1]);
            
                routing.AddVariableMinimizedByFinalizer(
                    timeDimension.CumulVar(routing.Start(i)));
                routing.AddVariableMinimizedByFinalizer(
                    timeDimension.CumulVar(routing.End(i)));
            }
        }
    }
}
