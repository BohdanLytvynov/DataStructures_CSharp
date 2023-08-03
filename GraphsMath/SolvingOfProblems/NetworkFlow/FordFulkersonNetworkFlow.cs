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
        

        #region Ctor
        public FordFulkersonNetworkFlow(IFlowGraph<TVertexKey, TFLowValue> graph, 
            TFLowValue initFlowValue) : base(graph, initFlowValue)
        {
           
        }
        #endregion

        #region Methods

        #region Private Methods

        private double CalculateDelta()
        {
            double delta = 0;

            double i = 0;

            while ((dynamic)delta <= FlowGraph.MaxEdgeCapacity)
            {
                delta = Math.Pow(2.0, i);
                i++;
            } 

            return delta/=2;
        }


        private TFLowValue CalculateBNValueviaDFS(TVertexKey vertex, 
            TVertexKey end, TFLowValue flow, Dictionary<TVertexKey, int> visited, 
            int visitToken, double delta)
        {             
            if (vertex.Equals(end)) return flow; //Base case to stop the reccurtion

            //Mark current vertex as visited

            visited[vertex] = visitToken;

            //Get neighbor Edges

            var edges = FlowGraph.GetAdjEdges(vertex);            

            //Push the flow through the edge
            foreach (var e in edges)
            {
                TFLowValue remCapacity = e.GetRemainingCapacity();

                if ((remCapacity >= (dynamic)delta && 
                    visited[e.To] != visitToken))
                {
                    TFLowValue bottleNeck = CalculateBNValueviaDFS(e.To, end, 
                        FlowGraph.SelectMinFlow(flow, 
                        remCapacity), visited, visitToken, delta);

                    if (bottleNeck.CompareTo((dynamic)0) == 1)
                    {
                        e.Augment(bottleNeck);
                        return bottleNeck;
                    }
                }
            }            

            return default;
        }

        #endregion
        //Modified with capacity scaling method
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
                    throw new InsufficientAmountOfArgumentsForSolverException("Start vertex is not Set!"):
                    (TVertexKey)args.Args[0];

                TVertexKey end = args.Args[1] == null ?
                    throw new InsufficientAmountOfArgumentsForSolverException("End vertex is not Set!") :
                    (TVertexKey)args.Args[1];
                                
                var visited = FlowGraph.CreateVisitDS(visitToken);
                
                //Main Solver

                //Calculate delta value

                var delta = CalculateDelta();

                for (TFLowValue flow = default ; delta>0; delta/=2)
                {
                    do
                    {
                        visitToken++;//Mark all verteces unvisited

                        flow = CalculateBNValueviaDFS(start, end, InitFlowValue, visited, visitToken, delta);

                        maxFlow += (dynamic)flow;

                    } while (flow.CompareTo(default) != 0);
                }

                //for (TFLowValue f = CalculateBNValueviaDFS(start, end, InitFlowValue, visited, ref visitToken); 
                //    !f.Equals(0); f = CalculateBNValueviaDFS(start, end, InitFlowValue, visited, ref visitToken))
                //{
                //    visitToken++;

                //    maxFlow += (dynamic)f;
                //}

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
