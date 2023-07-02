using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.Graphs.MultiDimGrid
{
    public class IncorrectAmountOfDimensionsException : Exception
    {
        public IncorrectAmountOfDimensionsException(string msg):base(msg)
        {

        }
    }
}
