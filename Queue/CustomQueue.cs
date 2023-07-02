using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    public class CustomQueue<T>
    {
        private readonly int m_size;

        public int Size { get => m_size; }

        T[] array;

        int m_front;// Index of the begining

        int m_end; // Index of the end

        public CustomQueue(int size)
        {
            m_size = size;

            array = new T[m_size];

            m_front = 0;

            m_end = 0;
        }

        public virtual void Enqueue(T data)
        {
            if (m_end == m_size)
            {
                //Queue is full
                return;
            }

            array[m_end] = data;

            m_end++;
        }

        public virtual T Dequeue()
        {
            if (m_end == m_front)
            {
                //Queue is empty
            }

            var r = array[m_front];

            m_front++;

            return r;
        }

        public virtual void Restore()
        {
            m_end = 0;

            m_front = 0;
        }
    }
}
