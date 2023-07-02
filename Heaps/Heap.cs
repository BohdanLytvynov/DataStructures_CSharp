using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heaps
{
    public class Heap<TItem> 
        where TItem : IComparable<TItem>, IEquatable<TItem>
    {
        #region Fields

        int m_heapSize; //Current size of a heap

        readonly int m_Arity;

        List<TItem> m_heap;

        Dictionary<TItem, List<int>> m_HashTable;

        #endregion

        #region Prperties
        public int HeapSize { get=> m_heapSize; }
        #endregion

        #region Ctor
        public Heap(int arity)
        {
            m_Arity = arity;

            m_heap = new List<TItem>();

            m_HashTable = new Dictionary<TItem, List<int>>();
        }
        #endregion

        #region Methods

        public void Remove(TItem item)
        {
            //Get item index

            var index = m_HashTable[item].First();

            Swap(index, m_heapSize - 1);

            m_heap.RemoveAt(m_heapSize - 1);

            m_heapSize = m_heap.Count;

            RemoveIndexFromHashTable(item, m_heapSize);

            if (m_heapSize > 0)
            {
                Sink(index);
            }
        }

        public TItem Poll()
        {
            TItem item = default;

            if (m_heapSize > 0)
            {
                item = m_heap[0];
                 
                Swap(0, m_heapSize - 1);

                m_heap.RemoveAt(m_heapSize - 1);

                m_heapSize = m_heap.Count;

                RemoveIndexFromHashTable(item, m_heapSize);

                if (m_heapSize > 0)
                {
                    Sink(0);
                }
                
            }

            return item;
        }

        public bool Peek(out TItem item)
        {
            bool res = true;

            if (m_heapSize == 0)
            {                
               res = false;
            }

            item = m_heap[0];

            return res;
        }

        public bool IsEmpty()
        { 
            return m_heapSize == 0;
        }

        public void Clear()
        {
            if (m_heapSize > 0)
            {
                m_heap.Clear();

                m_HashTable.Clear();
            }
        }

        public void Add(TItem item)
        {
            if (m_heapSize == 0)// case when heap is empty
            {
                m_heap.Add(item);

                m_heapSize = m_heap.Count;

                AddToHashTable(item, 0);
            }
            else
            {
                m_heap.Add(item);

                m_heapSize = m_heap.Count;

                AddToHashTable(item, m_heapSize - 1);
                
                Swim(m_heapSize - 1);
            }
        }

        #region Private Methods

        private void RemoveIndexFromHashTable(TItem item, int index)
        {
            if (m_HashTable.ContainsKey(item))
            {               
                int index_count = m_HashTable[item].Count;

                if (index_count == 1)
                {
                    m_HashTable.Remove(item);
                }
                else
                {
                    m_HashTable[item].Remove(index);                  
                }
            }
        }

        private int GetChildIndex(int parentIndex, int number)
        { 
            return m_Arity * parentIndex + number;
        }

        private int GetParentIndex(int ChildIndex) =>
            (int)Math.Floor((decimal)(ChildIndex - 1) / (decimal)m_Arity);

        private void Sink(int ParentIndex)
        {
            //Get Neighbors

            var parent = m_heap[ParentIndex];

            List<TItem> children = new List<TItem>();

            children.Add(parent);

            for (int i = 1; i <= m_Arity; i++)
            {
                var index = GetChildIndex(ParentIndex, i);

                if (index >= m_heapSize)
                {
                    break;
                }
                else
                {
                    children.Add(m_heap[index]);
                }                
            }

            if (children.Count > 1)
            {
                var min = children.Min();

                if (!(parent.CompareTo(min) == 0))
                {
                    var childIndex = m_heap.IndexOf(min);

                    Swap(ParentIndex, childIndex);

                    Sink(childIndex);
                }

                
            }

            



        }

        private void Swim(int ChildIndex)
        {
            var parent = GetParentIndex(ChildIndex);

            if (ChildIndex > 0 && (m_heap[(int)parent].CompareTo(m_heap[ChildIndex]) == 1))
            {
                Swap(parent, ChildIndex);

                Swim(parent);
            }
        }

        /// <summary>
        /// Element at i changed to element at j
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void Swap(int i, int j)
        {
            var item1 = m_heap[i];

            var item2 = m_heap[j];

            m_heap[i] = item2;

            m_heap[j] = item1;

            SwapIndexesInHashTableLists(item1, i, j);

            SwapIndexesInHashTableLists(item2, j, i);
        }



        private void SwapIndexesInHashTableLists(TItem item, int index, int newIndex)
        {
            var list = m_HashTable[item];

            int count = list.Count;

            for (int k = 0; k < count; k++)
            {
                if (list[k] == index)
                {
                    list[k] = newIndex;

                    break;
                }
            }
        }

        private void AddToHashTable(TItem item, int index)
        {
            if (m_HashTable.ContainsKey(item))
            {
                m_HashTable[item].Add(index);
            }
            else
            {
                m_HashTable.Add(item, new List<int>() { index });
            }
        }

        #endregion



        #endregion
    }
}
