using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.SolverArgs
{
    public class TravelSalesmanProblemArgs : SolverArgsBase
    {
        #region Ctro

        public TravelSalesmanProblemArgs(int StartVertex)
        {
            Args.Add(StartVertex);
        }

        #endregion
    }
}
