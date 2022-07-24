using System.Collections.Generic;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Impl;

namespace Depra.ObjectPooling.Runtime.Extensions
{
    public static class ObjectPoolExtensions
    {
        public static void AddFreeRange<T>(this ObjectPool<T> pool, IEnumerable<T> collection) where T : IPooled
        {
            foreach (var item in collection)
            {
                pool.AddFreeObject(item);
            }
        }
    }
}