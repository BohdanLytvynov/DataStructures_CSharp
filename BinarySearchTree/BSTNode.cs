using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class BSTNode<T> 
        where T : IComparable<T>
    {
        #region Fields

        BSTNode<T> m_left;

        BSTNode<T> m_right;

        private T m_data;

        #endregion

        #region Properties

        public T Data { get => m_data; set => m_data = value; }

        public BSTNode<T> Left { get => m_left; set => m_left = value; }

        public BSTNode<T> Right { get => m_right; set => m_right = value; }

        #endregion

        #region Ctor
        public BSTNode(T data)
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
