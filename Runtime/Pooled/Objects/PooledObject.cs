using Depra.ObjectPooling.Runtime.Pooled.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Interfaces;
using JetBrains.Annotations;

namespace Depra.ObjectPooling.Runtime.Pooled.Objects
{
    [PublicAPI]
    public abstract class PooledObject : IPooled
    {
        protected IPool Pool { get; private set; }
        
        public void OnPoolCreate(IPool pool)
        {
            Pool = pool;
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