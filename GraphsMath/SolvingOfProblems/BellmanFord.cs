using GraphsMath.Graphs.Interfaces;
using GraphsMath.SolvingOfProblems.SolverArgs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems
{
    public class BellmanFord<TVertexType, TVertexKey, TWeight> :
        Problem_Solver<TVertexType, TVertexKey, TWeight>
        where TVertexType : IEquatable<TVertexType>, IComparable<TVertexType>
        where TVertexKey : IEquatable<TVertexKey>, IComparable<TVertexKey>
    {
        #region Fields

        TWeight m_NoEdgeValue;

        TWeight m_NegativeCycleValue;

        #endregion

        public BellmanFord(IGraph<TVertexType, TVertexKey, TWeight> g, TWeight NoEdgeValue, 
            TWeight negativeCycleValue) : base(g)
        {
            m_NoEdgeValue = NoEdgeValue;
            m_NegativeCycleValue = negativeCycleValue;
        }

        public void FindNegativeCycles(Dictionary<TVertexKey, TWeight> distDictonary)
        {
            //Get Edge List:

            var edges = Graph.GetAllGraphEdges();

            var verteces = Graph.GetVerteces();

            int count = verteces.Count();

            int i = 0;

            //Negative Cycles Detection

            while (i < count)// V - 1 iterations
            {
                foreach (var edge in edges)
                {
                    //Calculate new Weight
                    dynamic newWeignt = (dynamic)distDictonary[edge.From] + (dynamic)edge.Weight;

                    if (distDictonary[edge.To] > newWeignt)
                    {
                        distDictonary[edge.To] = m_NegativeCycleValue;
                    }
                }

                i++;
            }


        }

        public override SolverResult Solve(SolverArgsBase args = null)
        {
            Exception ex = null;

            SolverResult res = null;

            Dictionary<TVertexKey, TWeight> distDictonary = new Dictionary<TVertexKey, TWeight>();

            try
            {
                //Get Edge List:

                var edges = Graph.GetAllGraphEdges();

                var verteces = Graph.GetVerteces();
                
                //Set all values to positive infinity first

                foreach (var vertex in verteces)
                {
                    distDictonary.Add(Graph.GetVertexKeyFromVertex(vertex), m_NoEdgeValue);
                }

                int i = 0;

                int count = verteces.Count();

                //Calculate DistDictironary

                while (i < count)// v - 1 iterations
                {
                    foreach (var edge in edges)
                    {
                        dynamic newWeight = (dynamic)distDictonary[edge.From] + (dynamic)edge.Weight;

                        if (distDictonary[edge.To].Equals(m_NoEdgeValue))
                        {
                            distDictonary[edge.To] = 
                                newWeight.Equals(m_NoEdgeValue)? edge.Weight: newWeight;
                        }
                        else
                        {
                            if (newWeight < distDictonary[edge.To])
                            {
                                distDictonary[edge.To] = newWeight;
                            }                            
                        }
                    }

                    i++;
                }
                              
            }
            catch (Exception e)
            {
                ex = e;
            }

            res = new SolverResult("BellmandFord", 
                new List<object>() { distDictonary }, ex != null? true:false, ex);

            return res;
        }
    }
}
