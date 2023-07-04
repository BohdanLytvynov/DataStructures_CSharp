using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.SolverArgs
{
    public class ArticulationPointDetectorArgs<TVertexKey> : SolverArgsBase
    {
        #region Ctor
        public ArticulationPointDetectorArgs(TVertexKey initParent)
        {
            Args.Add(initParent);
        }
        #endregion
    }
}
