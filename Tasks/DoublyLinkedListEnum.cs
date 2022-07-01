using System;
using System.Collections;
using System.Collections.Generic;

namespace Tasks
{
    public class DoublyLinkedListEnum<T> : IEnumerator<T>
    {
        public DoublyLinkedList<T> RootNode;

        int _position = -1;

        public DoublyLinkedListEnum(DoublyLinkedList<T> rootNode)
        {
            RootNode = rootNode;
        }

        public bool MoveNext()
        {
            _position++;
            return _position < RootNode.Length;
        }

        public void Reset()
        {
            _position = -1;
        }

        public T Current
        {
            get
            {
                try
                {
                    return RootNode.ElementAt(_position);
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}