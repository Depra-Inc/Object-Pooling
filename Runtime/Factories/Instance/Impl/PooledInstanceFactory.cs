using System;
using Depra.ObjectPooling.Runtime.Context.Interfaces;
using Depra.ObjectPooling.Runtime.Factories.Instance.Interfaces;
using Depra.ObjectPooling.Runtime.Factories.Obj.Interfaces;
using Depra.ObjectPooling.Runtime.Internal.Buffers.Interfaces;
using Depra.ObjectPooling.Runtime.Internal.Helpers;
using Depra.ObjectPooling.Runtime.Pooled.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Structs;

namespace Depra.ObjectPooling.Runtime.Factories.Instance.Impl
{
    public readonly struct PooledInstanceFactory<T> : IPooledInstanceFactory<T> where T : IPooled
    {
        private readonly IPoolContext<T> _context;
        private readonly IPooledObjectFactory<T> _objectFactory;

        private IInstanceBuffer<T> ActiveInstances => _context.ActiveInstances;
        private IInstanceBuffer<T> PassiveInstances => _context.PassiveInstances;

        public PooledInstance<T> MakeActiveInstance(out bool reuse)
        {
            PooledInstance<T> instance;

            if (PassiveInstances.Count > 0)
            {
                reuse = true;
                instance = PassiveInstances.PopInstance();
            }
            else
            {
                reuse = false;
                var obj = _objectFactory.CreateObject(_context.Pool);
                instance = new PooledInstance<T>(_context.Pool, obj);
            }

            ActivateInstance(ref instance);

            return instance;
        }

        public PooledInstance<T> MakePassiveInstance()
        {
            if (ActiveInstances.Count <= 0)
            {
                throw new NullReferenceException();
            }

            var instance = ActiveInstances.PopInstance();
            PassivateInstance(ref instance);

            return instance;
        }

        public PooledInstance<T> MakePassiveInstance(T obj)
        {
            var instance = obj.ToInstance(_context.Pool);
            PassivateInstance(ref instance);

            return instance;
        }

        public void DestroyInstance(ref PooledInstance<T> instance)
        {
            var obj = instance.Obj;
            _objectFactory.OnDisableObject(_context.Pool.Key, obj);
            _objectFactory.OnDisableObject(_context.Pool.Key, obj);
            _objectFactory.DestroyObject(_context.Pool.Key, obj);
        }

        public void ActivateInstance(ref PooledInstance<T> instance)
        {
            instance.SetActive(true);

            if (PassiveInstances.Contains(ref instance))
            {
                PassiveInstances.PopInstance();
            }

            ActiveInstances.PushInstance(ref instance);
        }

        public void PassivateInstance(ref PooledInstance<T> instance)
        {
            instance.SetActive(false);

            if (ActiveInstances.Contains(ref instance))
            {
                ActiveInstances.PopInstance();
            }

            PassiveInstances.PushInstance(ref instance);
        }

        public PooledInstanceFactory(IPoolContext<T> context, IPooledObjectFactory<T> objectFactory)
        {
            _context = context;
            _objectFactory = objectFactory;
        }
    }
}