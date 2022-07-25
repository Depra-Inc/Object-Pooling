using System;
using Depra.ObjectPooling.Runtime.Configuration.Interfaces;
using Depra.ObjectPooling.Runtime.Context.Interfaces;
using Depra.ObjectPooling.Runtime.Factories.Instance.Interfaces;
using Depra.ObjectPooling.Runtime.Pooled.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Abstract;
using Depra.ObjectPooling.Runtime.Pools.Structs;

namespace Depra.ObjectPooling.Runtime.Pools.Objects
{
    public class ObjectPool<T> : Pool<T> where T : IPooled
    {
        private readonly IPoolContext<T> _context;
        private readonly IPooledInstanceFactory<T> _instanceFactory;

        public override int CountActive => _context.ActiveInstances.Count;
        public override int CountInactive => _context.PassiveInstances.Count;

        public override T Request()
        {
            Request(out var obj);
            return obj;
        }

        public PooledInstance<T> Request(out T obj)
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

        public override void Release(T obj)
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
        }

        public void AddInactive(T obj)
        {
            _instanceFactory.MakePassiveInstance(obj);
            OnFreeObjectAdded(obj);
        }

        public ObjectPool(object key, IPoolConfiguration<T> configuration) : base(key)
        {
            _context = configuration.GetContext(this);
            _instanceFactory = configuration.GetFactory(_context);
        }
    }
}