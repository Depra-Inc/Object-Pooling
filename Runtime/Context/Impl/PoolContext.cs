using System;
using Depra.ObjectPooling.Runtime.Context.Interfaces;
using Depra.ObjectPooling.Runtime.Internal.Buffers.Impl;
using Depra.ObjectPooling.Runtime.Internal.Buffers.Interfaces;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Abstract;
using Depra.ObjectPooling.Runtime.Pools.Structs;

namespace Depra.ObjectPooling.Runtime.Context.Impl
{
    public class PoolContext<T> : IPoolContext<T> where T : IPooled
    {
        public PoolBase<T> Pool { get; }
        
        public IInstanceBuffer<T> ActiveInstances { get; }
        public IInstanceBuffer<T> PassiveInstances { get; }

        public void Clear(Action<PooledInstance<T>> onClear)
        {
            ClearCollection(ActiveInstances, onClear);
            ClearCollection(PassiveInstances, onClear);
        }

        private static void ClearCollection(IInstanceBuffer<T> instances, Action<PooledInstance<T>> onClear)
        {
            var allInstances = instances.GetAll();
            foreach (var instance in allInstances)
            {
                onClear.Invoke(instance);
            }

            instances.Dispose();
        }

        public PoolContext(PoolBase<T> pool, BorrowStrategy borrowStrategy, int capacity)
        {
            Pool = pool;

            switch (borrowStrategy)
            {
                case BorrowStrategy.LIFO:
                    ActiveInstances = new InstanceStack<T>(capacity);
                    PassiveInstances = new InstanceStack<T>(capacity);
                    break;
                case BorrowStrategy.FIFO:
                    ActiveInstances = new InstanceQueue<T>(capacity);
                    PassiveInstances = new InstanceQueue<T>(capacity);
                    break;
                case BorrowStrategy.Random:
                    ActiveInstances = new InstanceBag<T>(capacity);
                    PassiveInstances = new InstanceBag<T>(capacity);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(borrowStrategy), borrowStrategy, null);
            }
        }
    }
}