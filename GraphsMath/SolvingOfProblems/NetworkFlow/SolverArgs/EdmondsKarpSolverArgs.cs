using GraphsMath.SolvingOfProblems.SolverArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.NetworkFlow.SolverArgs
{
    public class EdmondsKarpSolverArgs<TVertexKey> : SolverArgsBase
    {
        #region ctor
        public EdmondsKarpSolverArgs(TVertexKey start, TVertexKey end)
        {
            Args.Add(start);

            Args.Add(end);
        }
        #endregion
    }
}
