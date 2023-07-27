using GraphsMath.Graphs.Graph_Components;
using GraphsMath.Graphs.Graph_Components.Interfaces;

namespace GraphsMath.Graphs.Interfaces
{
       
    public interface IGraph<TVertexType, TVertexKey, TWeight> 
        where TVertexKey :IEquatable<TVertexKey>, IComparable<TVertexKey>
        
    {
        #region Indexer

        TVertexType this[TVertexKey key] { get; }

        #endregion

        #region Properties

        public int VertexCount { get; }

        public TWeight NoEdgeValue { get; set; }

        #endregion

        #region Methods

        #region Prototype Pattern

        IGraph<TVertexType, TVertexKey, TWeight> Clone();

        #endregion

        void Clear();

        #region Vertrex Operations
        void AddVertex(params TVertexType[] vd);

        void RemoveVertex(params TVertexKey[] vd);

        void EditVertex(TVertexKey vd, TVertexType newVd);

        IEnumerable<TVertexType> GetNeighbors(TVertexKey Vertex);

        IEnumerable<TVertexType> GetVerteces();
        #endregion

        #region Edges Operations

        IEnumerable<IEdge<TVertexKey, TWeight>> GetAllGraphEdges();

        void AddEdge(TVertexKey start, TVertexKey end, TWeight weight);

        void AddBiDirectionalEdge(TVertexKey start, TVertexKey end, TWeight forward,
            TWeight backward);
        

        void RemoveEdge(TVertexKey start, TVertexKey end);

        void EditEdge(TVertexKey start, TVertexKey end, TWeight weight = default);
       
        IEnumerable<IEdge<TVertexKey, TWeight>> GetEdges(TVertexKey v);
        #endregion
        
        TWeight GetWeight(TVertexKey start, TVertexKey end);
               
        Dictionary<TVertexKey, bool> InitializeVisitDS();
        
        /// <summary>
        /// Depth First Search algorithm Vertex - start vertex. 
        /// GetVertex - Delegate that gets current vertex in a graph
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="GetVertexVisitDictionary"></param>
        void DFS(TVertexKey vertex, Dictionary<TVertexKey, bool> visitArray,
            Func<TVertexType, Dictionary<TVertexKey, bool>, bool> GetVertexVisitDictionary = null,
            Func<TVertexType, bool> RecursiveCallBack = null);

        /// <summary>
        /// Breadth First Search algorithm. startVertex - StartVertex.
        /// GetCurrebt_Neighbor - Func delegate that gets current, Vertex its neighbor,
        /// return value is Boolean. It controlls the iteration of the Graph. 
        /// If we return false it will stop iteration.
        /// </summary>
        /// <param name="startVertex"></param>
        /// <param name="GetCurrent_Neighbor"></param>
        void BFS(TVertexKey startVertex, Func<TVertexType, TVertexType, bool> GetCurrent_Neighbor);

        IEnumerable<TVertexType> TopSort();
        
        TVertexKey GetVertexKeyFromVertex(TVertexType vertex);

        #endregion

    }

    public interface IFlowGraph<TVertexKey, TFlowValue>
    {
        #region Properties

        public int VertexCount { get; }

        public SortedDictionary<TVertexKey, List<IFlowEdge<TVertexKey, TFlowValue>>> Graph { get; }

        #endregion

        #region Methods

        IEnumerable<TVertexKey> GetAllVerteces();

        IEnumerable<IFlowEdge<TVertexKey, TFlowValue>> GetAdjEdges(TVertexKey vertex);

        Dictionary<TVertexKey, int> CreateVisitDS(
            int visitToken);

        #endregion
    }
}