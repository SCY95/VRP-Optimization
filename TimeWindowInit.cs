using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Google.OrTools.ConstraintSolver;
using System.Linq;
using Newtonsoft.Json;
using Google.Protobuf.WellKnownTypes;//Duration

namespace VrpTest
{
    public partial class VrpTest
    {
        static void TimeWindowInit(in DataModel data,
            in RoutingModel routing,
            in RoutingIndexManager manager)
        {
            RoutingDimension timeDimension = routing.GetMutableDimension("Time");
            timeDimension.SetGlobalSpanCostCoefficient(100);

            // Add time window constraints for each location except depot.
            for (int i = 1; i < data.TimeWindows.GetLength(0); i++)
            {
                long index = manager.NodeToIndex(i);
                timeDimension.CumulVar(index).SetRange(
                    data.TimeWindows[i, 0],
                    data.TimeWindows[i, 1]);
            }

            // Add time window constraints for each vehicle start node.
            for (int i = 0; i < data.VehicleNumber; i++)
            {
                long index = routing.Start(i);
                timeDimension.CumulVar(index).SetRange(
                    data.TimeWindows[0, 0],
                    data.TimeWindows[0, 1]);
            }

            for (int i = 0; i < data.VehicleNumber; i++)
            {
                routing.AddVariableMinimizedByFinalizer(
                    timeDimension.CumulVar(routing.Start(i)));
                routing.AddVariableMinimizedByFinalizer(
                    timeDimension.CumulVar(routing.End(i)));
            }

        }


    }

}
