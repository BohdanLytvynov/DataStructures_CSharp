using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.CustomExceptons
{
    public class NoEdgesFoundException : Exception
    {
        public NoEdgesFoundException(string msg) : base(msg)
        {

        }
    }
}
