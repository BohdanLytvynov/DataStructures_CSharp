using GraphsMath.Graphs.Graph_Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.Graphs.Graph_Components
{
    /// <summary>
    /// Base Vertex Type. Used To store Data about Vertex in a Graph
    /// TVertexKey = Unique identifier for each Vertex. Can be number or string
    /// TWeight - type of the Weight for edge in a graph
    /// </summary>
    /// <typeparam name="TVertexKey"></typeparam>
    /// <typeparam name="TWeight"></typeparam>
    public struct AdjListVertex<TVertexKey, TWeight> : IVertexData<TVertexKey, TWeight>, 
        IComparable<AdjListVertex<TVertexKey, TWeight>>,
         IEquatable<AdjListVertex<TVertexKey, TWeight>>
         where TVertexKey : IComparable<TVertexKey>, IEquatable<TVertexKey>
    {
        #region Fields

        TVertexKey m_vertex;

        TWeight m_weight;

        SortedDictionary<string, dynamic> m_AddData;

        #endregion

        #region Properties
        public TVertexKey VertexKey { get => m_vertex; }

        public TWeight Weight { get => m_weight; set => m_weight = value; }

        public SortedDictionary<string, dynamic> AddData { get => m_AddData; set => m_AddData = value; }

        #endregion

        #region Ctor
        /// <summary>
        /// TVertexKey = Unique identifier for each Vertex. Can be number or string
        /// TWeight - type of the Weight for edge in a graph
        /// adData - Dictionary for storring add data at strings keys.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <param name="adData"></param>
        public AdjListVertex(TVertexKey v, TWeight w, SortedDictionary<string, dynamic> adData)
        {
            if (adData == null)
            {
                m_AddData = new SortedDictionary<string, dynamic>();
            }
            else
            {
                m_AddData = adData;
            }

            m_vertex = v;

            m_weight = w;
        }

        public AdjListVertex(TVertexKey v, TWeight w) : this(v, w, null)
        { }

        public AdjListVertex(TVertexKey v) : this(v, default, null)
        { }

        public AdjListVertex(TVertexKey v, SortedDictionary<string, dynamic> addData) :
            this(v, default, addData)
        { }

        #endregion

        #region Interfaces Implementations

        public int CompareTo(AdjListVertex<TVertexKey, TWeight> other)
        {
            return this.m_vertex.CompareTo(other.m_vertex);
        }

        public bool Equals(AdjListVertex<TVertexKey, TWeight> other)
        {
            return this.m_vertex.Equals(other.m_vertex);
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Sets New Data for this Vertex using other one
        /// </summary>
        /// <param name="other"></param>
        public void SetNewData(IVertexData<TVertexKey, TWeight> other)
        {
            m_vertex = other.VertexKey;

            m_weight = other.Weight;

            m_AddData = other.AddData;
        }

        public override string ToString()
        {
            string str = String.Empty;

            str += $"({m_vertex}, {m_weight})";

            int count = AddData.Count();

            if (count > 0)
            {
                str += "\nAditional Data: ";
            }

            foreach (var d in AddData)
            {
                str += $"{d}\n";
            }

            return str;
        }

        public void SetWeight(TWeight w)
        {
            m_weight = w;
        }


        #endregion

        #region Opertaors

        public static implicit operator AdjListVertex<TVertexKey, TWeight>(TVertexKey v)
        {
            return new AdjListVertex<TVertexKey, TWeight>(v);
        }


        #endregion
    }
}
