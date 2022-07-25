using Depra.ObjectPooling.Runtime.Pooled.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Interfaces;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.Pooled.UnityObjects.Abstract
{
    public abstract class PooledMonoBehavior : MonoBehaviour, IPooled
    {
        public virtual void OnPoolCreate(IPool pool)
        {
        }

        public virtual void OnPoolGet()
        {
        }

        public virtual void OnPoolSleep()
        {
        }

        public virtual void OnPoolReuse()
        {
        }
    }
}