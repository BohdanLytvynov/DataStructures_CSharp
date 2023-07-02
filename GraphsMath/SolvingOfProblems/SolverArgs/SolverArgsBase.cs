namespace GraphsMath.SolvingOfProblems.SolverArgs
{
    public class SolverArgsBase
    {
        #region Fields

        List<object> m_args;

        #endregion

        #region Properties

        public List<object> Args { get=> m_args; }

        #endregion

        #region Ctor

        public SolverArgsBase()
        {
            m_args = new List<object>();
        }

        #endregion

        #region Indexer

        public object this[int index]
        { 
            get => m_args[index];

            set => m_args[index] = value;
        }

        #endregion
    }
}