using GraphsMath.Graphs.Interfaces;
using GraphsMath.SolvingOfProblems.SolverArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems
{
    public class BridgesDetector<TVertexType, TVertexKey, TWeight> : 
        Problem_Solver<TVertexType, TVertexKey, TWeight>
        where TVertexKey : IEquatable<TVertexKey>, IComparable<TVertexKey>
    {
        #region Ctor
        public BridgesDetector(IGraph<TVertexType, TVertexKey, TWeight> g) : base(g)
        {

        }
        #endregion

        #region Methods

        private void DFSMod(TVertexKey at, 
            TVertexKey parent, Dictionary<TVertexKey, bool> visited, ref int id,
            Dictionary<TVertexKey, int> ids, Dictionary<TVertexKey, int> low_links,
            Dictionary<TVertexKey, TVertexKey> bridges )
        {
            visited[at] = true;

            id++;

            ids[at] = low_links[at] = id;

            var neighbors = Graph.GetNeighbors(at);

            foreach (var n in neighbors)
            {
                var Nkey = Graph.GetVertexKeyFromVertex(n);

                if (Nkey.Equals(parent))
                {
                    continue;
                }

                if (!visited[Nkey])
                {                    
                    DFSMod(Nkey, at, visited, ref id, ids, low_links, bridges );

                    low_links[at] = Math.Min(low_links[at], low_links[Nkey]);

                    if (ids[at] < low_links[Nkey])
                    {
                        bridges.Add(Nkey, at);                        
                    }
                }
                else
                {
                    low_links[at] = Math.Min(low_links[at], ids[Nkey]);
                }
            }

        }

        public override SolverResult Solve(SolverArgsBase args = null)
        {
            TVertexKey initParent = (TVertexKey)args.Args[0];

            SolverResult res = null;

            Exception ex = null;

            //Variables

            int id = 0;

            Dictionary<TVertexKey, TVertexKey> bridges =
                new Dictionary<TVertexKey, TVertexKey>();

            Dictionary<TVertexKey, bool> visited = Graph.InitializeVisitDS();// Visit DS

            Dictionary<TVertexKey, int> ids = new Dictionary<TVertexKey, int>();

            Dictionary<TVertexKey, int> low_links = new Dictionary<TVertexKey, int>();

            var verteces = Graph.GetVerteces();

            foreach (var verte in verteces)
            {
                ids.Add(Graph.GetVertexKeyFromVertex(verte), 0);

                low_links.Add(Graph.GetVertexKeyFromVertex(verte), 0);
            }

            try
            {
                foreach (var v in verteces)
                {
                    var vkey = Graph.GetVertexKeyFromVertex(v);

                    if (!visited[vkey])
                    {
                        DFSMod(vkey, initParent, visited, ref id,
                            ids, low_links, bridges);
                    }
                }
            }
            catch (Exception e)
            {
                ex = e;                
            }

            res = new SolverResult("Bridges Detection", new List<object>() { bridges },
                ex != null? true:false, ex);

            return res;

        }

        #endregion
    }
}
