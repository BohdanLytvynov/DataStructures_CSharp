using GraphsMath.Graphs.AMGraphs;
using GraphsMath.Graphs.Interfaces;
using GraphsMath.SolvingOfProblems.SolverArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems
{
    public class FLoydWarshal<TVertexType, TVertexKey, TWeight>:
        Problem_Solver<TVertexType, TVertexKey, TWeight>
        where TVertexKey : IEquatable<TVertexKey>, IComparable<TVertexKey>
    {
        #region Fields
        TWeight m_NegCycleValue;

        //TWeight m_NoPathValue;
        #endregion

        #region Ctor
        public FLoydWarshal(IGraph<TVertexType, TVertexKey, TWeight> g, TWeight negativeCycleValue) : base(g)
        {
            m_NegCycleValue = negativeCycleValue;
            //m_NoPathValue = noPathValue;
        }

        #endregion

        #region Methods

        public IEnumerable<int> ReconstructPath(int start, int end, int[,] next, TWeight[,] dc)
        {
            List<int> path = new List<int>();

            var emptyalue = (Graph as AdjacentMatrixGraph<TWeight>).NoEdgeValue;

            if (dc[start, end].Equals(emptyalue) || start.Equals(end))
            {
                return path;
            }

            int vertex = start;

            for (; !vertex.Equals(end) ; vertex = next[vertex, end] )
            {
                if (vertex == -1)
                {
                    return new List<int>() { };
                }

                path.Add(vertex);
            }

            if (next[vertex, end] == -1)
            {
                return new List<int>() { };
            }

            path.Add(end);

            return path;
        }

        public override SolverResult Solve(SolverArgsBase args = null)
        {
            //Deep copy of graph's matrix

            SolverResult res = null;

            Exception ex = null;

            bool DetectNegCycles = (bool)args.Args[0];

            int count = Graph.VertexCount;

            var dc = new TWeight[count, count];//Copy of the graph's matrix Will use this one

            var next = new int[count, count];

            var matrix = (Graph as AdjacentMatrixGraph<TWeight>).AdjMatrix;

            var emptyalue = (Graph as AdjacentMatrixGraph<TWeight>).NoEdgeValue;

            try
            {                
                for (int i = 0; i < count; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        dc[i, j] = matrix[i, j];

                        if (!matrix[i, j].Equals(emptyalue))
                        {
                            next[i, j] = j;
                        }
                        else
                        {
                            next[i, j] = -1;
                        }
                    }
                }

                ///Main Cycle
               
                for (int k = 0; k < count; k++)//External iteration cycle
                {                   
                    for (int i = 0; i < count; i++)
                    {
                        if (i == k)
                        {
                            continue;
                        }

                        for (int j = 0; j < count; j++)
                        {
                            if (j == k)
                            {
                                continue;
                            }

                            dynamic newWeight = (dynamic?)dc[i, k] + (dynamic?)dc[k, j];

                            if ((dynamic?)dc[i, j] > newWeight)
                            {
                                dc[i, j] = newWeight;

                                next[i, j] = next[i, k];
                            }
                        }
                    }
                }

                if (DetectNegCycles)
                {
                    for (int k = 0; k < count; k++)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            for (int j = 0; j < count; j++)
                            {
                                dynamic newWeight = (dynamic?)dc[i, k] + (dynamic?)dc[k, j];

                                if ((dynamic?)dc[i, j] > newWeight)
                                {
                                    dc[i, j] = m_NegCycleValue;

                                    next[i, j] = -1;
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

            res = new SolverResult("FLoydWarshal", new List<object>() { dc, next },
                ex != null? true:false, ex);

            return res;
        }

        #endregion
    }
}
