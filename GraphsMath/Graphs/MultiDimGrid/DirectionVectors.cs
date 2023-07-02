using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.Graphs.MultiDimGrid
{   
    public struct DirectionVectors<TCoordsType>
    {
        #region FIelds

        List<List<TCoordsType>> m_Dir_Vectors;

        int m_Dim_Count;

        #endregion

        #region Propeties

        public List<List<TCoordsType>> Dir_Vectors { get => m_Dir_Vectors; }

        public int DimensionsCount { get=> m_Dim_Count; }

        #endregion

        #region Ctor
        public DirectionVectors(List<List<TCoordsType>> Dir_Vectors)
        {
            m_Dir_Vectors = Dir_Vectors;

            m_Dim_Count = Dir_Vectors.Count;
        }
        #endregion

        #region Methods

        public List<TCoordsType> this [int dimension]
        { 
            get => m_Dir_Vectors[dimension];
        }

       

        #endregion
    }
}
