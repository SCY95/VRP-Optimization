using System;
using System.Collections.Generic;
using System.Text;
using Google.OrTools.ConstraintSolver;
using System.Diagnostics;

namespace VrpTest
{
    public interface IDataOutput
    {
        void PrintSolution(in DataModel data,
            in RoutingModel routing,
            in RoutingIndexManager manager,
            in Assignment solution);

        int PrintStatus(RoutingModel routing);
    }
}
