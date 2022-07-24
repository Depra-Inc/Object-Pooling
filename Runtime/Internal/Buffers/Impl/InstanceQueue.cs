using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Runtime.Internal.Buffers.Interfaces;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Structs;

namespace Depra.ObjectPooling.Runtime.Internal.Buffers.Impl
{
    internal class InstanceQueue<T> : IInstanceBuffer<T> where T : IPooled
    {
        private readonly Queue<PooledInstance<T>> _instances;

        public int Count => _instances.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(ref PooledInstance<T> instance) => _instances.Contains(instance);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PushInstance(ref PooledInstance<T> instance) => _instances.Enqueue(instance);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PooledInstance<T> PopInstance() => _instances.Dequeue();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<PooledInstance<T>> GetAll() => _instances;

        public InstanceQueue(int capacity)
        {
            _instances = new Queue<PooledInstance<T>>(capacity);
        }

        public void Dispose() => _instances.Clear();
    }
}