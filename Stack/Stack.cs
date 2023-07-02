using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    public class Stack<T>
    {
        #region Fields
        private readonly int m_size;

        private int m_index;

        private T[] m_array;
        #endregion

        #region Properties
        public int Size { get=> m_size; }
        #endregion

        #region Ctor
        public Stack(int size)
        {
            m_size = size;

            m_index = -1;

            m_array = new T[m_size];
        }
        #endregion

        #region Methods

        public void Push(T value)
        {
            if (m_index == m_size - 1)//Case when stack is full
            {
                return;
            }
            else
            {
                ++m_index;

                m_array[m_index] = value;
            }
        }

        public bool Pop(ref T temp)
        {            
            if (m_index == -1)
            {
                //Stack is empty;
                return false;
            }
            else
            {
                temp = m_array[m_index];

                m_array[m_index] = default;

                m_index--;   
                
                return true;
            }
        }

        #endregion



    }
}
