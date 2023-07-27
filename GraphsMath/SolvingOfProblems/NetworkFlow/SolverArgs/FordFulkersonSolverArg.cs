using GraphsMath.SolvingOfProblems.SolverArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.NetworkFlow.SolverArgs
{
    public class FordFulkersonSolverArg<TVertexKey> : SolverArgsBase
    {
        #region Ctor
        public FordFulkersonSolverArg(TVertexKey start, TVertexKey end)
        {
            Args.Add(start);

            Args.Add(end);
        }
        #endregion
    }
}
