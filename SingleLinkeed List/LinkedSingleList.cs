using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SingleLinkeed_List
{
    public class LinkedSingleList<T>
    {
        #region Fields

        private Node<T> m_head;

        private int m_count;

        #endregion

        #region Properties

        public int Count { get=> m_count; }

        public virtual Node<T> Head 
        {
            get { return m_head; }

            protected set => m_head = value;
        }

        #endregion

        #region Ctor
        public LinkedSingleList()
        {
            
        }
        #endregion

        #region Methods

        public virtual IEnumerable<T> GetData()
        { 
            var r = new List<T>();

            Node<T> temp = m_head;
            
            while (temp != null)
            {
                r.Add(temp.Data);

                temp = temp.Next;
            }

            return r;
        }

        public virtual void AddFirst(T data)// O(1)
        {
            Node<T> newNode = new Node<T>(data);

            newNode.Next = m_head;

            m_head = newNode;

            m_count++;
        }


        public virtual void AddLast(T data)
        { 
            Node<T> newNode = new Node<T> (data);

            if (m_head == null)
            {
                m_head = newNode;
            }
            else
            {
                Node<T> last = m_head;

                while (last.Next != null)
                {
                    last = last.Next;
                }

                last.Next = newNode;
            }

            m_count++;
        }

        public virtual Node<T> Search(T data)
        {
            var temp = Head;

            while (temp != null)
            {
                if (temp.Data.Equals(data))
                {
                    return temp;
                }
                else
                {
                    temp = temp.Next;
                }
            }

            return null;
        }

        public virtual void Delete(T data)
        {
            if (m_head == null) //Empty list
            {
                return;
            }

            if (m_head.Data.Equals(data))
            {
                m_head = m_head.Next;
            }
            else
            {
                Node<T> temp = m_head;

                while (temp.Next != null)
                {
                    if (temp.Next.Data.Equals(data))
                    {
                        temp.Next = temp.Next.Next;
                        break;
                    }
                    else
                    {
                        temp = temp.Next;
                    }
                }
            }

            m_count--;
        }

        public virtual void Clear()
        {
            m_head = null;

            m_count = 0;
        }
        
        #endregion
    }
}
