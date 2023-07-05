using GraphsMath.Graphs.Interfaces;
using GraphsMath.SolvingOfProblems.SolverArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems
{
    public class StronglyConnectedComponentsDetector<TVertexType, TVertexKey, TWeight> 
        : Problem_Solver<TVertexType, TVertexKey, TWeight>
        where TVertexKey : IEquatable<TVertexKey>, IComparable<TVertexKey>
    {
        #region Fields

        #endregion

        #region Ctor
        public StronglyConnectedComponentsDetector(IGraph<TVertexType, TVertexKey, TWeight> g)
            : base(g)
        {

        }
        #endregion

        #region Methods

        private void DFS(TVertexKey at, ref int id, Dictionary<TVertexKey, bool> visit,
            Dictionary<TVertexKey, int> low_link,
            Dictionary<TVertexKey, int> ids,
            Dictionary<TVertexKey, bool> onStack,
            Stack<TVertexKey> stack, int unvisited, ref int SCCCount)
        {
            id++;

            visit[at] = true;

            stack.Push(at);

            onStack[at] = true;

            ids[at] = low_link[at] = id;

            var neighbors = Graph.GetNeighbors(at);

            foreach (var v in neighbors)
            {
                var k = Graph.GetVertexKeyFromVertex(v);

                if (ids[k].Equals(unvisited))
                {
                    DFS(k, ref id, visit, low_link, ids, onStack, stack, unvisited,
                        ref SCCCount);
                }

                if (onStack[k])
                {
                    low_link[at] = Math.Min(low_link[at], low_link[k]);
                }
            }

            if (ids[at].Equals(low_link[at]))
            {
                for (var v = stack.Pop(); ; v = stack.Pop())
                {
                    onStack[v] = false;

                    low_link[v] = ids[at];

                    if (v.Equals(at))
                    {
                        break;
                    }

                    SCCCount++;
                }
            }

        }

        public override SolverResult Solve(SolverArgsBase args = null)
        {
            SolverResult res = null;

            Exception ex = null;

            int notVisited = -1;

            int SCCCount = 0;

            int id = 0;

            Dictionary<TVertexKey, int> ids = new Dictionary<TVertexKey, int>();

            Dictionary<TVertexKey, int> low_link = new Dictionary<TVertexKey, int>();

            Dictionary<TVertexKey, bool> visit = Graph.InitializeVisitDS();

            Dictionary<TVertexKey, bool> onStack = Graph.InitializeVisitDS();

            Stack<TVertexKey> stack = new Stack<TVertexKey>();

            try
            {
                var verteces = Graph.GetVerteces();

                foreach (var v in verteces)
                {
                    var k = Graph.GetVertexKeyFromVertex(v);

                    ids.Add(k, notVisited);

                    low_link.Add(k, 0);
                }

                foreach (var v in verteces)
                {
                    var k = Graph.GetVertexKeyFromVertex(v);

                    if (!visit[k])
                    {
                        DFS(k,ref id, visit, low_link, ids, onStack, stack, notVisited,
                            ref SCCCount);
                    }
                }
            }
            catch (Exception e)
            {
                ex = e;                
            }

            res = new SolverResult("StronglyConnectedComponentsDetector", 
                new List<object>() { low_link }, ex != null? true:false, ex);

            return res;
        }

        #endregion
    }
}
