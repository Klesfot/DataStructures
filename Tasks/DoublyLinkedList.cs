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
                IncrementIndicesRight(current);
                return;
            }
            
            _rootNode = newNode;
            newNode.next = current;
            newNode.prev = null;
            current.prev = newNode;
            IncrementIndicesRight(current);
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
            return new DoublyLinkedListEnum<T>(this);
        }

        public void Remove(T item)
        {
            Node current = _rootNode;
            do
            {
                if (current.data.Equals(item) && current.prev == null)
                {
                    current.next.prev = current.prev;
                    _rootNode = current.next;
                    DecrementIndicesRight(_rootNode);
                    return;
                }

                if (current.data.Equals(item) && current.next == null)
                {
                    current.prev.next = current.next;
                    return;
                }

                if (current.data.Equals(item) && current.next != null && current.prev == null)
                {
                    current.next.prev = current.prev;
                    current.prev.next = current.next;
                    return;
                }

                current = current.next;
            } while (current != null);
        }

        public T RemoveAt(int index)
        {
            if (_rootNode == null || index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            Node current = _rootNode;
            while (current.index <= index)
            {
                if (current.index == index && current.prev == null)
                {
                    current.next.prev = current.prev;
                    _rootNode = current.next;
                    DecrementIndicesRight(_rootNode);
                    return current.data;
                }

                if (current.index == index && current.next == null)
                {
                    current.prev.next = current.next;
                    return current.data;
                }

                if (current.index == index)
                {
                    current.prev.next = current.next;
                    current.next.prev = current.prev;
                    return current.data;
                }
                current = current.next;
            }

            return default;
        }

        private void IncrementIndicesRight(Node node)
        {
            node.index++;

            if (node.next == null)
            {
                return;
            }

            var current = node.next;
            while (current.next != null)
            {
                current.index++;
                current = current.next;
            }
        }

        private void DecrementIndicesRight(Node node)
        {
            node.index--;

            if (node.next == null)
            {
                return;
            }

            var current = node.next;
            while (current.next != null)
            {
                current.index--;
                current = current.next;
            }
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
