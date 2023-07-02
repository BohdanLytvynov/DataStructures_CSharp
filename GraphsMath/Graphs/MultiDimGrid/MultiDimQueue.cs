using QueueLinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsMath.Graphs.MultiDimGrid
{
    public class MultiDimQueue<TCoordsType>
    {
        #region Fields
        QueueLL<TCoordsType>[] m_queues;

        int m_dim;
        #endregion

        #region Properties
        public QueueLL<TCoordsType> [] Queues { get => m_queues; }

        public int Dimensions { get=> m_dim; }
        #endregion

        #region Ctor
        public MultiDimQueue(int dimenCount)
        {
            m_dim = dimenCount;

            m_queues = new QueueLL<TCoordsType>[dimenCount];

            for (int i = 0; i < m_dim; i++)
            {
                m_queues[i] = new QueueLL<TCoordsType>();
            }
        }
        #endregion

        #region Methods

        public bool IsEmpty()
        {            
            var empty = new bool[m_dim];

            for (int i = 0; i < m_dim; i++)
            {
                empty[i] = m_queues[i].IsEmpty();
            }

            for (int i = 0; i < m_dim; i++)
            {
                if (empty[i] == false)
                {
                    return false;
                }
            }

            return true;
        }

        public void Enqueue(List<TCoordsType> Vector)
        {
            int count = Vector.Count;

            if (count != m_queues.Length)
            {
                throw new IncorrectAmountOfDimensionsException("Incorrect ammount of dimensions in input vector and queues array");
            }

            for (int i = 0; i < count; i++)
            {
                m_queues[i].Enqueue(Vector[i]);
            }
        }

        public List<TCoordsType> Dequeue()
        {
            List<TCoordsType> Vector = new List<TCoordsType>();

            for (int i = 0; i < m_dim; i++)
            {
                Vector.Add(m_queues[i].Dequeue());
            }

            return Vector;
        }

        #endregion
    }
}
