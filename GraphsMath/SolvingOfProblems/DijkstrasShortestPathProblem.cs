using GraphsMath.Graphs.Interfaces;
using GraphsMath.SolvingOfProblems.SolverArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems
{
    public class DijkstrasShortestPathProblem<TVertexType, TVertexKey, TWeight> :
        Problem_Solver<TVertexType, TVertexKey, TWeight>
        where TVertexKey : IEquatable<TVertexKey>, IComparable<TVertexKey>
        where TVertexType : IEquatable<TVertexType>, IComparable<TVertexType>
    {
        #region Fields

        TVertexKey m_nullValue;

        TWeight m_defValue;

        #endregion

        #region Methods

        public List<TVertexType> ReconsructPath(TVertexType start, TVertexType end,
            Dictionary<TVertexKey, TVertexKey> prevDictionary)            
        {
            List<TVertexType> path = new List<TVertexType>();

            if (prevDictionary.Count > 0)
            {
                var key = Graph.GetVertexKeyFromVertex(end);

                for (TVertexKey item = key; !item.Equals(m_nullValue);
                    item = prevDictionary[item])
                {
                    path.Add(Graph[item]);
                }
            }

            path.Reverse();

            if (path[0].Equals(start))
            {
                return path;
            }
            else
            {
                return new List<TVertexType>() { };
            }

            
        }

        #endregion

        #region Ctor

        public DijkstrasShortestPathProblem(IGraph<TVertexType, TVertexKey, TWeight> g
            , TVertexKey nullValue, TWeight defValue) :
            base(g)
        {
            m_nullValue = nullValue;
            m_defValue = defValue;
        }

        public override SolverResult Solve(SolverArgsBase args)
        {            
            SolverResult res = null;

            Exception ex = null;

            Dictionary<TVertexKey, TVertexKey> prevDic =
                    new Dictionary<TVertexKey, TVertexKey>();

            Dictionary<TVertexKey, TWeight> distDictionary =
                new Dictionary<TVertexKey, TWeight>();

            try
            {
                TVertexKey start = (TVertexKey)args.Args[0];

                var visitDic = Graph.InitializeVisitDS();
                
                var verteces = Graph.GetVerteces();

                PriorityQueue<TVertexKey, TWeight> pq =
                    new PriorityQueue<TVertexKey, TWeight>();

                foreach (var item in verteces)
                {
                    prevDic.Add(Graph.GetVertexKeyFromVertex(item), m_nullValue);

                    distDictionary.Add(Graph.GetVertexKeyFromVertex(item), m_defValue);
                }

                distDictionary[start] = m_defValue;

                pq.Enqueue(start, m_defValue);

                while (pq.Count != 0)
                {
                    var vertex = pq.Dequeue();

                    visitDic[vertex] = true;

                    var edges = Graph.GetEdges(vertex);

                    if (edges != null && edges.Count() > 0)
                    {
                        foreach (var edge in edges)
                        {
                            if (visitDic[edge.To])
                            {
                                continue;
                            }

                            //Edge Relaxation

                            dynamic newDistance = (dynamic?)distDictionary[vertex] +
                                (dynamic?)edge.Weight;

                            if (distDictionary[edge.To].Equals(m_defValue))
                            {
                                distDictionary[edge.To] = newDistance;

                                prevDic[edge.To] = vertex;

                                pq.Enqueue(edge.To, newDistance);
                            }
                            else
                            {
                                if (newDistance < distDictionary[edge.To])
                                {
                                    distDictionary[edge.To] = newDistance;

                                    prevDic[edge.To] = vertex;

                                    pq.Enqueue(edge.To, newDistance);
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

            

            res = new SolverResult("ShortestPathProblem(Dijkstra)",
                new List<object> { distDictionary, prevDic },
                ex != null? true:false, ex);

            return res;
        }

        #endregion
    }
}
