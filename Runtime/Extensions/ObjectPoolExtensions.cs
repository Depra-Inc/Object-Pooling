using System.Collections.Generic;
using Depra.ObjectPooling.Runtime.Pools.Impl;
using Depra.ObjectPooling.Runtime.Pools.Interfaces;

namespace Depra.ObjectPooling.Runtime.Extensions
{
    public static class ObjectPoolExtensions
    {
        public static IEnumerable<T> RequestMany<T>(this ObjectPool<T> pool, int count) where T : IPooled
        {
            var result = new T[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = pool.RequestObject();
            }

            return result;
        }
        
        public static void AddRange<T>(this ObjectPool<T> pool, IEnumerable<T> collection) where T : IPooled
        {
            foreach (var item in collection)
            {
                pool.AddObject(item);
            }
        }

        public static void FreeRange<T>(this ObjectPool<T> pool, IEnumerable<T> collection) where T : IPooled
        {
            foreach (var item in collection)
            {
                pool.FreeObject(item);
            }
        }
    }
}