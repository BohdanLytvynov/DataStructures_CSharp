using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.Graphs.Graph_Components.Interfaces
{
    public interface IFlowEdge<TVertexKey, TFlowValue>
    {
        #region Properties

        public TVertexKey From { get; }

        public TVertexKey To { get; }

        public TFlowValue Flow { get; set; }

        public TFlowValue Capacity { get;}

        public IFlowEdge<TVertexKey, TFlowValue> ResidualEdge { get; set; }

        #endregion

        #region Methods
        
        bool IsResidual();

        TFlowValue GetRemainingCapacity();

        void Augment(TFlowValue bottleNeckValue);

        #endregion
    }
}
