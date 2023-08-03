using GraphsMath.Graphs.Graph_Components;
using GraphsMath.Graphs.Graph_Components.Interfaces;
using GraphsMath.Graphs.Interfaces;
using GraphsMath.SolvingOfProblems.CustomExceptons;
using GraphsMath.SolvingOfProblems.SolverArgs;
using QueueLinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.NetworkFlow
{
    public class EdmondsKarpNetworkFlow<TVertexKey, TFLowValue> 
        : NetworkFlowProblemSolver<TVertexKey, TFLowValue>
        where TVertexKey : IEquatable<TVertexKey>, IComparable<TVertexKey>
        where TFLowValue : IEquatable<TFLowValue>, IComparable<TFLowValue>
    {
        #region Fields
       
        #endregion

        #region Properties

        #endregion

        #region Ctor
        public EdmondsKarpNetworkFlow(IFlowGraph<TVertexKey, TFLowValue> graph,
            TFLowValue initFlowValue) : base(graph, initFlowValue)
        {

        }
        #endregion

        #region Methods

        #region Private methods

        private TFLowValue FindBVviaBFS(TVertexKey start, TVertexKey end,
            Dictionary<TVertexKey, int> visited, int visitToken)
        {
            QueueLL<TVertexKey> queue = new QueueLL<TVertexKey>();

            visited[start] = visitToken;

            queue.Enqueue(start);

            //Prepare Disctionary for path reconstruction

            Dictionary<TVertexKey, IFlowEdge<TVertexKey, TFLowValue>> prevPathDic =
                FlowGraph.BuildVertexTableWithValue<IFlowEdge<TVertexKey, TFLowValue>>(null);
                                          
            while (!queue.IsEmpty())
            {
                //Get vertex from Queue

                var vertex = queue.Dequeue();

                if (vertex.Equals(end)) break;

                var adjEdges = FlowGraph.GetAdjEdges(vertex);

                foreach (var edge in adjEdges)
                {
                    TFLowValue remCapacity = edge.GetRemainingCapacity();

                    if (remCapacity.CompareTo(default) == 1 && 
                        !visited[edge.To].Equals(visitToken))
                    {
                        visited[edge.To] = visitToken;

                        prevPathDic[edge.To] = edge;

                        queue.Enqueue(edge.To);
                    }
                }
            }

            if (prevPathDic[end] == null)//No path wa found 
                return default;

            //Calculate bottleneck using path reconstruction

            TFLowValue bottleNeck = InitFlowValue;

            for (var edge = prevPathDic[end]; edge != null; edge = prevPathDic[edge.From])
            {
                bottleNeck = FlowGraph.SelectMinFlow(bottleNeck, edge.GetRemainingCapacity());
            }

            //Augment the path

            for (var edge = prevPathDic[end]; edge != null; edge = prevPathDic[edge.From])
            {
                edge.Augment(bottleNeck);
            }

            return bottleNeck;
        }

        #endregion

        public override SolverResult Solve(SolverArgsBase args = null)
        {
            Exception ex = null;

            SolverResult res = null;

            int visitToken = 0;

            TFLowValue maxFlow = default;

            try
            {
                if (args == null)
                    throw new ArgumentsNotSetException("Arguments for Solver are not set!!");

                TVertexKey start = args.Args[0] == null ?
                    throw new InsufficientAmountOfArgumentsForSolverException("Start vertex is not Set!") :
                    (TVertexKey)args.Args[0];

                TVertexKey end = args.Args[1] == null ?
                    throw new InsufficientAmountOfArgumentsForSolverException("End vertex is not Set!") :
                    (TVertexKey)args.Args[1];

                var visited = FlowGraph.CreateVisitDS(visitToken);

                TFLowValue flow = default;

                do
                {
                    visitToken++;

                    flow = FindBVviaBFS(start, end, visited, visitToken);

                    maxFlow += (dynamic)flow;

                } while (flow.CompareTo(default) != 0);
            }
            catch (Exception e)
            {
                ex = e;
            }
            finally
            {
                res = new SolverResult("EdmondKarpMaxFlow",
                new List<object>() { maxFlow }, ex != null ? true : false, ex);
            }
            

            return res;
        }
        #endregion
    }
}
