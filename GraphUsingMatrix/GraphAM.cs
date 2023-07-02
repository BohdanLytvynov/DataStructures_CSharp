
using Graphs.Interfaces;
using QueueLinkedList;
using System.Runtime.Serialization.Formatters;

namespace GraphUsingMatrix
{
    public class GraphAM<TWeight> : IGraphAM<int, TWeight>
    {
        #region Fields

        int m_VertexCount;

        TWeight[,] m_AdjacencyMatrix;
        
        TWeight m_NoEdgeValue;        

        #endregion

        #region Properties
       
        public int VertexCount { get=> m_VertexCount; }

        public TWeight[,] AdjacencyMatrix { get => m_AdjacencyMatrix; }

        public TWeight NoEdgeValue { get=> m_NoEdgeValue; set => m_NoEdgeValue = value; }

        #endregion

        #region Ctor
        public GraphAM(int vertexCount, TWeight NoEdgeValue = default)
        {
            m_VertexCount = vertexCount;

            m_AdjacencyMatrix = new TWeight[m_VertexCount, m_VertexCount];
            
            m_NoEdgeValue = NoEdgeValue;           
        }
        #endregion

        #region Methods
        
        public void AddEdge(int start, int end, TWeight weight)
        {
            m_AdjacencyMatrix[start, end] = weight;
        }

        public void RemoveEdge(int start, int end)
        {
            m_AdjacencyMatrix[start, end] = default;
        }

        public void EditEdge(int start, int end, TWeight weight)
        {
            m_AdjacencyMatrix[start, end] = weight;
        }
        public TWeight GetWeight(int start, int end)
        {
            return m_AdjacencyMatrix[start, end];
        }

        public IEnumerable<int> GetNeighbors(int Vertex)
        {
            List<int> r = new List<int>();

            for (int i = 0; i < m_VertexCount; i++)
            {
                if (!m_AdjacencyMatrix[Vertex, i].Equals(m_NoEdgeValue))
                {
                    r.Add(i);
                }
            }

            return r;
        }

        //Depth First Search algorithm
        //This algorithm iterates graph vertexies

        public void DFS(int vertex, bool[] visitArray,
            Action<int, bool[]> GetVertex = null)
        {
            DFSRec(vertex, visitArray, GetVertex);
        }

        private void DFSRec(int vertex, bool[] visitArray,
            Action<int, bool[]> VertexAndVisitArray = null)
        {
            visitArray[vertex] = true;

            VertexAndVisitArray?.Invoke(vertex, visitArray);

            var neighbors = GetNeighbors(vertex);

            foreach (var n in neighbors)
            {
                if (!visitArray[n])
                {
                    DFSRec(n, visitArray, VertexAndVisitArray);
                }
            }
        }

        //Breadth first search algorithm
        public void BFS(int startVertex, Func<int, int, bool> GetCurrent_Neighbor)
        {
            bool[] visitArray = new bool[m_VertexCount];

            QueueLL<int> q = new QueueLL<int>();

            q.Enqueue(startVertex);

            visitArray[startVertex] = true;
            
            bool stop = false;

            while (!q.IsEmpty() && !stop)
            {
                int currVertex = q.Dequeue();

                var neighbors = GetNeighbors(currVertex);

                foreach (var n in neighbors)
                {
                    if (!visitArray[n])
                    {
                        q.Enqueue(n);

                        visitArray[n] = true;

                        if (!(bool)GetCurrent_Neighbor?.Invoke(currVertex, n))
                        {
                            stop = true;

                            break;
                        }                        
                    }
                }
            }                           
        }
        
        public override string ToString()
        {
            string str = String.Empty;

            for (int i = 0; i < m_VertexCount; i++)
            {
                for (int j = 0; j < m_VertexCount; j++)
                {
                    str += $"{m_AdjacencyMatrix[i, j],4}";
                }

                str += "\n";
            }

            return str;
        }
        
        public void Clear()
        {
            for (int i = 0; i < m_VertexCount; i++)
            {
                for (int j = 0; j < m_VertexCount; j++)
                {
                    m_AdjacencyMatrix[i, j] = m_NoEdgeValue;
                }
            }            
        }

        public IEnumerable<int> GetVerteces()
        {
            long count = m_AdjacencyMatrix.GetLongLength(0);

            int[] verteces = new int[count];
            
            for (int i = 0; i < count; i++)
            {
                verteces[i] = i;
            }

            return verteces;
        }

        public bool[] InitializeVisitDS()
        {
            return new bool[m_VertexCount];
        }

        #endregion
    }
}