using SingleLinkeed_List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueLinkedList
{
    public class QueueLL<T>
    {
        Node<T> m_front;

        Node<T> m_end;

        public QueueLL()
        {

        }

        public virtual void Enqueue(T data)
        {
            Node<T> newNode = new Node<T>(data);

            if (m_end == null && m_front == null)
            {
                m_front = m_end = newNode;
            }
            else
            {
                m_end.Next = newNode;

                m_end = newNode;
            }
        }

        public bool IsEmpty()
        {
            return m_front == null;
        }

        public virtual T Dequeue()
        {
            T r;

            if (m_front == null)
            {                
                return default;
            }
            else
            {
                r = m_front.Data;

                m_front = m_front.Next;

                if (m_front == null)
                {
                    m_end = null;
                }
            }

            return r;
        }       
    }
}
