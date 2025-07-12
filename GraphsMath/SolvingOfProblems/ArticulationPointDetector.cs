using GraphsMath.Graphs.Interfaces;
using GraphsMath.SolvingOfProblems.SolverArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems
{
    public class ArticulationPointDetector<TVertexType, TVertexKey, TWeight> : 
        Problem_Solver<TVertexType, TVertexKey, TWeight>
        where TVertexKey : IEquatable<TVertexKey>, IComparable<TVertexKey>
    {
        #region Fields

        #endregion

        #region Ctor
        public ArticulationPointDetector(IGraph<TVertexType, TVertexKey, TWeight> g): base(g)
        {

        }
        #endregion

        #region Methods

        private void DFSMod(TVertexKey root, TVertexKey at, TVertexKey parent,
            ref int id, ref int outEdgeCount, Dictionary<TVertexKey, bool> visit,
            Dictionary<TVertexKey, int> ids,
            Dictionary<TVertexKey, int> low_links,
            Dictionary<TVertexKey, bool> isArt)
        {
            if (parent.Equals(root))
            {
                outEdgeCount++;
            }

            visit[at] = true;

            id++;

            ids[at] = low_links[at] = id;

            var neighbors = Graph.GetNeighbors(at);

            foreach (var n in neighbors)
            {
                var k = Graph.GetVertexKeyFromVertex(n);

                if (parent.Equals(k))
                {     
                    continue;
                }

                if (!visit[k])
                {                    
                    DFSMod(root, k ,at, ref id, ref outEdgeCount, visit, ids, low_links, 
                        isArt);

                    low_links[at] = Math.Min(low_links[at], low_links[k]);

                    if (ids[at] <= low_links[k])
                    {
                        isArt[at] = true;
                    }
                }
                else
                {
                    low_links[at] = Math.Min(ids[k], low_links[at]);
                }
            }

        }

        public override SolverResult Solve(SolverArgsBase args = null)
        {
            SolverResult res = null;

            TVertexKey initParent = (TVertexKey)args.Args[0];

            Exception ex = null;

            Dictionary<TVertexKey, bool> visit = Graph.InitializeVisitDS();

            Dictionary<TVertexKey, bool> IsArticulationPoint = Graph.InitializeVisitDS();

            Dictionary<TVertexKey, int> ids = new Dictionary<TVertexKey, int>();

            Dictionary<TVertexKey, int> low_links = new Dictionary<TVertexKey, int>();

            int outEdgeCount = 0;

            int id = 0;

            var verteces = Graph.GetVerteces();

            foreach (var v in verteces)
            {
                var k = Graph.GetVertexKeyFromVertex(v);

                ids.Add(k, 0);

                low_links.Add(k, 0);
            }

            try
            {
                foreach (var v in verteces)
                {
                    var k = Graph.GetVertexKeyFromVertex(v);

                    if (!visit[k])
                    {
                        outEdgeCount = 0;

                        DFSMod(k,k,initParent,ref id, ref outEdgeCount,
                            visit, ids, low_links,
                            IsArticulationPoint);

                        IsArticulationPoint[k] = (outEdgeCount > 1);
                    }
                }
            }
            catch (Exception e)
            {
                ex = e;                
            }

            res = new SolverResult("Articulation_Point_Detection", new List<object>()
            { IsArticulationPoint }, ex!=null? true:false, ex);

            return res;
        }
        #endregion
    }
}
