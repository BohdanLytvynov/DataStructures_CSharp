using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.SolverArgs
{
    public class FloydWarshalArgs : SolverArgsBase
    {
        public FloydWarshalArgs(bool detectNegativeCycles)
        {
            Args.Add(detectNegativeCycles);
        }
    }
}
