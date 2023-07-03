using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.SolverArgs
{
    public class BridgeDetectorArgs<TVertexKey> : SolverArgsBase
    {
        #region Ctor
        public BridgeDetectorArgs(TVertexKey initParent)
        {
            Args.Add(initParent);
        }
        #endregion
    }
}
