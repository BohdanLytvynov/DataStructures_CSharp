using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.CustomExceptons
{
    public class ArgumentsNotSetException : Exception
    {
        #region Ctor

        public ArgumentsNotSetException(string msg):base(msg)
        {

        }

        #endregion
    }
}
