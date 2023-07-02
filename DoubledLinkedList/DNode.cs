using SingleLinkeed_List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubledLinkedList
{
    public class DNode<T> 
    {
        #region Fields

        DNode<T> m_prev;

        DNode<T> m_next;

        private T m_data;

        #endregion

        #region Properties

        public T Data { get=> m_data; }

        public DNode<T> Prev { get=> m_prev ; set => m_prev = value; }

        public DNode<T> Next { get=> m_next; set=> m_next = value; }

        #endregion

        #region Ctor
        public DNode(T data)
        {
            m_data = data;
        }
        #endregion

        public override string ToString()
        {
            return m_data.ToString();
        }
    }
}
