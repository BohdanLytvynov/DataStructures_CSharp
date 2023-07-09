using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.CustomExceptons
{
    public class StartVertexNotSetException : Exception
    {
        #region Ctor

        public StartVertexNotSetException(string msg): base(msg)
        {

        }

        #endregion
    }
}
