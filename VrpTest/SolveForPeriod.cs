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
    //public partial class VrpTest
    //{
    //    public void SolveForPeriod(Period period, List<Location> locations)
    //    {
    //        for (int i = 0; i < period.Days.Count; i++)
    //        {
    //            // Instantiate the data problem.
    //            DataInput dataInput = new DataInput();//Config interface
    //            DataOutput dataOutput = new DataOutput();//Output interface
    //            VrpProblem vrpProblem = new VrpProblem();

    //            //Period(x) => period for x days     
    //            period.Days.ElementAt(i).SetDay(locations);
    //            ConfigParams cfg = new ConfigParams();
    //            GetInput(dataInput, cfg, period.Days.ElementAt(i));
    //            SolveForDay(dataInput, dataOutput, vrpProblem, period.Days.ElementAt(i), cfg);
    //        }
    //    }
    //}
}
















