using Depra.ObjectPooling.Runtime.Factories.Obj.Impl;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.Configuration.Impl
{
    public class PrefabPoolConfiguration<T> : PoolConfiguration<T> where T : MonoBehaviour, IPooled
    {
        private readonly T _prefab;

        public PrefabPoolConfiguration(T prefab, int capacity) : base(new PrefabPooledObjectFactory<T>(prefab),
            capacity)
        {
        }
    }
}