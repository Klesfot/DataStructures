using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private DoublyLinkedList<T> _storage;
        public T Dequeue()
        {
            if (_storage == null || _storage.ElementAt(0) == null)
            {
                throw new InvalidOperationException();
            }
            
            return _storage.RemoveAt(0);
        }

        public void Enqueue(T item)
        {
            EnsureStorageIsCreated();

            _storage.Add(item);
        }

        public T Pop()
        {
            if (_storage == null || _storage.ElementAt(0) == null)
            {
                throw new InvalidOperationException();
            }

            return _storage.RemoveAt(_storage.TopIndex);
        }

        public void Push(T item)
        {
            EnsureStorageIsCreated();

            _storage.Add(item);
        }

        private void EnsureStorageIsCreated()
        {
            _storage ??= new DoublyLinkedList<T>();
        }
    }
}
