using System.Collections.Generic;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Abstract;

namespace Depra.ObjectPooling.Runtime.Extensions
{
    public static class GeneralPoolExtensions
    {
        public static void WarmUp<T>(this PoolBase<T> pool, int count) where T : IPooled
        {
            var objects = pool.RequestRange(count);
            pool.ReleaseRange(objects);
        }

        public static IEnumerable<T> RequestRange<T>(this PoolBase<T> pool, int count) where T : IPooled
        {
            var result = new T[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = pool.RequestObject();
            }

            return result;
        }

        /// <summary>
        /// Releases all objects in the list, the list should be cleared afterwards.
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="collection"></param>
        public static void ReleaseRange<T>(this PoolBase<T> pool, IEnumerable<T> collection) where T : IPooled
        {
            foreach (var item in collection)
            {
                pool.ReleaseObject(item);
            }
        }
    }
}