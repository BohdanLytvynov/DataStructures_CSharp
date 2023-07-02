using GraphsMath.Graphs.Graph_Components;
using GraphsMath.Graphs.Graph_Components.Interfaces;
using GraphsMath.Graphs.Interfaces;
using QueueLinkedList;
using System.Runtime.Serialization.Formatters;

namespace GraphsMath.Graphs.AMGraphs
{
    public class AdjacentMatrixGraph<TWeight> : IGraph<int, int, TWeight>
    {
        #region Fields

        int m_VertexCount;

        TWeight[,] m_AdjacencyMatrix;
        
        TWeight m_NoEdgeValue;

        #endregion

        #region Properties

        public TWeight[,] AdjMatrix { get=>m_AdjacencyMatrix; }

        public int VertexCount { get=> m_VertexCount; }

        public TWeight[,] AdjacencyMatrix { get => m_AdjacencyMatrix; }

        public TWeight NoEdgeValue { get=> m_NoEdgeValue; set => m_NoEdgeValue = value; }        

        #endregion

        #region Ctor
        public AdjacentMatrixGraph(int vertexCount, TWeight NoEdgeValue = default)
        {
            m_VertexCount = vertexCount;

            m_AdjacencyMatrix = new TWeight[m_VertexCount, m_VertexCount];
            
            m_NoEdgeValue = NoEdgeValue;

            if (!NoEdgeValue.Equals(default))
            {
                for (int i = 0; i < m_VertexCount; i++)
                {
                    for (int j = 0; j < m_VertexCount; j++)
                    {
                        m_AdjacencyMatrix[i, j] = m_NoEdgeValue;
                    }
                }
            }
        }

        public AdjacentMatrixGraph(AdjacentMatrixGraph<TWeight> donor)
        {
            m_VertexCount = donor.VertexCount;

            m_AdjacencyMatrix = new TWeight[m_VertexCount, m_VertexCount];

            m_NoEdgeValue = donor.NoEdgeValue;

            for (int i = 0; i < m_VertexCount; i++)
            {
                for (int j = 0; j < m_VertexCount; j++)
                {
                    m_AdjacencyMatrix[i, j] = donor.AdjacencyMatrix[i, j];
                }
            }
        }

        #endregion

        #region Methods
        
        public void AddEdge(int start, int end, TWeight weight)
        {
            m_AdjacencyMatrix[start, end] = weight;            
        }

        public void RemoveEdge(int start, int end)
        {
            m_AdjacencyMatrix[start, end] = m_NoEdgeValue;            
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
        //This algorithm iterates graph verteces

        public void DFS(int vertex, Dictionary<int, bool> visitArray,
            Func<int, Dictionary<int, bool>, bool> GetVertex = null, 
            Func<int, bool> RecursiveCallBack = null)
        {
            DFSRec(vertex, visitArray, GetVertex, RecursiveCallBack);
        }

        private void DFSRec(int vertex, Dictionary<int, bool> visitArray,
            Func<int, Dictionary<int, bool>, bool> VertexAndVisitArray = null,
            Func<int, bool> RecursiveCallBack = null)
        {
            visitArray[vertex] = true;

            if (!(VertexAndVisitArray?.Invoke(vertex, visitArray) ?? true))
            {
                return;
            }
            
            var neighbors = GetNeighbors(vertex);

            foreach (var n in neighbors)
            {
                if (!visitArray[n])
                {
                    DFSRec(n, visitArray, VertexAndVisitArray, RecursiveCallBack);
                }
            }

            if (!(RecursiveCallBack?.Invoke(vertex) ?? true))
            {
                return;
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

        public Dictionary<int ,bool> InitializeVisitDS()
        {
            var d = new Dictionary<int ,bool>();

            for (int i = 0; i < m_VertexCount; i++)
            {
                d.Add(i, false);
            }

            return d;
        }

        public IEnumerable<int> TopSort()
        {            
            var visitArray = InitializeVisitDS();
                        
            List<int> visitNodes = new List<int>();

            for (int j = 0; j < m_VertexCount; j++)
            {
                if (!visitArray[j])
                {                    
                    DFS(j, visitArray, 
                        null,
                        (v) => {
                        
                            visitNodes.Add(v);
                           
                            return true;
                        });                   
                }
            }

            visitNodes.Reverse();

            return visitNodes;
        }

        public IEnumerable<int> TopSort(int start, int end)
        {
            if (start > end)
            {
                return new int[] { };
            }

            int count = end - start;

            var visitArray = InitializeVisitDS();
           
            int i = count - 1;

            List<int> visitNodes = new List<int>();

            bool write = true;

            for (int j = start; j < count; j++)
            {
                if (!visitArray[j])
                {
                    DFS(j, visitArray, (v, vA) =>
                    {
                        if (v == end)
                        {
                            write = false;

                            return write;
                        }

                        return true;
                    }, (v) => 
                    {
                        if (write)
                        {
                            visitNodes.Add(v);                                          
                        }
                        else
                        {
                            return false;
                        }

                        return true;
                    });
                }
            }

            visitNodes.Reverse();

            return visitNodes;
        }

        public IEnumerable<int> VertexKeyTopSort()
        {
            return TopSort();
        }

        public IEnumerable<IEdge<int, TWeight>> GetEdges(int v)
        {
            List<IEdge<int, TWeight>> edges = new List<IEdge<int, TWeight>>();
            
            for (int i = 0; i < m_VertexCount; i++)
            {
                if (!m_AdjacencyMatrix[v, i].Equals(NoEdgeValue))
                {
                    edges.Add(new Edge<int, TWeight>(v, i, GetWeight(v, i)));
                }
            }

            return edges;
        }

        public IGraph<int, int, TWeight> Clone()
        {
            return new AdjacentMatrixGraph<TWeight>(this);
        }

        #region Explicitly Implemented Methods

        int IGraph<int, int, TWeight>.this[int key] => 
            throw new NotImplementedException("Use for AdjacentList Graphs only!");

        void IGraph<int, int, TWeight>.AddVertex(params int[] vd)
        {
            throw new NotImplementedException("Use for AdjacentList Graphs only!");
        }

        void IGraph<int, int, TWeight>.RemoveVertex(params int[] vd)
        {
            throw new NotImplementedException("Use for AdjacentList Graphs only!");
        }

        void IGraph<int, int, TWeight>.EditVertex(int vd, int newVd)
        {
            throw new NotImplementedException("Use for AdjacentList Graphs only!");
        }

        public int GetVertexKeyFromVertex(int vertex)
        {
            return vertex;
        }

        public IEnumerable<IEdge<int, TWeight>> GetAllGraphEdges()
        {       
            List<IEdge<int, TWeight>> edges = new List<IEdge<int, TWeight>>();

            for (int i = 0; i < m_VertexCount; i++)
            {
                var VertEdges = GetEdges(i);

                foreach (var edge in VertEdges)
                {
                    edges.Add(edge);
                }
            }

            return edges;
        }
       
        #endregion



        #endregion
    }
}