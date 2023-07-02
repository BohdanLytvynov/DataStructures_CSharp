using DoubledLinkedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularDoubledLinkedList
{
    public class Circular_DoubledLinkedList<T> : DoubledLinkedList<T>
    {               
        #region Ctor
        public Circular_DoubledLinkedList()
        {

        }
        #endregion

        #region Methods

        public override void Clear() => base.Clear();

        public override IEnumerable<T> GetDataForward()
        {
            var l = new List<T>();
            
            if (Head == null)
            {
                return l;
            }

            var temp = Head;

            do
            {
                l.Add(temp.Data);

                temp = temp.Next;

            } while (temp != Head);

            return l;
        }

        public override IEnumerable<T> GetDataBackward()
        {
            var l = new List<T>();

            if (Tail == null)
            {
                return l;
            }

            var last = Tail;

            do
            {
                l.Add(last.Data);

                last = last.Prev;
            } while (last != Tail);

            return l;
        }

        public override void AddFirst(T data)
        {
            var newNode = new DNode<T>(data);

            if (Head == null) //List is empty
            {
                newNode.Next = newNode;

                newNode.Prev = newNode;

                Head = newNode;

                Tail = newNode;
            }
            else //Linked list already has nodes. Add at the begining
            {
                newNode.Next = Head;

                Head.Prev = newNode;
                                
                Head = newNode;

                Head.Prev = Tail;

                Tail.Next = Head;
            }
        }

        public override void AddLast(T data)
        {
            var newNode = new DNode<T>(data);

            if (Head == null) //List is empty
            {
                newNode.Next = newNode;

                newNode.Prev = newNode;

                Head = newNode;

                Tail = newNode;
            }
            else
            {
                Tail.Next = newNode;

                newNode.Prev = Tail;

                Tail = newNode;

                Head.Prev = Tail;

                Tail.Next = Head;
            }
        }

        public override DNode<T> Search(T key)
        {
            if (Head == null || Tail == null)
            {
                return null;
            }

            if (Head.Data.Equals(key))
            {
                return Head;
            }

            if (Tail.Data.Equals(key))
            {
                return Tail;
            }

            //Need iteration

            var temp = Head.Next;

            do
            {
                if (temp.Data.Equals(key))
                {
                    return temp;
                }

                temp = temp.Next;

            } while (temp != Tail);

            return null;
        }

        public override bool Remove(T key)
        {            
            if (Head == null)
            {
                return false;
            }

            if (Head.Data.Equals(key) && Head.Next == Head && Head.Prev == Head)
            {
                Clear();

                return true;
            }

            if (Head.Data.Equals(key))
            {
                Tail.Next = Head.Next;

                Head.Next.Prev = Tail;

                Head = Head.Next;

                return true;
            }

            if (Tail.Data.Equals(key))
            {
                Tail.Prev.Next = Head;

                Head.Prev = Tail.Prev;

                Tail = Tail.Prev;

                return true;
            }

            var temp = Head.Next;

            do
            {
                if (temp.Data.Equals(key))
                {
                    temp.Prev.Next = temp.Next;

                    temp.Next.Prev = temp.Prev;

                    return true;
                }

                temp = temp.Next;
            } while (temp != Tail);
                        
            return false;
        }

        #endregion
    }
}
