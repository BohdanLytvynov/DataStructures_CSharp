using GraphsMath.Graphs.Graph_Components;
using GraphsMath.Graphs.Interfaces;
using GraphsMath.SolvingOfProblems.SolverArgs;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems.ShortestPathProblemOnWDAG
{
    public class ShortestPathProblemOnWDAG<TVertexType, TVertexKey, TWeight> :
        Problem_Solver<TVertexType, TVertexKey, TWeight>
        where TVertexKey : IEquatable<TVertexKey>,IComparable<TVertexKey>
        where TWeight : IEquatable<TWeight>
        where TVertexType : IComparable<TVertexType>
       
    {
        #region Fields

        TWeight m_defaul;

        #endregion

        #region Ctor
        public ShortestPathProblemOnWDAG(IGraph<TVertexType, TVertexKey, TWeight> g, TWeight defaultValue): 
            base(g)
        {
            m_defaul = defaultValue;
        }
        #endregion

        public override SolverResult Solve(SolverArgsBase args = null)
        {
            SolverResult res = null;

            Exception ex = null;

            Dictionary<TVertexKey, TWeight> distDictionary = null;

            var start = (TVertexKey)args[0]; 
            //1)Peerform topological sorting
            try
            {
                var sorted = Graph.TopSort();

                distDictionary =
                    new Dictionary<TVertexKey, TWeight>();

                if (sorted != null)
                {
                    if (sorted.Count() > 0)
                    {
                        var verteces = Graph.GetVerteces();

                        foreach (var item in verteces)
                        {
                            distDictionary.Add(Graph.GetVertexKeyFromVertex(item), m_defaul);
                        }
                                                                      
                        foreach (var vertex in sorted)
                        {
                            var edges = Graph.GetEdges(Graph.GetVertexKeyFromVertex(vertex));

                            if (edges != null)
                            {
                                if (edges.Count() > 0)
                                {
                                    foreach (var edge in edges)
                                    {
                                        //Edge Relaxation
                                        dynamic newWeight = (edge.Weight as dynamic) +
                                            (distDictionary[Graph.GetVertexKeyFromVertex(vertex)] as dynamic);

                                        if (distDictionary[edge.To].Equals(m_defaul))
                                        {
                                            distDictionary[edge.To] = newWeight;
                                        }
                                        else
                                        {
                                            var arg1 = double.Parse(distDictionary[edge.To].ToString());

                                            var arg2 = double.Parse(newWeight.ToString());
                                            
                                            if (arg1 > arg2)
                                            {
                                                distDictionary[edge.To] = newWeight;
                                            }
                                        }
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                ex = e;
            }

            res = new SolverResult("ShortestPathOnWDAG", 
                new List<object>() { distDictionary },
                ex!=null? true:false, ex);

            return res;
        }
    }
}
