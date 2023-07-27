using GraphsMath.Graphs.Graph_Components.Interfaces;
using GraphsMath.Graphs.Interfaces;
using GraphsMath.SolvingOfProblems.CustomExceptons;
using GraphsMath.SolvingOfProblems.SolverArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.NetworkFlow
{
    public class FordFulkersonNetworkFlow<TVertexKey, TFLowValue> :
        NetworkFlowProblemSolver<TVertexKey, TFLowValue> 
        where TVertexKey : IEquatable<TVertexKey>, IComparable<TVertexKey>
        where TFLowValue : IEquatable<TFLowValue>, IComparable<TFLowValue>
    {
        #region Fields
        TFLowValue m_InitFlowValue;
        #endregion

        #region Ctor
        public FordFulkersonNetworkFlow(IFlowGraph<TVertexKey, TFLowValue> graph, 
            TFLowValue initFlowValue) : base(graph)
        {
            m_InitFlowValue = initFlowValue;
        }
        #endregion

        #region Methods

        #region Private Methods

        private TFLowValue SelectMinFlow(TFLowValue f1, TFLowValue f2)
        {
            if (f1.CompareTo(f2) == -1) //f1 < f2
            {
                return f1;
            }
            else
            {
                return f2;
            }
        }


        private TFLowValue CalculateBNValueviaDFS(TVertexKey verstex, 
            TVertexKey end, TFLowValue flow, Dictionary<TVertexKey, int> visited, 
            ref int visitToken)
        {
            TFLowValue bottleNeck = default;

            if (verstex.Equals(end)) return flow; //Base case to stop the reccurtion

            //Mark current vertex as visited

            visited[verstex] = visitToken;

            //Get neighbor Edges

            var edges = FlowGraph.GetAdjEdges(verstex);

            IFlowEdge<TVertexKey, TFLowValue> currentEdge = null;

            //Push the flow through the edge
            foreach (var e in edges)
            {
                currentEdge = e;

                if ((dynamic)e.GetRemainingCapacity() > (dynamic)0 && visited[e.To] != visitToken
                    && !e.IsResidual())
                {
                    bottleNeck = CalculateBNValueviaDFS(e.To, end, SelectMinFlow(flow, 
                        e.GetRemainingCapacity()), visited, ref visitToken);
                }
            }

            if (bottleNeck.CompareTo((dynamic)0) == 1)
            {
                currentEdge.Augment(bottleNeck);                
            }

            return bottleNeck;
        }

        #endregion

        public override SolverResult Solve(SolverArgsBase args = null)
        {                        
            Exception ex = null;

            SolverResult res = null;

            int visitToken = -1;

            TFLowValue maxFlow = default;

            try
            {
                if (args == null)
                    throw new ArgumentsNotSetException("Arguments for Solver are not set!!");

                TVertexKey start = args.Args[0] == null ?
                    throw new InsufficientAmountOfArgumentsForSolverException("Start vertex is not Set!"):
                    (TVertexKey)args.Args[0];

                TVertexKey end = args.Args[1] == null ?
                    throw new InsufficientAmountOfArgumentsForSolverException("End vertex is not Set!") :
                    (TVertexKey)args.Args[1];
                                
                var visited = FlowGraph.CreateVisitDS(visitToken);

                visitToken++;

                //Main Solver

                for (TFLowValue f = CalculateBNValueviaDFS(start, end, m_InitFlowValue, visited, ref visitToken); 
                    !f.Equals(0); f = CalculateBNValueviaDFS(start, end, m_InitFlowValue, visited, ref visitToken))
                {
                    visitToken++;

                    maxFlow += (dynamic)f;
                }

            }
            catch (Exception e)
            {
                ex = e;
            }
            finally
            {
                res = new SolverResult("FordFulkersonNetworkFlow", 
                    new List<object>() { maxFlow }, ex != null? true: false, ex);
            }

            return res;
        }
        #endregion
    }
}
