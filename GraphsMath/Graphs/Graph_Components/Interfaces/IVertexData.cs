using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.Graphs.Graph_Components.Interfaces
{
    public interface IVertexData<TVertexKey, TWeight>
    {
        #region Properties
        public TVertexKey VertexKey { get; }

        public TWeight Weight { get; set; }

        public SortedDictionary<string, dynamic> AddData { get; set; }
        #endregion

        #region Methods

        void SetNewData(IVertexData<TVertexKey, TWeight> other);

        void SetWeight(TWeight w);

        #endregion


    }
}
