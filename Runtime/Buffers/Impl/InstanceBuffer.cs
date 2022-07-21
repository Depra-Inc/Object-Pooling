using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Runtime.Buffers.Interfaces;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Structs;

namespace Depra.ObjectPooling.Runtime.Buffers.Impl
{
    public class InstanceBuffer<TSource> : IInstanceBuffer<PooledInstance<TSource>> where TSource : IPooled
    {
        private readonly Stack<PooledInstance<TSource>> _objectsFree;
        private readonly Stack<PooledInstance<TSource>> _objectsInUse;

        public int FreeCount => _objectsFree.Count;
        public int UsedCount => _objectsInUse.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IInstanceBuffer<PooledInstance<TSource>>.AddFree(PooledInstance<TSource> instance)
        {
            _objectsFree.Push(instance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool IInstanceBuffer<PooledInstance<TSource>>.TryRemoveFree(out PooledInstance<TSource> instance)
        {
            if (_objectsFree.Count > 0)
            {
                instance = _objectsFree.Pop();
                return true;
            }

            instance = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IInstanceBuffer<PooledInstance<TSource>>.AddInUse(PooledInstance<TSource> instance)
        {
            _objectsInUse.Push(instance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool IInstanceBuffer<PooledInstance<TSource>>.TryRemoveUsed(out PooledInstance<TSource> instance)
        {
            if (_objectsInUse.Count > 0)
            {
                instance = _objectsInUse.Pop();
                return true;
            }

            instance = default;
            return false;
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