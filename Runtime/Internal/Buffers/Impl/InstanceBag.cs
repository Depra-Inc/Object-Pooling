using System;
using System.Collections.Generic;
using Depra.ObjectPooling.Runtime.Internal.Buffers.Interfaces;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Structs;

namespace Depra.ObjectPooling.Runtime.Internal.Buffers.Impl
{
    internal class InstanceBag<T> : IInstanceBuffer<T> where T : IPooled
    {
        private readonly Random _random;
        private readonly List<PooledInstance<T>> _instances;

        public int Count => _instances.Count;

        public bool Contains(ref PooledInstance<T> instance) => _instances.Contains(instance);

        public void PushInstance(ref PooledInstance<T> instance) => _instances.Add(instance);

        public PooledInstance<T> PopInstance()
        {
            var randomIndex = GetRandomIndex();
            var randomInstance = _instances[randomIndex];
            _instances.RemoveAt(randomIndex);

            return randomInstance;
        }

        public IEnumerable<PooledInstance<T>> GetAll() => _instances;

        public InstanceBag(int capacity)
        {
            _instances = new List<PooledInstance<T>>(capacity);
        }

        public void Dispose() => _instances.Clear();

        private int GetRandomIndex()
        {
            const int minIndex = 0;
            var maxIndex = _instances.Count;
            var randomIndex = _random.Next(minIndex, maxIndex);

            return randomIndex;
        }
    }
}