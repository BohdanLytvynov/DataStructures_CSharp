using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.Graphs.MultiDimGrid
{
    public class MultiDim_Grid<TGridItem> : IGrid<TGridItem, int>
    {
        #region Fields

        dynamic m_grid;
        
        int m_rank;

        List<int> m_Bounds;

        Func<dynamic, List<int>, TGridItem> m_GetElementMethod;

        Action<dynamic, List<int>, TGridItem> m_SetElementMethod;
        
        #endregion

        #region Properties       
        public int Rank { get => m_rank; }
        public List<int> Bounds { get => m_Bounds; }
        dynamic IGrid<TGridItem, int>.GridMatrix { get => m_grid;}
                
        #endregion

        #region Ctor
        public MultiDim_Grid(dynamic Matrix, Func<dynamic, List<int>, TGridItem> GetElementMethod,
            Action<dynamic, List<int>, TGridItem> SetElementMethod)
        {            
            m_SetElementMethod = SetElementMethod;

            m_GetElementMethod = GetElementMethod;

            m_grid = Matrix;

            m_rank = m_grid.Rank;

            m_Bounds = new List<int>();

            for (int i = 0; i < m_rank; i++)
            {
                m_Bounds.Add((int)m_grid.GetLongLength(i));
            }                                               
        }
        #endregion

        #region Methods
        public TGridItem GetItem(List<int> point)
        {
            return m_GetElementMethod?.Invoke(m_grid, point);
        }

        public void SetItem(TGridItem item, List<int> point)
        {
            m_SetElementMethod?.Invoke(m_grid, point, item);
        }
        
        #endregion

    }
}
