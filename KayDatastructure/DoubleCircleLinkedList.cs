/********************************************************************************
** All rights reserved
** Auth： kay.yang
** E-mail: 1025115216@qq.com
** Date： 8/2/2017 2:34:09 PM
** Version:  v1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KayDatastructure
{
    public class DoubleCircleLinkedList<T>
    {
        private LinkedList<T> mDataList;
        public DoubleCircleLinkedList()
        {
            mDataList = new LinkedList<T>();
        }

        public int Count
        {
            get
            {
                return mDataList.Count;
            }
        }

        public void RemoveLast()
        {
            mDataList.RemoveLast();
        }
        public void RemoveFirst()
        {
            mDataList.RemoveFirst();
        }

        public void Remove(LinkedListNode<T> node)
        {
            mDataList.Remove(node);
        }

        public void AddLast(T node)
        {
            mDataList.AddLast(node);
        }
        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> value)
        {
            mDataList.AddAfter(node, value);
        }
        public void AddAfter(LinkedListNode<T> node, T value)
        {
            mDataList.AddAfter(node, value);
        }
        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> value)
        {
            mDataList.AddBefore(node, value);
        }
        public void AddBefore(LinkedListNode<T> node, T value)
        {
            mDataList.AddBefore(node, value);
        }
        public void AddFirst(T node)
        {
            mDataList.AddFirst(node);
        }
        public LinkedListNode<T> Next(LinkedListNode<T> node)
        {
            if (node.Next == null)
                return mDataList.First;
            return node.Next;
        }
        public LinkedListNode<T> Previous(LinkedListNode<T> node)
        {
            if (node.Previous == null)
                return mDataList.Last;
            return node.Previous;
        }
        public LinkedListNode<T> Last
        {
            get
            {
                return mDataList.Last;
            }
        }
        public LinkedListNode<T> First
        {
            get
            {
                return mDataList.First;
            }
        }


    }
}
