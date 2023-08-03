using GraphsMath.SolvingOfProblems.SolverArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.NetworkFlow.SolverArgs
{
    public class Didinc_sSolverArgs<TVertexKey> : SolverArgsBase
    {
        #region Ctor
        public Didinc_sSolverArgs(TVertexKey start, TVertexKey end)
        {
            Args.Add(start);

            Args.Add(end);
        }
        #endregion
    }
}
