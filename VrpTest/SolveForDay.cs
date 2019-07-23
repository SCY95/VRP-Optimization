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
        public static void SolveForDay(DataInput dataInput, DataOutput dataOutput, 
            VrpProblem vrpProblem, Day day,ConfigParams cfg)
        {
            
            //TimeMatrixInit(day, cfg);

            //SaveTimeMatrix(day);
            //LoadTimeMatrix(day);
            CalculateTMWithHaversineFormula(day);

            //vrpProblem.SolveVrpProblem(day, cfg);

            int i = 54;

            try
            {
                while (day.LocationDropped)
                {
                    day.SetVehicleNumber(i);
                    day.ResetResults();

                    vrpProblem.SolveVrpProblem(day, cfg);

                    dataOutput.PrintSolution(vrpProblem.day, vrpProblem.routing, vrpProblem.manager, vrpProblem.solution);
                    dataOutput.PrintStatus(vrpProblem.routing);

                    i++;
                }
            }
            catch
            {

            }




            return;
        }
    }
}
















