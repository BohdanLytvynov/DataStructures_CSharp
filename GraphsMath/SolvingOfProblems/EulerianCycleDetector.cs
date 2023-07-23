using GraphsMath.Graphs.Graph_Components;
using GraphsMath.Graphs.Graph_Components.Interfaces;
using GraphsMath.Graphs.Interfaces;
using GraphsMath.SolvingOfProblems.CustomExceptons;
using GraphsMath.SolvingOfProblems.SolverArgs;
using SingleLinkeed_List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems
{
    public class EulerianCycleDetector<TVertexType, TVertexKey, TWeight>
        : Problem_Solver<TVertexType, TVertexKey, TWeight>
        where TVertexKey : IEquatable<TVertexKey>, IComparable<TVertexKey>
    {
        #region Fields

        #endregion

        #region Ctor
        public EulerianCycleDetector(IGraph<TVertexType, TVertexKey, TWeight> graph) : base(graph)
        {

        }
        #endregion

        #region Methods

        #region PrivateSection

        private void IncrementVertexDegree(Dictionary<TVertexKey, int> dictionary, TVertexKey vertex,
            int increment)
        {
            if (dictionary.ContainsKey(vertex))
                dictionary[vertex] += increment;
            else
                dictionary.Add(vertex, increment);
        }

        private void CalculateInOutDegrees(IEnumerable<IEdge<TVertexKey, TWeight>> edges,
            Dictionary<TVertexKey, int> inDegrees,
            Dictionary<TVertexKey, int> outDegrees)
        {
            var EdgeCount = edges.Count() == 0 ? throw new NoEdgesFoundException("There is no edges in a graph.")
                : edges.Count();

            foreach (var edge in edges)
            {
                IncrementVertexDegree(outDegrees, edge.From, 1);

                IncrementVertexDegree(inDegrees, edge.To, 1);
            }
        }

        private bool EulrianPathExists(IEnumerable<TVertexType> verteces,
            Dictionary<TVertexKey, int> inDegrees,
            Dictionary<TVertexKey, int> outDegrees)
        {
            int start_nodes = 0, end_nodes = 0;

            foreach (var v in verteces)
            {
                var key = Graph.GetVertexKeyFromVertex(v);

                if ((outDegrees[key] - inDegrees[key] > 1) || (inDegrees[key] - outDegrees[key] > 1))
                    //No path exists to many extra out or in edges
                    return false;
                else if (outDegrees[key] - inDegrees[key] == 1)
                    //Graph has start vertex
                    start_nodes++;
                else if (inDegrees[key] - outDegrees[key] == 1)
                    //Graph has end node
                    end_nodes++;
            }

            //There can be 2 cases:
            //either we have start point and end point or we have neither start nor end points

            return (start_nodes == 1 && end_nodes == 1) || (start_nodes == 0 && end_nodes == 0);
        }

        private TVertexKey FindStartVertex(IEnumerable<TVertexType> verteces,
            Dictionary<TVertexKey, int> inDegrees,
            Dictionary<TVertexKey, int> outDegrees)
        {
            TVertexKey start = default;

            foreach (var v in verteces)
            {
                var key = Graph.GetVertexKeyFromVertex(v);

                if (outDegrees[key] - inDegrees[key] == 1)
                    return key;

                if (outDegrees[key] > 0)
                    start = key;
            }

            return start;
        }

        //private IEdge<TVertexKey, TWeight> FindUnvisitedEdge(
        //    Dictionary<IEdge<TVertexKey, TWeight>, bool> visitedEdge,
        //    IEnumerable<IEdge<TVertexKey, TWeight>> edges)
        //{
        //    foreach (var edge in edges)
        //        if (!visitedEdge[edge])
        //            return edge;

        //    return null;
        //}

        private void DepthFirstSearch(TVertexKey startVertex, Dictionary<TVertexKey, int> outDegrees,
             LinkedSingleList<TVertexKey> path)
        {
            while (outDegrees[startVertex] != 0)//Unvisited edges
            {                
                //Select Next unvisited OutgoingEdge
                var next = Graph.GetNeighbors(startVertex).ToArray()[--outDegrees[startVertex]];

                DepthFirstSearch(Graph.GetVertexKeyFromVertex(next), outDegrees, path);
            }

            path.AddFirst(startVertex);
        }

        #endregion

        public override SolverResult Solve(SolverArgsBase args = null)
        {
            Exception ex = null;

            SolverResult result = null;

            bool pathExists = false;

            //Here we will store the founded path
            LinkedSingleList<TVertexKey> path = new LinkedSingleList<TVertexKey>();

            try
            {
                Dictionary<TVertexKey, int> inDegrees = new Dictionary<TVertexKey, int>();

                Dictionary<TVertexKey, int> outDegrees = new Dictionary<TVertexKey, int>();

                //Dictionary<IEdge<TVertexKey, TWeight>, bool> visited = new Dictionary<IEdge<TVertexKey, TWeight>, bool>();

                var verteces = Graph.GetVerteces() ?? throw new Exception("Fail to get Verteces from the graph.");

                if (verteces.Count() == 0)
                    throw new EmptyGraphException("Graph is empty!");

                var edges = Graph.GetAllGraphEdges() ?? throw new Exception("Fail to get Edges from the graph.");

                //foreach (var edge in edges)
                //{
                //    visited.Add(edge, false);
                //}

                CalculateInOutDegrees(edges, inDegrees, outDegrees);

                if (!EulrianPathExists(verteces, inDegrees, outDegrees))
                    return null;

                DepthFirstSearch(FindStartVertex(verteces, inDegrees, outDegrees), outDegrees,
                    path);

                if (path.Count == edges.Count() + 1)
                    pathExists = true;

            }
            catch (Exception e)
            {
                ex = e;
            }
            finally
            {
                result = new SolverResult("EulerianPathDetection",
                    new List<object>() { pathExists? "Eulerian path was found":"There is no Eulerian path",
                        pathExists? path:null }, ex != null ? true : false, ex);
            }

            return result;
        }
        #endregion
    }
}
