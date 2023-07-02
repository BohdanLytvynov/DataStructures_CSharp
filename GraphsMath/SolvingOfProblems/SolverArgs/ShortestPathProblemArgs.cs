
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.SolverArgs
{
    public class ShortestPathProblemArgs<TVertexKey> : SolverArgsBase
    {
        public ShortestPathProblemArgs(TVertexKey start)
        {
            Args.Add(start);
        }
    }
}
