using Depra.ObjectPooling.Runtime.Context.Interfaces;
using Depra.ObjectPooling.Runtime.Factories.Instance.Interfaces;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Abstract;

namespace Depra.ObjectPooling.Runtime.Configuration.Interfaces
{
    public interface IPoolConfiguration<T> where T : IPooled
    {
        IPoolContext<T> GetContext(PoolBase<T> pool);

        IPooledInstanceFactory<T> GetFactory(IPoolContext<T> context);
    }
}