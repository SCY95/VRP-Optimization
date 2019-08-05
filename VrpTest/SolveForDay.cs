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
        public static void SolveForAssignedDay(IDataInput dataInput, IDataOutput dataOutput, 
            VrpProblem vrpProblem, Day day,ConfigParams cfg, int[] VCMinMax)
        {

            //TimeMatrixInit(day, cfg);

            //SaveTimeMatrix(day);
            //LoadTimeMatrix(day);
            CalculateTMWithHaversineFormula(day);

            //vrpProblem.SolveVrpProblem(day, cfg);

            int min = VCMinMax[0];
            int max = VCMinMax[1];

            //try
            //{
            while ((day.LocationDropped && !day.InfeasibleNodes) && max >= min)
            {
                day.SetVehicleNumber(min);
                day.ResetResults();

                Console.WriteLine("While ara");
                Console.WriteLine(min + " " + max);
                vrpProblem.SolveVrpProblem(day, cfg, vrpProblem, dataOutput, VCMinMax);

                    
                    dataOutput.PrintSolution(vrpProblem.day, vrpProblem.routing, vrpProblem.manager, vrpProblem.solution);
                    dataOutput.PrintStatus(vrpProblem.routing);
                    i++;
                }
                foreach (var item in day.DroppedLocations)
                {
                    day.Locations.Remove(item);
                }
                
            //}
            //catch
            //{
            //    //7.Güne geçerken Null Reference hatası vererek patlıyor.
            //}




            return;
        }

        public static void AssignAndSolveForDay(IDataInput dataInput, IDataOutput dataOutput,
            VrpProblem vrpProblem, Day day, ConfigParams cfg, int[] VCMinMax)
        {
            CalculateTMWithHaversineFormula(day);
            int i = VCMinMax[0];

            while(day.LocationDropped && !day.InfeasibleNodes)//TODO
            {
                day.SetVehicleNumber(i);
                day.ResetResults();

                vrpProblem.SolveVrpProblem(day, cfg, vrpProblem, dataOutput, VCMinMax);


            }
            foreach (var item in day.DroppedLocations)
            {
                day.Locations.Remove(item);
            }
        }
    }
}
















