using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SingleLinkeed_List
{
    public class Node<T>
    {
        #region Fields

        private T m_data;

        private Node<T> m_next;

        #endregion

        #region Properties

        public Node<T> Next { get=> m_next; set => m_next = value; }

        public T Data { get=> m_data; set=> m_data = value; }

        #endregion

        #region Ctor

        public Node(T data)
        {
            m_data = data;

            m_next = null;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return m_data.ToString();
        }

        public virtual Node<T> Clone()
        {
            return new Node<T>(this.m_data);
        }

        #endregion
    }
}
