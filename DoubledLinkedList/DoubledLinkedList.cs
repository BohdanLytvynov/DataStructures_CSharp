using SingleLinkeed_List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubledLinkedList
{
    public class DoubledLinkedList<T> where T : IComparable<T>
    {
        #region Fields

        DNode<T> m_head;

        DNode<T> m_tail;

        #endregion

        #region Properties

        public DNode<T> Head { get => m_head; protected set => m_head = value; }

        public DNode<T> Tail { get => m_tail; protected set => m_tail = value; }

        #endregion

        #region ctor

        public DoubledLinkedList()
        {

        }

        #endregion

        #region Methods

        public virtual void Clear()
        {
            m_head = null;

            m_tail = null;
        }

        public virtual void AddFirst(T data)
        {
            DNode<T> newNode = new DNode<T>(data);

            if (m_head == null)
            {
                m_head = newNode;

                m_tail = newNode;
            }
            else
            {
                newNode.Next = m_head;

                m_head.Prev = newNode;

                m_head = newNode;
            }
        }

        public virtual void AddLast(T data)
        {
            DNode<T> newNode = new DNode<T>(data);

            if (m_head == null)
            {
                m_head = newNode;

                m_tail = newNode;
            }
            else
            {
                //var last = m_head;

                //while (last.Next != null)
                //{
                //    last = last.Next;
                //}

                m_tail.Next = newNode;

                newNode.Prev = m_tail;

                m_tail = newNode;
            }
        }

        public virtual IEnumerable<T> GetDataForward()
        {
            List<T> l = new List<T>();

            var temp = m_head;

            while (temp != null)
            {
                l.Add(temp.Data);

                temp = temp.Next;
            }

            return l;
        }

        public virtual IEnumerable<T> GetDataBackward()
        {
            List<T> l = new List<T>();

            var temp = m_tail;

            while (temp != null)
            {
                l.Add(temp.Data);

                temp = temp.Prev;
            }

            return l;
        }

        public virtual DNode<T> Search(T key)
        {
            if (m_tail.Data.CompareTo(key) == 0)
            {
                return m_tail;
            }

            if (m_head.Data.CompareTo(key) == 0)
            {
                return m_head;
            }

            var temp = m_head.Next;

            while (temp != null)
            {
                if (temp.Data.CompareTo(key) == 0)
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

        public virtual bool Remove(T key)
        {
            bool r = false;

            if (m_head == null)
            {
                return r;
            }

            var temp = m_head;

            while (temp != null && !(temp.Data.CompareTo(key) == 0))
            {
                temp = temp.Next;
            }

            if (temp == null)
            {
                return r;
            }
            else if (m_head == temp)
            {
                m_head = m_head.Next;

                m_head.Prev = null;

                r = true;
            }
            else if (temp.Next == null)
            {
                m_tail = temp.Prev;

                m_tail.Next = null;

                r = true;
            }
            else
            {
                temp.Prev.Next = temp.Next;

                temp.Next.Prev = temp.Prev;

                r = true;
            }

            return r;

        }

        public DoubledLinkedList<T> ForwardCopy()
        {
            var n = new DoubledLinkedList<T>();

            var l = GetDataForward();

            foreach (var item in l)
            {
                n.AddLast(item);
            }

            return n;
        }

        #endregion

        #region To String
       
        public override string ToString()
        {
            var str = String.Empty;

            foreach (var item in GetDataForward())
            {
                str += $"{item}";
            }

            return str;
        }

        #endregion


    }
}
