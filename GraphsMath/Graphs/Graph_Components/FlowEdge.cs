using GraphsMath.Graphs.Graph_Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.Graphs.Graph_Components
{
    public class FlowEdge<TVertexKey, TFlowValue> : IFlowEdge<TVertexKey, TFlowValue>
        where TVertexKey : IEquatable<TVertexKey>, IComparable<TVertexKey>
    {
        #region Fields
        TVertexKey m_From;

        TVertexKey m_To;

        TFlowValue m_Flow;

        TFlowValue m_Capacity;

        IFlowEdge<TVertexKey, TFlowValue> m_ResidualEdge;
        #endregion

        #region Properties
        public TVertexKey From { get=> m_From; }

        public TVertexKey To { get=> m_To; }

        public TFlowValue Flow { get => m_Flow; set => m_Flow = value; }

        public TFlowValue Capacity { get=> m_Capacity; }

        public IFlowEdge<TVertexKey, TFlowValue> ResidualEdge { get => m_ResidualEdge;
            set => m_ResidualEdge = value;
        }
        #endregion

        #region Ctor
        public FlowEdge(TVertexKey from, TVertexKey to, TFlowValue capacity)
        {
            m_From = from;

            m_To = to;
           
            m_Capacity = capacity;
        }
        #endregion

        #region Methods
        public void Augment(TFlowValue bottleNeckValue)
        {
            m_Flow += (dynamic)bottleNeckValue;

            m_ResidualEdge.Flow -= (dynamic)bottleNeckValue;
        }

        public TFlowValue GetRemainingCapacity()
        {
            return m_Capacity -= (dynamic)m_Flow;
        }

        public bool IsResidual()
        {
            return m_Capacity.Equals(0);
        }

        public override string ToString()
        {
            return $"{From} -> {To} F: {Flow} C: {Capacity} R?: {IsResidual()}"; 
        }


        #endregion

    }
}
