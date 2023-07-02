
using GraphsMath.Graphs.Interfaces;
using GraphsMath.SolvingOfProblems;

using GraphsMath.SolvingOfProblems.SolverArgs;

using System.Net.Http.Headers;

namespace GraphsMath.SolvingOfProblems.ShortestPathProblem
{
    public class ShortestPath_Problem<TVertexType, TVertexKey, TWeight> 
        : Problem_Solver<TVertexType, TVertexKey, TWeight>
        where TVertexKey : IComparable<TVertexKey>, IEquatable<TVertexKey>
        where TVertexType : IEquatable<TVertexType>, IComparable<TVertexType>

         
    {
        #region fields

        TVertexType m_empty;

        #endregion

        #region Ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="SetPrevArrayGenerator"></param>
        /// <param name="GetCurrent_Prev_Verteces"></param>
        /// <param name="PathReconstructor"></param>
        public ShortestPath_Problem(IGraph<TVertexType, TVertexKey, TWeight> g,
            TVertexType emptyValue) : base(g)
        {
           m_empty = emptyValue;
        }

        public List<TVertexType> ReconstructPath(TVertexType start, TVertexType end, 
            Dictionary<TVertexType, TVertexType> prev)
        {
            List<TVertexType> path = new List<TVertexType>();

            for (TVertexType item = end; !(item.CompareTo(m_empty) == 0); item = prev[item] )
            {
                path.Add(item);
            }

            path.Reverse();

            if (path.Count == 0)
            {
                return new List<TVertexType>();
            }

            if (path[0].CompareTo(start) == 0)//Path exists
            {
                return path;
            }
            else
            {
                return new List<TVertexType>();
            }
        }
       
        /// <summary>
        /// args - List for arguments. Pass here the Vertex from we start iteration which.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override SolverResult Solve(SolverArgsBase args)
        {
            TVertexKey Startvertex = (TVertexKey)args.Args[0];

            Dictionary<TVertexType, TVertexType> prev = 
                new Dictionary<TVertexType, TVertexType>(); ;

            Exception ex = null;

            try
            {
                var verteces = Graph.GetVerteces();

                foreach (var item in verteces)
                {
                    prev.Add(item, m_empty);
                }

                Graph.BFS(Startvertex,
                        (c, n) =>
                        {
                            prev[n] = c;

                            return true;
                        }
                        );                                                        
            }
            catch (Exception e)
            {

                ex = e;
            }
            
            SolverResult r = new SolverResult("ShortestPath_Problem",
                new List<object>() { prev }, ex == null? false: true, ex );

            return r;

        }
        #endregion
    }
}