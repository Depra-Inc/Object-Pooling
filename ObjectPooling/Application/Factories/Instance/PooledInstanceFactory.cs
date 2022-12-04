// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.ObjectPooling.Application.Factories.Obj;
using Depra.ObjectPooling.Application.Internal.Buffers.Interfaces;
using Depra.ObjectPooling.Application.Internal.Helpers;
using Depra.ObjectPooling.Application.Pools.Interfaces;
using Depra.ObjectPooling.Domain.Entities;
using Depra.ObjectPooling.Domain.Exceptions;
using Depra.ObjectPooling.Domain.Structs;

namespace Depra.ObjectPooling.Application.Factories.Instance
{
    public readonly struct PooledInstanceFactory<T> : IPooledInstanceFactory<T> where T : IPooled
    {
        private readonly IPoolContext<T> _context;
        private readonly IPooledObjectFactory<T> _objectFactory;

        private IInstanceBuffer<T> ActiveBuffer => _context.ActiveInstances;
        private IInstanceBuffer<T> PassiveBuffer => _context.PassiveInstances;

        public PooledInstance<T> MakeActiveInstance(out bool reuse)
        {
            PooledInstance<T> instance;

            if (PassiveBuffer.Count > 0)
            {
                reuse = true;
                instance = PassiveBuffer.GetInstance();
            }
            else
            {
                reuse = false;
                instance = ConstructInstance();
            }

            ActivateInstance(instance);

            return instance;
        }

        public PooledInstance<T> MakePassiveInstance()
        {
            if (ActiveBuffer.Count <= 0)
            {
                _context.HandleException(new NotEnoughActiveObjectsInPoolException(_context.Pool.Key, typeof(T)));
            }

            var instance = ActiveBuffer.GetInstance();
            PassivateInstance(instance);

            return instance;
        }

        public PooledInstance<T> MakePassiveInstance(T obj)
        {
            var instance = obj.ToInstance(_context.Pool);
            PassivateInstance(instance);

            return instance;
        }

        public void DestroyInstance(ref PooledInstance<T> instance)
        {
            var obj = instance.Obj;
            _objectFactory.OnDisableObject(_context.Pool.Key, obj);
            _objectFactory.DestroyObject(_context.Pool.Key, obj);
        }

        public PooledInstanceFactory(IPoolContext<T> context, IPooledObjectFactory<T> objectFactory)
        {
            _context = context;
            _objectFactory = objectFactory;
        }

        private PooledInstance<T> ConstructInstance()
        {
            var obj = _objectFactory.CreateObject(_context.Pool);
            var instance = new PooledInstance<T>(_context.Pool, obj);

            return instance;
        }

        private void ActivateInstance(PooledInstance<T> instance)
        {
            instance.Activate();
            ActiveBuffer.AddInstance(ref instance);
        }

        private void PassivateInstance(PooledInstance<T> instance)
        {
            instance.Deactivate();
            PassiveBuffer.AddInstance(ref instance);
        }
    }
}