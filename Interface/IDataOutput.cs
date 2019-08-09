using System;
using System.Collections.Generic;
using System.Text;
using Google.OrTools.ConstraintSolver;
using System.Diagnostics;
using VrpTest.Struct;

namespace VrpTest
{
    public interface IDataOutput
    {
        void PrintSolution( Day day,
            RoutingModel routing,
            RoutingIndexManager manager,
            Assignment solution);

        int PrintStatus(RoutingModel routing);
    }
}
