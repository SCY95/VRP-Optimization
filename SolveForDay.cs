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
        public static void SolveForDay(DataInput dataInput, DataOutput dataOutput, VrpProblem vrpProblem)
        {
            DataModel data = new DataModel(dataInput);

            DistanceMatrixInit(data);

            vrpProblem.SolveVrpProblem(data);

            int i = 1;
            int max_vehicles = 150;
            while (data.locationDropped && i < max_vehicles)
            {
                data.SetVehicleNumber(i);

                vrpProblem.SolveVrpProblem(data);

                dataOutput.PrintSolution(vrpProblem.data, vrpProblem.routing, vrpProblem.manager, vrpProblem.solution);

                i++;
            }

            dataOutput.PrintStatus(vrpProblem.routing);


            return;
        }
    }
}
















