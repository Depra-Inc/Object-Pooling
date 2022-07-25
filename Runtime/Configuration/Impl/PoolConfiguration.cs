using Depra.ObjectPooling.Runtime.Configuration.Interfaces;
using Depra.ObjectPooling.Runtime.Context.Impl;
using Depra.ObjectPooling.Runtime.Context.Interfaces;
using Depra.ObjectPooling.Runtime.Factories.Instance.Impl;
using Depra.ObjectPooling.Runtime.Factories.Instance.Interfaces;
using Depra.ObjectPooling.Runtime.Factories.Obj.Interfaces;
using Depra.ObjectPooling.Runtime.Internal.Buffers.Impl;
using Depra.ObjectPooling.Runtime.Pooled.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Abstract;

namespace Depra.ObjectPooling.Runtime.Configuration.Impl
{
    public abstract class PoolConfiguration<T> : IPoolConfiguration<T> where T : IPooled
    {
        private readonly int _capacity;
        private readonly BorrowStrategy _borrowStrategy;
        private readonly IPooledObjectFactory<T> _objectFactory;

        public IPoolContext<T> GetContext(PoolBase pool)
        {
            var context = new PoolContext<T>(pool, _borrowStrategy, _capacity);
            return context;
        }

        public IPooledInstanceFactory<T> GetFactory(IPoolContext<T> context)
        {
            var factory = new PooledInstanceFactory<T>(context, _objectFactory);
            return factory;
        }

        public PoolConfiguration(IPooledObjectFactory<T> objectFactory, BorrowStrategy borrowStrategy, int capacity)
        {
            _capacity = capacity;
            _borrowStrategy = borrowStrategy;
            _objectFactory = objectFactory;
        }
    }
}