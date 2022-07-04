using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private Node _rootNode;

        public int TopIndex => Length - 1;

        public int Length
        {
            get
            {
                if (_rootNode == null)
                {
                    return 0;
                }

                var len = 1;
                var current = _rootNode;
                while (current.Next != null)
                {
                    len++;
                    current = current.Next;
                }
                return len;
            }
        }

        public void Add(T e)
        {
            if (_rootNode == null)
            {
                _rootNode = new Node(e, Length);
                return;
            }

            var current = _rootNode;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = new Node(e, Length);
            current.Next.Prev = current;
        }

        public void AddAt(int index, T e)
        {
            if (_rootNode == null)
            {
                _rootNode = new Node(e, Length);
                return;
            }

            var newNode = new Node(e, index);
            var current = _rootNode;

            while (current.Next != null && current.Index < index)
            {
                current = current.Next;
            }

            if (current.Prev == null)
            {
                _rootNode = newNode;
                newNode.Next = current;
                newNode.Prev = null;
                current.Prev = newNode;
                IncrementIndicesRight(current);
                return;
            }

            newNode.Next = current;
            newNode.Prev = current.Prev;
            newNode.Prev.Next = newNode;
            current.Prev = newNode;
            IncrementIndicesRight(current);
        }

        public T ElementAt(int index)
        {
            if (_rootNode == null || index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            var current = _rootNode;

            while (current.Index < index && current.Next != null)
            {
                current = current.Next;
            }

            return current.Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnum<T>(this);
        }

        public void Remove(T item)
        {
            var current = _rootNode;
            while (current.Next != null && !current.Data.Equals(item))
            {
                current = current.Next;
            }

            if (!current.Data.Equals(item))
            {
                return;
            }

            if (current.Prev == null)
            {
                current.Next.Prev = null;
                _rootNode = current.Next;
                DecrementIndicesRight(_rootNode);
                return;
            }

            if (current.Next == null)
            {
                current.Prev.Next = current.Next;
                return;
            }

            current.Next.Prev = current.Prev;
            current.Prev.Next = current.Next;
        }

        public T RemoveAt(int index)
        {
            if (_rootNode == null || index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            var current = _rootNode;
            while (current.Index < index)
            {
                current = current.Next;
            }

            if (current.Index != index)
            {
                return default;
            }

            if (current.Prev == null)
            {
                current.Next.Prev = current.Prev;
                _rootNode = current.Next;
                DecrementIndicesRight(_rootNode);
                return current.Data;
            }

            if (current.Next == null)
            {
                current.Prev.Next = current.Next;
                return current.Data;
            }

            current.Prev.Next = current.Next;
            current.Next.Prev = current.Prev;
            return current.Data;
        }

        private void IncrementIndicesRight(Node node)
        {
            node.Index++;

            if (node.Next == null)
            {
                return;
            }

            var current = node.Next;
            while (current.Next != null)
            {
                current.Index++;
                current = current.Next;
            }
        }

        private void DecrementIndicesRight(Node node)
        {
            node.Index--;

            if (node.Next == null)
            {
                return;
            }

            var current = node.Next;
            while (current.Next != null)
            {
                current.Index--;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class Node
        {
            public T Data;
            public Node Prev;
            public Node Next;
            public int Index;

            public Node(T value, int index)
            {
                Data = value;
                Index = index;
            }
        }
    }
}
