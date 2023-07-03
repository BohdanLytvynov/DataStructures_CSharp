
using DoubledLinkedList;
using GraphsMath.Graphs.Graph_Components;
using GraphsMath.Graphs.Graph_Components.Interfaces;
using GraphsMath.Graphs.Interfaces;
using QueueLinkedList;

namespace GraphsMath.Graphs.ALGraphs
{
    public class AdjacentListGraph<TVertexKey, TWeight>
        :  IGraph<AdjListVertex<TVertexKey, TWeight>, TVertexKey, TWeight>
        where TVertexKey : IComparable<TVertexKey>,IEquatable<TVertexKey>
    {
        #region Fields

        private int m_VertexCount;

        private SortedDictionary<AdjListVertex<TVertexKey, TWeight>,
            DoubledLinkedList<AdjListVertex<TVertexKey, TWeight>>> m_graph;

        #endregion

        #region Properties
        
        public int VertexCount { get => m_VertexCount; }
        TWeight IGraph<AdjListVertex<TVertexKey, TWeight>, TVertexKey, TWeight>.NoEdgeValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #endregion

        #region Indexer

        public AdjListVertex<TVertexKey, TWeight> this[TVertexKey v]
        {
            get
            {
                var r = from k in m_graph.Keys
                        where k.CompareTo(v) == 0
                        select k;

                return r.First();
            }
        }

        #endregion

        #region Ctor

        public AdjacentListGraph(List<AdjListVertex<TVertexKey, TWeight>> verteces)
        {           
            m_graph = new SortedDictionary<AdjListVertex<TVertexKey, TWeight>, DoubledLinkedList<AdjListVertex<TVertexKey, TWeight>>>();
            
            AddVertex(verteces.ToArray());
        }

        public AdjacentListGraph(SortedDictionary<AdjListVertex<TVertexKey, TWeight>, DoubledLinkedList<AdjListVertex<TVertexKey, TWeight>>> graph)
        {            
            m_graph = graph;
            m_VertexCount = m_graph.Count;
        }

        public AdjacentListGraph()
        {
            m_graph = new SortedDictionary<AdjListVertex<TVertexKey, TWeight>, DoubledLinkedList<AdjListVertex<TVertexKey, TWeight>>>();
        }

        public AdjacentListGraph(AdjacentListGraph<TVertexKey, TWeight> donor)
        {
            m_VertexCount = donor.VertexCount;

            m_graph = new SortedDictionary<AdjListVertex<TVertexKey, TWeight>, DoubledLinkedList<AdjListVertex<TVertexKey, TWeight>>>();

            var verteces = donor.GetVerteces();

            foreach (var vertex in verteces)
            {
                m_graph.Add(vertex, donor.m_graph[vertex].ForwardCopy());
            }
        }

        #endregion

        #region Methods
       
        public void Clear()
        {
            if (m_graph.Count > 0)
            {
                m_graph.Clear();

                m_VertexCount = m_graph.Count;
            }
        }

        #region Vertex Operations
        
        public IEnumerable<AdjListVertex<TVertexKey, TWeight>> GetVerteces()
        {
            return m_graph.Keys.ToList();
        }

        public void AddVertex(params AdjListVertex<TVertexKey, TWeight>[] vd)
        {
            int count = vd.Length;

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (!m_graph.ContainsKey(vd[i]))
                    {
                        var dll = new DoubledLinkedList<AdjListVertex<TVertexKey, TWeight>>();

                        m_graph.Add(vd[i], dll);
                    }

                }
            }

