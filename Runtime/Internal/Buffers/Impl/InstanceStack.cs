using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Runtime.Internal.Buffers.Interfaces;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Structs;

namespace Depra.ObjectPooling.Runtime.Internal.Buffers.Impl
{
    internal class InstanceStack<T> : IInstanceBuffer<T> where T : IPooled
    {
        private readonly Stack<PooledInstance<T>> _instances;

        public int Count => _instances.Count;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(ref PooledInstance<T> instance) => _instances.Contains(instance);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PushInstance(ref PooledInstance<T> instance) => _instances.Push(instance);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PooledInstance<T> PopInstance() => _instances.Pop();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<PooledInstance<T>> GetAll() => _instances;

        public InstanceStack(int capacity)
        {
            _instances = new Stack<PooledInstance<T>>(capacity);
        }

        public void Dispose()
        {
            _instances.Clear();
        }
    }
}