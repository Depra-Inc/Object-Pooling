using System;
using Depra.ObjectPooling.Runtime.Internal.Buffers.Interfaces;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Abstract;
using Depra.ObjectPooling.Runtime.Pools.Structs;

namespace Depra.ObjectPooling.Runtime.Context.Interfaces
{
    public interface IPoolContext<T> where T : IPooled
    {
        PoolBase<T> Pool { get; }

        IInstanceBuffer<T> ActiveInstances { get; }

        IInstanceBuffer<T> PassiveInstances { get; }

        void Clear(Action<PooledInstance<T>> onClear);
    }
}