using Depra.ObjectPooling.Runtime.Context.Interfaces;
using Depra.ObjectPooling.Runtime.Factories.Instance.Interfaces;
using Depra.ObjectPooling.Runtime.Pooled.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Abstract;

namespace Depra.ObjectPooling.Runtime.Configuration.Interfaces
{
    public interface IPoolConfiguration<T> where T : IPooled
    {
        IPoolContext<T> GetContext(PoolBase pool);

        IPooledInstanceFactory<T> GetFactory(IPoolContext<T> context);
    }
}