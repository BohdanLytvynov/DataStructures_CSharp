using GraphsMath.Graphs.Interfaces;
using GraphsMath.SolvingOfProblems.SolverArgs;

using System.Reflection;

namespace GraphsMath.SolvingOfProblems
{
    public class SolverResult
    {
        #region Properties

        public string ProblemName { get; }

        public List<object> Result { get; }

        public bool HasError { get; }

        public Exception Exception { get; }

        #endregion

        #region Ctor

        public SolverResult(string problemName, List<object> result, bool hasError = false,
            Exception exception = null)
        {
            ProblemName = problemName;

            Result = result;

            HasError = hasError;

            Exception = exception;
        }

        #endregion
    }

    public abstract class Problem_Solver<TVertexType, TVertexKey, TWeight>
        where TVertexKey : IEquatable<TVertexKey>, IComparable<TVertexKey>
    {
        #region Fields

        private IGraph<TVertexType, TVertexKey, TWeight> m_graph;

        public IGraph<TVertexType, TVertexKey, TWeight> Graph { get => m_graph; }

        #endregion
        
        #region Ctor
        public Problem_Solver(IGraph<TVertexType, TVertexKey, TWeight> graph)
        {                        
            m_graph = graph ?? throw new ArgumentNullException(nameof(graph));
        }
        #endregion

        #region Methods
        public abstract SolverResult Solve(SolverArgsBase args = null);
       
        #endregion
    }

    public abstract class NetworkFlowProblemSolver<TVertexKey, TFlowValue>
    {
        #region Fields

        IFlowGraph<TVertexKey, TFlowValue> m_graph;

        #endregion

        #region Properties

        public IFlowGraph<TVertexKey, TFlowValue> FlowGraph { get=> m_graph; }

        #endregion

        #region Ctor
        public NetworkFlowProblemSolver(IFlowGraph<TVertexKey, TFlowValue> flowGraph)
        {
            m_graph = flowGraph ?? throw new ArgumentNullException(nameof(flowGraph));
        }
        #endregion

        #region Methods
        public abstract SolverResult Solve(SolverArgsBase args = null);

        
        #endregion
    }
}