using System;
using System.Collections.Generic;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Structs;

namespace Depra.ObjectPooling.Runtime.Internal.Buffers.Interfaces
{
    public interface IInstanceBuffer<T> : IDisposable where T : IPooled
    {
        int Count { get; }
        
        bool Contains(ref PooledInstance<T> instance);
        
        void PushInstance(ref PooledInstance<T> instance);

        PooledInstance<T> PopInstance();

        IEnumerable<PooledInstance<T>> GetAll();
    }
}