            m_VertexCount = m_graph.Count();
        }

        public void RemoveVertex(params TVertexKey[] vd)
        {
            int count = vd.Length;

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (m_graph.ContainsKey(vd[i]))
                    {
                        m_graph.Remove(vd[i]);
                    }
                }

                var keys = m_graph.Keys;

                foreach (var item in keys)
                {
                    for (int i = 0; i < count; i++)
                    {
                        m_graph[item].Remove(vd[i]);
                    }
                }

                m_VertexCount = m_graph.Count;

            }
        }

        public void EditVertex(TVertexKey vd, AdjListVertex<TVertexKey, TWeight> newVd)
        {
            var temp = m_graph[vd].ForwardCopy();

            if (m_graph.ContainsKey(vd))
            {
                m_graph.Remove(vd);
            }

            if (!m_graph.ContainsKey(newVd.VertexKey))
            {
                m_graph.Add(newVd.VertexKey, temp);
            }

            var keys = m_graph.Keys.ToList();

            foreach (var item in keys)
            {
                m_graph[item].Search(vd)?.Data.SetNewData(newVd);
            }
        }

        #endregion

        #region Edges Operations

        public IEnumerable<IEdge<TVertexKey, TWeight>> GetEdges(TVertexKey v)
        {
            List<IEdge<TVertexKey, TWeight>> edges = new List<IEdge<TVertexKey, TWeight>>()
            {

            };
            var r = m_graph[v].GetDataBackward();

            foreach (var adjv in r)
            {
                edges.Add(new Edge<TVertexKey, TWeight>(v, adjv.VertexKey, adjv.Weight));
            }

            return edges;
        }

        public void AddEdge(TVertexKey start, TVertexKey end, TWeight weight)
        {
            if (m_graph.ContainsKey(start) && m_graph.ContainsKey(end))
            {
                var e = this[end];

                e.Weight = weight;

                if (m_graph.ContainsKey(start))
                {
                    m_graph[start].AddLast(e);
                }                                           
            }

        }

        public void RemoveEdge(TVertexKey start, TVertexKey end)
        {
            if (m_graph.ContainsKey(start))
            {
                m_graph[start].Remove(end);
            }
        }

        public void EditEdge(TVertexKey start, TVertexKey end, TWeight weight = default)
        {
            if (m_graph.ContainsKey(start))
                m_graph[start].Search(end).Data.SetWeight(weight);
        }

        public TWeight GetWeight(TVertexKey start, TVertexKey end)
        {
            return m_graph[start].Search(end).Data.Weight;
        }
        
        public IEnumerable<AdjListVertex<TVertexKey, TWeight>> GetNeighbors(TVertexKey v)
        {
            return m_graph[v].GetDataBackward();
        }

        #endregion

        #region Iterative Operations

        public void DFS(TVertexKey vertex,
            Dictionary<TVertexKey, bool> visitArray,
            Func<AdjListVertex<TVertexKey, TWeight>,
                Dictionary<TVertexKey, bool>, bool> GetVertex = null,
            Func<AdjListVertex<TVertexKey, TWeight>, bool> RecursiveCallback = null)
        {           
            DFSRec(vertex, visitArray, GetVertex, RecursiveCallback);
        }

        private void DFSRec(AdjListVertex<TVertexKey, TWeight> vertex,
            Dictionary<TVertexKey, bool> visitArray,
            Func<AdjListVertex<TVertexKey, TWeight>,
                Dictionary<TVertexKey, bool>, bool> GetVertex,
            Func<AdjListVertex<TVertexKey, TWeight>, bool> RecursiveCallback = null)
        {
            visitArray[vertex.VertexKey] = true;
            
            if (!(GetVertex?.Invoke(vertex, visitArray) ?? true))
            {
                return;
            }

            var neighbors = GetNeighbors(vertex.VertexKey);

            foreach (var item in neighbors)
            {
                if (!visitArray[item.VertexKey])
                {
                    DFSRec(item, visitArray, GetVertex, RecursiveCallback);
                }
            }

            if (!(RecursiveCallback?.Invoke(vertex) ?? true))
            {
                return;
            }            
        }

        public void BFS(TVertexKey startVertex,
            Func<AdjListVertex<TVertexKey, TWeight>, AdjListVertex<TVertexKey, TWeight>, bool> GetCurrent_Neighbor)
        {
            var visitArray =
                InitializeVisitDicionary(this);

            QueueLL<AdjListVertex<TVertexKey, TWeight>> q = new QueueLL<AdjListVertex<TVertexKey, TWeight>>();

            q.Enqueue(startVertex);

            visitArray[startVertex] = true;

            bool stop = false;

            while (!q.IsEmpty() && !stop)
            {
                AdjListVertex<TVertexKey, TWeight> currVertex = q.Dequeue();

                var neighbors = GetNeighbors(currVertex.VertexKey);

                foreach (var n in neighbors)
                {
                    if (!visitArray[n.VertexKey])
                    {
                        q.Enqueue(n);

                        visitArray[n.VertexKey] = true;

                        if (!(bool)GetCurrent_Neighbor?.Invoke(currVertex, n))
                        {
                            stop = true;

                            break;
                        }
                    }
                }
            }
        }

        public Dictionary<TVertexKey, bool> InitializeVisitDS()
        {
            return InitializeVisitDicionary(this);
        }

        public IEnumerable<AdjListVertex<TVertexKey, TWeight>> TopSort()
        {
            int count = m_VertexCount;

            var visitDic = this.InitializeVisitDS();

            var verteces = this.GetVerteces();

            List<AdjListVertex<TVertexKey, TWeight>> visitVertex = 
                new List<AdjListVertex<TVertexKey, TWeight>>();

            foreach (var item in verteces)
            {
                if (!visitDic[item.VertexKey])
                {
                    DFS(item.VertexKey, visitDic, null,
                        (v)=>
                        {
                            visitVertex.Add(v);

                            return true;
                        }
                        );
                }
            }

            visitVertex.Reverse();

            return visitVertex;
        }
        
        #endregion

        public override string ToString()
        {
            var str = String.Empty;

            foreach (var p in m_graph)
            {
                str += $"{p.Key.VertexKey} -> {p.Value}\n";
            }

            return str;
        }



        #region Static methods

        public static Dictionary<TVertexKey, bool> InitializeVisitDicionary(IGraph<AdjListVertex<TVertexKey, TWeight>, TVertexKey, TWeight> graph)
        {
            var d = new Dictionary<TVertexKey, bool>();

            var v = graph.GetVerteces();

            foreach (var item in v)
            {
                d.Add(item.VertexKey, false);
            }

            return d;
        }

        public TVertexKey GetVertexKeyFromVertex(AdjListVertex<TVertexKey, TWeight> vertex)
        {
            return vertex.VertexKey;    
        }

        public IEnumerable<IEdge<TVertexKey, TWeight>> GetAllGraphEdges()
        {
            List<IEdge<TVertexKey, TWeight>> edges = new List<IEdge<TVertexKey, TWeight>>();

            var keys = m_graph.Keys.ToList();

            foreach (var key in keys)
            { 
                var Vertexedges = GetEdges(key.VertexKey);

                foreach (var edge in Vertexedges)
                {
                    edges.Add(edge);
                }
            }

            return edges;
        }

        public IGraph<AdjListVertex<TVertexKey, TWeight>, TVertexKey, TWeight> Clone()
        {
            return new AdjacentListGraph<TVertexKey, TWeight>(this);
        }

        public void AddBiDirectionalEdge(TVertexKey start, TVertexKey end, TWeight forward, TWeight backward)
        {
            AddEdge(start, end, forward);

            AddEdge(end, start, backward);
        }

        #endregion

        #endregion

    }
   
}