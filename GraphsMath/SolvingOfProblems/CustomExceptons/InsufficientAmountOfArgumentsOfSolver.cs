using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.CustomExceptons
{
    public class InsufficientAmountOfArgumentsForSolverException : Exception
    {
        #region Ctor
        public InsufficientAmountOfArgumentsForSolverException(string msg):base(msg)
        {

        }
        #endregion
    }
}
