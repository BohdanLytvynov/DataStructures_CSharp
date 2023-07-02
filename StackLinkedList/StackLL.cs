using SingleLinkeed_List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackLinkedList
{
    public class StackLL<T>
    {
        private Node<T> m_head;

        public virtual Node<T> Head { get=> m_head; protected set => m_head = value; }

        public StackLL()
        {

        }

        public virtual void Push(T data)// O(1)
        {
            Node<T> newNode = new Node<T>(data);

            newNode.Next = m_head;

            m_head = newNode;
        }

        public virtual Node<T> Pop()
        {
            Node<T> r = null;

            if (m_head == null)
            {
                return null;
            }
            else
            {
                r = m_head.Clone();

                m_head = m_head.Next;
            }

            return r;

        }
    }
}
