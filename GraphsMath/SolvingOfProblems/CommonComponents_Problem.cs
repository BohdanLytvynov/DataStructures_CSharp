using GraphsMath.Graphs.Interfaces;
using GraphsMath.SolvingOfProblems.SolverArgs;

namespace GraphsMath.SolvingOfProblems
{
    public class CommonComponents_Problem<TVertexType, TVertexKey, TWeight>
        : Problem_Solver<TVertexType, TVertexKey, TWeight>
        where TVertexKey : IEquatable<TVertexKey>, IComparable<TVertexKey>
    {
        #region Fields

        #endregion

        #region Ctor
        public CommonComponents_Problem(IGraph<TVertexType, TVertexKey, TWeight> graph) 
            : base(graph)
        {

        }

        #endregion

        public override SolverResult Solve(SolverArgsBase args = null)
        {
            int count = 0;//Coloring variable

            int vertexcount = Graph.VertexCount;

            var components = new Dictionary<TVertexType, int>();// Array repreentation of a graph 

            Exception ex = null;

            try
            {
                var visitArrayTemp = Graph.InitializeVisitDS();

                var verteces = Graph.GetVerteces();

                foreach (var item in verteces)
                {
                    components.Add(item, 0);
                }

                if (verteces?.Count() > 0)
                {                  
                    foreach (var item in verteces)
                    {
                        if (!visitArrayTemp[Graph.GetVertexKeyFromVertex(item)])
                        {
                           Graph.DFS(Graph.GetVertexKeyFromVertex(item), 
                               visitArrayTemp,
                                
                                    (v, visitDic)=>
                                    {
                                        components[v] = count;

                                        visitArrayTemp = visitDic;

                                        return true;
                                    }
                                                                 
                                );

                            count++;
                        }

                        
                    }
                }
            }
            catch (Exception e)
            {

                ex = e;
            }

            List<object> r = new List<object>() { components, count };

            SolverResult res = new SolverResult("CommonComponents_Problem", r,
                ex == null ? false : true, ex);


            return res;
        }
    }
}