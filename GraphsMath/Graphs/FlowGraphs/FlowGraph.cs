using GraphsMath.Graphs.Graph_Components;
using GraphsMath.Graphs.Graph_Components.Interfaces;
using GraphsMath.Graphs.Interfaces;
using GraphsMath.SolvingOfProblems.CustomExceptons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.Graphs.FlowGraphs
{
    public class FlowGraph<TVertexKey, TFlowValue> : IFlowGraph<TVertexKey, TFlowValue>
        where TFlowValue : IComparable<TFlowValue>, IEquatable<TFlowValue>
        where TVertexKey : IComparable<TVertexKey>, IEquatable<TVertexKey>
    {
        #region Fields
        private SortedDictionary<TVertexKey, List<IFlowEdge<TVertexKey, TFlowValue>>> m_flowGraph;

        private int m_VertexCount;
        #endregion

        #region Properties
        //Here graph is represented as sorted dictionary of Vertex Keys and
        //Lists of adjacent Edges to this graph
        public SortedDictionary<TVertexKey, List<IFlowEdge<TVertexKey, TFlowValue>>> Graph
        {
            get => m_flowGraph;
        }

        public int VertexCount { get=> m_VertexCount; }

        #endregion

        #region Ctor
        public FlowGraph(List<TVertexKey> Verteces)
        {
            m_flowGraph = new SortedDictionary<TVertexKey, List<IFlowEdge<TVertexKey, TFlowValue>>>();

            foreach (var v in Verteces)
            {
                m_flowGraph.Add(v, new List<IFlowEdge<TVertexKey, TFlowValue>>());

                m_VertexCount++;
            }
        }
        #endregion

        #region Methods

        #region VisitDS Generator
        public Dictionary<TVertexKey, int> CreateVisitDS(
            int visitToken)
        {
            Dictionary<TVertexKey, int> visited = new Dictionary<TVertexKey, int>();

            if (m_VertexCount == 0)
                return visited;


            foreach (var v in Graph.Keys)
                visited.Add(v, visitToken);

            return visited;
        }
        #endregion

        #region Edges Operations

        public void AddEdge(TVertexKey from, TVertexKey to, TFlowValue capacity)
        {
            if (capacity.CompareTo((dynamic)0) == -1)
                throw new Exception("Capacity of the forward gooing edge can't be zero!!!");

            var edge = new FlowEdge<TVertexKey, TFlowValue>(from, to, capacity);

            var resEdge = new FlowEdge<TVertexKey, TFlowValue>(to, from, default);

            edge.ResidualEdge = resEdge;

            resEdge.ResidualEdge = edge;

            Graph[from].Add(edge);

            Graph[to].Add(resEdge);

        }

        public IEnumerable<IFlowEdge<TVertexKey, TFlowValue>> GetAdjEdges(TVertexKey vertex)
        {
            return Graph[vertex];
        }

        public IEnumerable<TVertexKey> GetAllVerteces()
        {
            if (m_VertexCount == 0)
                throw new EmptyGraphException("There is no verteces in the graph!");

            return Graph.Keys.ToList();
        }

        #endregion

        private string GetListItems(TVertexKey key)
        {
            var str = String.Empty;

            foreach (var v in Graph[key])
            {
                str += $"{v} ";
            }

            return str;
        }

        public override string ToString()
        {
            var str = String.Empty;

            var Keys = Graph.Keys.ToList();

            foreach (var k in Keys)
            {
                str += $"{k} --> {GetListItems(k)}\n";
            }

            return str;
        }
        #endregion


    }
}
