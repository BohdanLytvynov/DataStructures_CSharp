using GraphsMath.Graphs.AMGraphs;
using GraphsMath.Graphs.Interfaces;
using GraphsMath.SolvingOfProblems.CustomExceptons;
using GraphsMath.SolvingOfProblems.SolverArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.SolvingOfProblems
{
    public class TravelSalesmanProblem<TWeight> : Problem_Solver<int, int, TWeight>
    {
        #region Fields

        TWeight m_DistNotSet;

        #endregion

        #region Ctor
        public TravelSalesmanProblem(IGraph<int, int, TWeight> graph, TWeight distNotSet) : base(graph)
        {
            m_DistNotSet = distNotSet;
        }
        #endregion

        #region Methods

        #region Private Methods

        private TWeight[,] CreateAndSetupMemoMatrix(TWeight[,] sourceMatrix, int startVertex)
        {
            //Get sourse matrix dimensions
            var rowCount = Graph.VertexCount == 0 ?
                throw new EmptyGraphException("There is no bounds in a Graph!") : Graph.VertexCount;

            var memoMatrix = new TWeight[rowCount, 1 << (int)rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                if (i == startVertex) continue; //We need to skip start node

                memoMatrix[i, (1 << startVertex) | (1 << i)] = sourceMatrix[startVertex, i];
            }

            return memoMatrix;
        }

        private void Combinator(int bitNumber, int shift, int amountOf1, 
            int countOfDigits, List<int> bits)
        {
            if (amountOf1 == 0) // Base case
                bits.Add(bitNumber);
            else
                for (int i = shift; i < countOfDigits; i++)
                {
                    bitNumber |= 1 << i;

                    Combinator(bitNumber, i + 1, amountOf1 - 1, countOfDigits, bits);

                    bitNumber &= ~(1 << i);
                }
        }

        private IEnumerable<int> CreateBitCombinations(int r, int VertexCount)
        {
            List<int> bits = new List<int>();

            Combinator(0, 0, r, VertexCount, bits);

            return bits;
        }

        private bool NotInSubset(int i, int subset)
        {
            return ((1 << i) & subset) == 0;
        }

        private void Solve(TWeight[,] matrix, TWeight[,] memoMatrix, int startVertex)
        {
            int count = Graph.VertexCount == 0 ?
                throw new EmptyGraphException("There is no bounds in a Graph!") : Graph.VertexCount; ;

            for (int r = 3; r <= count; r++)
            {
                foreach (var bitsSet in CreateBitCombinations(r, count))
                {
                    if (NotInSubset(startVertex, bitsSet)) continue;

                    for (int next = 0; next < count; next++)
                    {
                        if (next == startVertex || NotInSubset(next, bitsSet)) continue;

                        var state = bitsSet ^ (1 << next);

                        dynamic minDistance = m_DistNotSet;

                        for (int e = 0; e < count; e++)
                        {
                            if (e == startVertex || e == next || NotInSubset(e, bitsSet))
                                continue;

                            dynamic newDistance = (dynamic?)memoMatrix[e, state] + 
                                (dynamic?)matrix[e, next];

                            minDistance = newDistance < minDistance ? newDistance : minDistance;
                        }

                        memoMatrix[next, bitsSet] = minDistance;
                        
                    }
                }
            }
        }

        private dynamic FindCostOfMinTour(TWeight[,] memoMatrix, TWeight[,] matrix,
            int StartVertex
            )
        {
            int count = Graph.VertexCount;

            var endState = (1 << count) - 1;

            dynamic minTourCost = m_DistNotSet;

            for (int e = 0; e < count; e++)
            {
                if (e == StartVertex) continue;

                var TourCost = (dynamic?)memoMatrix[e, endState] +
                    (dynamic?)matrix[e, StartVertex];

                minTourCost = TourCost < minTourCost? TourCost : minTourCost;
            }

            return minTourCost;
        }

        private int [] FindTourOfMinCost(TWeight[,] memoMatrix, TWeight[,] matrix,
            int StartVertex)
        {
            int count = Graph.VertexCount;

            int [] tour = new int[count + 1];

            int lastIndex = StartVertex;

            var state = (1 << count) - 1;

            for (int i = count - 1; i >= 1; i--)
            {
                int index = -1;

                for (int j = 0; j < count; j++)
                {
                    if (j == StartVertex || NotInSubset(j, state)) continue;

                    index = index == -1? j : index;

                    var PrevDistance = (dynamic?)memoMatrix[index, state] + 
                        (dynamic?)matrix[index, lastIndex];

                    var NewDistance = (dynamic?)memoMatrix[j, state] +
                        (dynamic?)matrix[j, lastIndex];

                    index = NewDistance < PrevDistance ? j : index;
                }

                tour[i] = index;

                state ^= (1 << index);

                lastIndex = index;
            }

            tour[0] = tour[count] = StartVertex;

            return tour;
        }

        #endregion

        public override SolverResult Solve(SolverArgsBase args = null)
        {
            SolverResult res = null;

            Exception ex = null;

            int StartVertex = -1;

            dynamic minCostTour = 0;

            int[] path = null;

            try
            {
                StartVertex = (int)args.Args[0];

                if (StartVertex == -1)
                    throw new StartVertexNotSetException("Unable to set start Vertex!!!");

                var AdjMatrixGraph = Graph as AdjacentMatrixGraph<TWeight> == null ?
                    throw new NullReferenceException("Can't convert graph to AdjacentMфtrixGraph. Maybe Incorrect of graph type!") :
                    (AdjacentMatrixGraph<TWeight>)Graph;

                var matrix = AdjMatrixGraph.AdjMatrix ?? throw new NullReferenceException("Fail to get reference to matrix of a graph"); ;

                var memoMatrix = CreateAndSetupMemoMatrix(matrix, StartVertex);

                Solve(matrix, memoMatrix, StartVertex);

                minCostTour = FindCostOfMinTour(memoMatrix, matrix, StartVertex);

                path = FindTourOfMinCost(memoMatrix, matrix, StartVertex);
            }
            catch (Exception e)
            {
                ex = e;
            }

            res = new SolverResult("TravelSalesmanProblem",
                new List<object>() { minCostTour, path }, ex != null ? true : false, ex);

            return res;
        }
        #endregion
    }
}
