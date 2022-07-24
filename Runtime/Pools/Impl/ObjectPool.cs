using System;
using Depra.ObjectPooling.Runtime.Configuration.Interfaces;
using Depra.ObjectPooling.Runtime.Context.Interfaces;
using Depra.ObjectPooling.Runtime.Factories.Instance.Interfaces;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Abstract;
using Depra.ObjectPooling.Runtime.Pools.Structs;

namespace Depra.ObjectPooling.Runtime.Pools.Impl
{
    public class ObjectPool<T> : PoolBase<T> where T : IPooled
    {
        private readonly IPoolContext<T> _context;
        private readonly IPooledInstanceFactory<T> _instanceFactory;

        public override T RequestObject()
        {
            RequestObject(out var obj);
            return obj;
        }

        public PooledInstance<T> RequestObject(out T obj)
        {
            var instance = _instanceFactory.MakeActiveInstance(out var reuse);
            obj = instance.Obj;
            
            if (reuse)
            {
                OnObjectReused(obj);
            }
            else
            {
                OnObjectCreated(obj);
            }

            OnObjectRequested(obj);
            
            return instance;
        }

        public override void ReleaseObject(T obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }
            
            var instance = _instanceFactory.MakePassiveInstance();
            OnObjectReleased(instance.Obj);
        }

        public override void Clear()
        {
            _context.Clear(instance =>
            {
                instance.Obj.OnPoolSleep();
                _instanceFactory.DestroyInstance(ref instance);
            });
            
            CountAll = 0;
        }

        public void AddFreeObject(T obj)
        {
            var instance = _instanceFactory.MakePassiveInstance(obj);
            OnFreeObjectAdded(obj);
        }

        public ObjectPool(object key, IPoolConfiguration<T> configuration) : base(key)
        {
            _context = configuration.GetContext(this);
            _instanceFactory = configuration.GetFactory(_context);
        }
    }
}