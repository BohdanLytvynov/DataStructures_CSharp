using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.SolverArgs
{
    public class ShortestPathProblemForWDAGArgs<TVertexKey> : SolverArgsBase
    {
        #region Ctor
        public ShortestPathProblemForWDAGArgs(TVertexKey start)
        {
            Args.Add(start);
        }
        #endregion
    }
}
