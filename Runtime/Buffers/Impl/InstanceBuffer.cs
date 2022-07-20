using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Runtime.Buffers.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Structs;

namespace Depra.ObjectPooling.Runtime.Buffers.Impl
{
    public class InstanceBuffer<TSource> : IInstanceBuffer<PooledInstance<TSource>> where TSource : IPooled
    {
        private readonly Stack<PooledInstance<TSource>> _objectsFree;
        private readonly Stack<PooledInstance<TSource>> _objectsInUse;

        public int FreeCount => _objectsFree.Count;
        public int UsedCount => _objectsInUse.Count;

        public bool HasFree => _objectsFree.Count > 0;
        public bool HasInUse => _objectsInUse.Count > 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IInstanceBuffer<PooledInstance<TSource>>.AddFree(PooledInstance<TSource> instance)
        {
            _objectsFree.Push(instance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        PooledInstance<TSource> IInstanceBuffer<PooledInstance<TSource>>.RemoveFree()
        {
            if (_objectsFree.Count == 0)
            {
                throw new Exception("Collection of free instances is empty!");
            }
            
            return _objectsFree.Pop();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IInstanceBuffer<PooledInstance<TSource>>.AddInUse(PooledInstance<TSource> instance)
        {
            _objectsInUse.Push(instance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PooledInstance<TSource> RemoveInUse()
        {
            if (_objectsInUse.Count == 0)
            {
                throw new Exception("Collection of free instances is empty!");
            }

            return _objectsInUse.Pop();
        }

        public IReadOnlyCollection<PooledInstance<TSource>> GetAllFree() => _objectsFree;

        public IReadOnlyCollection<PooledInstance<TSource>> GetAllInUse() => _objectsInUse;

        public void Clear()
        {
            _objectsFree.Clear();
            _objectsInUse.Clear();
        }

        public InstanceBuffer(int capacity)
        {
            _objectsFree = new Stack<PooledInstance<TSource>>(capacity);
           _objectsInUse = new Stack<PooledInstance<TSource>>(capacity);
        }

        public void Dispose() => Clear();
    }
}