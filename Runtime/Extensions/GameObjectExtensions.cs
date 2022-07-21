using Depra.ObjectPooling.Runtime.PooledObjects.Impl;
using Depra.ObjectPooling.Runtime.Pools.Impl;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Recycle this GameObject, if it belongs to a pool.
        /// </summary>
        public static void Recycle(this GameObject self)
        {
            if (self.TryGetComponent(out PooledGameObject pooledGameObject))
            {
                pooledGameObject.OnPoolReuse();
            }
        }
    }
}