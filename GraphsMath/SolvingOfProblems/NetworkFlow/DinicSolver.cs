using GraphsMath.Graphs.Graph_Components;
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
    public class DinicSolver<TVertexKey, TFlowValue> : 
        NetworkFlowProblemSolver<TVertexKey, TFlowValue>
        where TVertexKey : IEquatable<TVertexKey>, IComparable<TVertexKey>
        where TFlowValue : IEquatable<TFlowValue>, IComparable<TFlowValue>
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Ctor
        public DinicSolver(IFlowGraph<TVertexKey, TFlowValue> graph, TFlowValue initFlow):
            base(graph, initFlow)
        {

        }
        #endregion

        #region Methods

        #region Private methods
        //Builds a level Graph
        private bool BFS(TVertexKey start, TVertexKey end,
            Dictionary<TVertexKey, int> levels)
        {
            QueueLL<TVertexKey> queue = new QueueLL<TVertexKey>();

            queue.Enqueue(start);

            foreach (var k in levels.Keys)
            {
                levels[k] = -1;
            }

            levels[start] = 0;

            while (!queue.IsEmpty())
            {
                var vertex = queue.Dequeue();

                var AdjEdges = FlowGraph.GetAdjEdges(vertex);

                foreach (var e in AdjEdges)
                {
                    TFlowValue remCapacity = e.GetRemainingCapacity();

                    if (remCapacity.CompareTo(default)==1 && levels[e.To] == -1)
                    {
                        queue.Enqueue(e.To);

                        levels[e.To] = levels[vertex] + 1;
                    }
                }
            }

            return levels[end] != -1;
        }

        private TFlowValue FIndBNValueViaDFS(TVertexKey start, 
            TVertexKey end,
            Dictionary<TVertexKey, int> next,
            Dictionary<TVertexKey, int> levels,
            TFlowValue flow)
        {
            if (start.Equals(end)) return flow;

            var adjEdges = FlowGraph.GetAdjEdges(start).ToList();

            int edgeCount = adjEdges.Count;

            for (; next[start]<edgeCount; next[start]++)
            {
                //Select edge without dead end

                var edge = adjEdges[next[start]];

                TFlowValue remCapacity = edge.GetRemainingCapacity();

                if (remCapacity.CompareTo(default) == 1 && 
                    levels[edge.To] == levels[start]+1)
                {
                    TFlowValue bottleNeck = FIndBNValueViaDFS(edge.To, end, next, levels,
                        FlowGraph.SelectMinFlow(flow, remCapacity));

                    if (bottleNeck.CompareTo(default) == 1)
                    {
                        edge.Augment(bottleNeck);

                        return bottleNeck;
                    }
                }
            }

            return default;
        }

        #endregion

        public override SolverResult Solve(SolverArgsBase args = null)
        {
            Exception ex = null;

            SolverResult res = null;

            TFlowValue maxFlow = default;

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

                var verteces = FlowGraph.GetAllVerteces();

                var vertecesCount = verteces.Count();

                Dictionary<TVertexKey, int> levels = FlowGraph.BuildVertexTableWithValue<int>(-1);

                Dictionary<TVertexKey, int> next = FlowGraph.BuildVertexTableWithValue<int>(0);

                while (BFS(start, end, levels))
                {                    
                    for (TFlowValue flow = FIndBNValueViaDFS(start, end, next, levels, InitFlowValue);
                        flow.CompareTo(default) != 0; 
                        flow = FIndBNValueViaDFS(start, end, next, levels, InitFlowValue))
                    {
                        maxFlow += (dynamic)flow;   
                    }

                    foreach (var k in next.Keys)
                    {
                        next[k] = 0;
                    }
                }
            }
            catch (Exception e)
            {
                ex = e;
                throw;
            }

            res = new SolverResult("Dinic Algorithm", 
                new List<object>() { maxFlow }, ex!=null? true:false, ex);

            return res;
        }
        #endregion
    }
}
