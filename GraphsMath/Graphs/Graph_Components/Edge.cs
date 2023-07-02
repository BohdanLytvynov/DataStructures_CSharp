using GraphsMath.Graphs.Graph_Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.Graphs.Graph_Components
{
    public struct Edge<TVertexKey, TWeight> : IEdge<TVertexKey, TWeight>,
        IComparable<Edge<TVertexKey, TWeight>>, IEquatable<Edge<TVertexKey, TWeight>>
        where TVertexKey : IEquatable<TVertexKey>, IComparable<TVertexKey>        
    {
        #region Fields

        TVertexKey m_from;

        TVertexKey m_to;

        TWeight m_weight;

        #endregion

        #region Properties

        public TVertexKey From { get=> m_from;}

        public TVertexKey To { get => m_to;}

        public TWeight Weight { get => m_weight; }

        #endregion

        #region Ctor
        public Edge(TVertexKey from, TVertexKey to, TWeight weight)
        {
            m_from = from;

            m_to = to;

            m_weight = weight;
        }
        #endregion

        #region Methods

        public override string ToString()
        {
            return $"From: {m_from} To: {m_to} with weight: {m_weight}"; 
        }

        public int CompareTo(Edge<TVertexKey, TWeight> other)
        {
            int res = 0;

            if ((m_from.CompareTo(other.m_from) == 0) && (m_from.CompareTo(other.m_from) == 0))
            {
                res = 0;
            }
            else if ((m_from.CompareTo(other.m_from) == 1) && (m_from.CompareTo(other.m_from) == 1))
            {
                res = 1;
            }
            else if ((m_from.CompareTo(other.m_from) == -1) && (m_from.CompareTo(other.m_from) == -1))
            {
                res = -1;
            }

            return res;
        }

        public bool Equals(Edge<TVertexKey, TWeight> other)
        {
            return m_from.Equals(other.m_from) && m_to.Equals(other.m_to);
        }

        #endregion
    }
}
