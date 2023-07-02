using Heaps;

namespace PriorityQueues
{
    public class PriorityQueueItem<TItem, TPriority> : 
        IComparable<PriorityQueueItem<TItem, TPriority>>,
        IEquatable<PriorityQueueItem<TItem, TPriority>>
        where TPriority : IComparable<TPriority>, IEquatable<TPriority>
    {
        #region Fields

        TItem m_item;

        TPriority m_priority;

        #endregion

        #region Properties

        public TItem Item { get=> m_item; set=> m_item = value; }

        public TPriority Priority { get=> m_priority; set=> m_priority = value; }

        #endregion

        #region ctor

        public PriorityQueueItem(TItem item, TPriority priority)
        {
            m_item = item;

            m_priority = priority;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{m_item} {m_priority}";
        }

        public int CompareTo(PriorityQueueItem<TItem, TPriority>? other)
        {
            return this.m_priority.CompareTo(other.m_priority);
        }

        public bool Equals(PriorityQueueItem<TItem, TPriority>? other)
        {
            return m_priority.Equals(other.m_priority);
        }

        #endregion
    }

    public class PriorityQueue<TItem, TPriority>
        where TPriority : IComparable<TPriority>, IEquatable<TPriority>
    {
        #region Fields

        Heap<PriorityQueueItem<TItem, TPriority>> m_heap;

        int m_QueueSize;

        int m_Arity;

        #endregion

        #region Properties

        public int Ariy { get=> m_Arity;}

        public int QueueSize { get=> m_QueueSize; }

        #endregion

        #region Ctor
        public PriorityQueue(int arity)
        {
            m_Arity = arity;

            m_heap = new Heap<PriorityQueueItem<TItem, TPriority>>(m_Arity);

            m_QueueSize = m_heap.HeapSize;
        }
        #endregion

        #region Methods

        public virtual void Enqueue(TItem item, TPriority priority)
        {
            PriorityQueueItem<TItem, TPriority> queueItem =
                new PriorityQueueItem<TItem, TPriority>(item, priority);

            m_heap.Add(queueItem);

            m_QueueSize = m_heap.HeapSize;
        }

        public virtual TItem Dequeue()
        {
            var item =  m_heap.Poll().Item;

            m_QueueSize = m_heap.HeapSize;

            return item;
        }

        public bool isEmpty()
        {
            return m_QueueSize == 0;
        }

        public bool Peek(out TItem item)
        {            
            PriorityQueueItem<TItem, TPriority> queueElem = null;

            var res = m_heap.Peek(out queueElem);

            item = queueElem.Item;

            return res;
        }
        
        #endregion
    }
}