using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.CustomExceptons
{
    public class EmptyGraphException : Exception
    {
        #region Ctor

        public EmptyGraphException(string msg) : base(msg)
        {

        }

        #endregion
    }
}
