using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private Node _rootNode;
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
                while (current.next != null)
                {
                    len++;
                    current = current.next;
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

            Node current = _rootNode;
            while (current.next != null)
            {
                current = current.next;
            }
            current.next = new Node(e, Length);
            current.next.prev = current;
        }

        public void AddAt(int index, T e)
        {
            if (_rootNode == null)
            {
                _rootNode = new Node(e, Length);
                return;
            }

            Node current = _rootNode;
            while (current.next != null && current.index < index)
            {
                current = current.next;
            }

            if (current.index == Length)
            {
                current.next = new Node(current.data, Length);
                current.next.prev = current;
                current.data = e;
                return;
            }

            var newNode = new Node(e, index);

            if (current.prev != null)
            {
                newNode.next = current;
                newNode.prev = current.prev;
                newNode.prev.next = newNode;
                current.prev = newNode;
                current.index++;
                return;
            }
            
            _rootNode = newNode;
            newNode.next = current;
            newNode.prev = current.prev;
            current.prev = newNode;
            current.index++;
        }

        public T ElementAt(int index)
        {
            if (_rootNode == null || index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            Node current = _rootNode;
            while (current.index < index && current.next != null)
            {
                current = current.next;
            }

            return current.data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Remove(T item)
        {
            throw new NotImplementedException();
        }

        public T RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class Node
        {
            public T data;
            public Node prev;
            public Node next;
            public int index;

            public Node(T value, int index)
            {
                data = value;
                this.index = index;
            }
        }
    }
}
