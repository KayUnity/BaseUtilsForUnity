/********************************************************************************
** All rights reserved
** Auth： kay.yang
** E-mail: 1025115216@qq.com
** Date： 6/30/2017 11:13:04 AM
** Version:  v1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace KayDatastructure
{
    public sealed class KeyedPriorityQueueEventArgs<T> : EventArgs // where T : class
    {
        private T _newFirstElement;
        private T _oldFirstElement;
        public KeyedPriorityQueueEventArgs(T oldFirstElement, T newFirstElement)
        {
            this._oldFirstElement = oldFirstElement;
            this._newFirstElement = newFirstElement;
        }
        public T mNewFirstElement
        {
            get
            {
                return this._newFirstElement;
            }
        }
        public T mOldFirstElement
        {
            get
            {
                return this._oldFirstElement;
            }
        }
    }

    [Serializable]
    public class KeyedPriorityQueue<K, V, P> // where V : class
    {
        private List<HeapNode<K, V, P>> mPriorityHeap;
        private HeapNode<K, V, P> mPlaceHolder;
        private Comparer<P> mPriorityComparer;
        private int mSize;
        // public event EventHandler<KeyedPriorityQueueEventArgs<V>> mFirstElementChanged;
        public KeyedPriorityQueue()
        {
            this.mPriorityHeap = new List<HeapNode<K, V, P>>();
            this.mPriorityComparer = Comparer<P>.Default;
            this.mPlaceHolder = new HeapNode<K, V, P>();
            this.mPriorityHeap.Add(new HeapNode<K, V, P>());
        }

        public KeyedPriorityQueue(Comparer<P> compair)
        {
            this.mPriorityHeap = new List<HeapNode<K, V, P>>();
            this.mPriorityComparer = compair;
            this.mPlaceHolder = new HeapNode<K, V, P>();
            this.mPriorityHeap.Add(new HeapNode<K, V, P>());
        }

        public void Clear()
        {
            this.mPriorityHeap.Clear();
            this.mPriorityHeap.Add(new HeapNode<K, V, P>());
            this.mSize = 0;
        }

        public V Dequeue()
        {
            V local = (this.mSize < 1) ? default(V) : this.DequeueImpl();
            V newHead = (this.mSize < 1) ? default(V) : this.mPriorityHeap[1].Value;
            // this.RaiseHeadChangedEvent(default(V), newHead);
            return local;
        }

        private V DequeueImpl()
        {
            V local = this.mPriorityHeap[1].Value;
            this.mPriorityHeap[1] = this.mPriorityHeap[this.mSize];
            this.mPriorityHeap[this.mSize--] = this.mPlaceHolder;
            this.Heapify(1);
            return local;
        }

        public void Enqueue(K key, V value, P priority)
        {
            V local = (this.mSize > 0) ? this.mPriorityHeap[1].Value : default(V);
            int num = ++this.mSize;
            int num2 = num >> 1;
            if (num == this.mPriorityHeap.Count)
            {
                this.mPriorityHeap.Add(this.mPlaceHolder);
            }
            while ((num > 1) && this.IsHigher(priority, this.mPriorityHeap[num2].Priority))
            {
                this.mPriorityHeap[num] = this.mPriorityHeap[num2];
                num = num2;
                num2 = num >> 1;
            }
            this.mPriorityHeap[num] = new HeapNode<K, V, P>(key, value, priority);
            V newHead = this.mPriorityHeap[1].Value;
            //if (!newHead.Equals(local))
            //{
            //    this.RaiseHeadChangedEvent(local, newHead);
            //}
        }

        public V FindByPriority(P priority, Predicate<V> match)
        {
            if (this.mSize >= 1)
            {
                return this.Search(priority, 1, match);
            }
            return default(V);
        }

        private void Heapify(int i)
        {
            int num = i << 1;
            int num2 = num + 1;
            int j = i;
            if ((num <= this.mSize) && this.IsHigher(this.mPriorityHeap[num].Priority, this.mPriorityHeap[i].Priority))
            {
                j = num;
            }
            if ((num2 <= this.mSize) && this.IsHigher(this.mPriorityHeap[num2].Priority, this.mPriorityHeap[j].Priority))
            {
                j = num2;
            }
            if (j != i)
            {
                this.Swap(i, j);
                this.Heapify(j);
            }
        }

        protected virtual bool IsHigher(P p1, P p2)
        {
            return (this.mPriorityComparer.Compare(p1, p2) < 1);
        }

        public V Peek()
        {
            if (this.mSize >= 1)
            {
                return this.mPriorityHeap[1].Value;
            }
            return default(V);
        }

        private void RaiseHeadChangedEvent(V oldHead, V newHead)
        {
            //if (!oldHead.Equals(newHead))
            //{
            //    EventHandler<KeyedPriorityQueueEventArgs<V>> firstElementChanged = this.mFirstElementChanged;
            //    if (firstElementChanged != null)
            //    {
            //        firstElementChanged(this, new KeyedPriorityQueueEventArgs<V>(oldHead, newHead));
            //    }
            //}
        }

        public bool Contain(K key)
        {
            if (this.mSize >= 1)
            {
                for (int i = 1; i <= this.mSize; i++)
                {
                    if (this.mPriorityHeap[i].Key.Equals(key))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public V Remove(K key)
        {
            if (this.mSize >= 1)
            {
                V oldHead = this.mPriorityHeap[1].Value;
                for (int i = 1; i <= this.mSize; i++)
                {
                    if (this.mPriorityHeap[i].Key.Equals(key))
                    {
                        V local2 = this.mPriorityHeap[i].Value;
                        this.Swap(i, this.mSize);
                        this.mPriorityHeap[this.mSize--] = this.mPlaceHolder;
                        this.Heapify(i);
                        //V local3 = this.mPriorityHeap[1].Value;
                        //if (!oldHead.Equals(local3))
                        //{
                        //    this.RaiseHeadChangedEvent(oldHead, local3);
                        //}
                        return local2;
                    }
                }
            }
            return default(V);
        }

        private V Search(P priority, int i, Predicate<V> match)
        {
            V local = default(V);
            if (this.IsHigher(this.mPriorityHeap[i].Priority, priority))
            {
                if (match(this.mPriorityHeap[i].Value))
                {
                    local = this.mPriorityHeap[i].Value;
                }
                int num = i << 1;
                int num2 = num + 1;
                if ((local == null) && (num <= this.mSize))
                {
                    local = this.Search(priority, num, match);
                }
                if ((local == null) && (num2 <= this.mSize))
                {
                    local = this.Search(priority, num2, match);
                }
            }
            return local;
        }

        private void Swap(int i, int j)
        {
            HeapNode<K, V, P> node = this.mPriorityHeap[i];
            this.mPriorityHeap[i] = this.mPriorityHeap[j];
            this.mPriorityHeap[j] = node;
        }

        public int Count
        {
            get
            {
                return this.mSize;
            }
        }

        public ReadOnlyCollection<K> Keys
        {
            get
            {
                List<K> list = new List<K>();
                for (int i = 1; i <= this.mSize; i++)
                {
                    list.Add(this.mPriorityHeap[i].Key);
                }
                return new ReadOnlyCollection<K>(list);
            }
        }

        public ReadOnlyCollection<V> Values
        {
            get
            {
                List<V> list = new List<V>();
                for (int i = 1; i <= this.mSize; i++)
                {
                    list.Add(this.mPriorityHeap[i].Value);
                }
                return new ReadOnlyCollection<V>(list);
            }
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        private struct HeapNode<KK, VV, PP>
        {
            public KK Key;
            public VV Value;
            public PP Priority;
            public HeapNode(KK key, VV value, PP priority)
            {
                this.Key = key;
                this.Value = value;
                this.Priority = priority;
            }
        }

    }
}
