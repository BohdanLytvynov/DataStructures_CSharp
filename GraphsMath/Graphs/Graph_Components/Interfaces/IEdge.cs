using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.Graphs.Graph_Components.Interfaces
{
    public interface IEdge<TVertexKey, TWeight>
    {
        #region Properties

        public TVertexKey From { get; }

        public TVertexKey To { get; }

        public TWeight Weight { get; }

        #endregion

        #region Methods

        #endregion
    }
}
