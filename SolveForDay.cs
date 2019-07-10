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
        public static void SolveForDay(DataInput dataInput, DataOutput dataOutput, VrpProblem vrpProblem, Day day)
        {
            DataModel data = new DataModel(dataInput);

            TimeMatrixInit(data, day);

            vrpProblem.SolveVrpProblem(data, day);

            int i = 1;
            int max_vehicles = 150;
            while (data.locationDropped && i < max_vehicles)
            {
                data.SetVehicleNumber(i);

                vrpProblem.SolveVrpProblem(data ,day);

                dataOutput.PrintSolution(vrpProblem.data, vrpProblem.routing, vrpProblem.manager, vrpProblem.solution);

                i++;
            }

            dataOutput.PrintStatus(vrpProblem.routing);


            return;
        }
    }
}
















