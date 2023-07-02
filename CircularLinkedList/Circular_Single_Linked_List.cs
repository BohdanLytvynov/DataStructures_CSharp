using SingleLinkeed_List;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularLinkedList
{
    public class Circular_Single_Linked_List<T>: LinkedSingleList<T>
    {
        #region Fields

        #endregion

        #region Properties
        
        #endregion

        #region Ctor
        public Circular_Single_Linked_List()
        {

        }
        #endregion

        #region Mehods

        public override void Clear()
        {
            base.Clear();
        }

        public override IEnumerable<T> GetData()
        {
            var l = new List<T>();

            if (Head == null)
            {
                return l;
            }
            else
            {
                var temp = Head;

                do
                {
                    l.Add(temp.Data);

                    temp = temp.Next; 
                } while (temp != Head);
            }

            return l;

        }

        public override void AddFirst(T data)
        {
            Node<T> newNode = new Node<T>(data);

            if (Head == null) //CSLL is empty
            {
                newNode.Next = newNode;

                Head = newNode;
            }
            else //CSLL already contains some elements
            {
                var last = Head;

                while (last.Next != Head)
                {
                    last = last.Next;
                }

                newNode.Next = Head;

                last.Next = newNode;

                Head = newNode; 
            }

        }

        public override void AddLast(T data)
        {
            var newNode = new Node<T>(data);

            if (Head == null)
            {
                newNode.Next = newNode;

                Head = newNode;
            }
            else
            {
                var last = Head;

                while (last.Next != Head)
                {
                    last = last.Next;
                }

                last.Next = newNode;

                newNode.Next = Head;
            }
        }

        public override Node<T> Search(T data)
        {
            var temp = Head;

            do
            {
                if (temp.Data.Equals(data))
                {
                    return temp;
                }

                temp = temp.Next;
            } while (temp.Next != Head);

            return null;
        }

        public override void Delete(T data)
        {
            if (Head == null)// CSLL is empty
            {
                return;
            }

            if (Head.Data.Equals(data) && Head.Next == Head)// We need to delete first element
                //and CSLL contains only 1 element
            {
                Head = null;
            }
            else if (Head.Data.Equals(data))//CSLL contains more then one element. And we need to 
                //delete it
            {
                //We need to find last element

                var last = Head;

                while (last.Next != Head)
                {
                    last = last.Next;
                }

                last.Next = Head.Next;

                Head = Head.Next;
            }
            else //Element for deletion is not in a begining
            {
                var temp = Head;

                while (temp.Next != Head)
                {
                    if (temp.Next.Data.Equals(data))
                    {
                        temp.Next = temp.Next.Next;

                        break;
                    }
                    else temp = temp.Next;
                }
            }


        }

        #endregion
    }
}
