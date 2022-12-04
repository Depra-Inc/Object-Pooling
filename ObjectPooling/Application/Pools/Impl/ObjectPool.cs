// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Depra.ObjectPooling.Application.Configuration.Interfaces;
using Depra.ObjectPooling.Application.Factories.Instance;
using Depra.ObjectPooling.Application.Pools.Abstract;
using Depra.ObjectPooling.Application.Pools.Interfaces;
using Depra.ObjectPooling.Domain.Entities;
using Depra.ObjectPooling.Domain.Exceptions;
using Depra.ObjectPooling.Domain.Structs;

namespace Depra.ObjectPooling.Application.Pools.Impl
{
    public sealed class ObjectPool<T> : Pool<T> where T : IPooled
    {
        private readonly IPoolContext<T> _context;
        private readonly IPooledInstanceFactory<T> _instanceFactory;

        public ObjectPool(object key, IPoolConfiguration<T> configuration) : base(key)
        {
            _context = configuration.GetContext(this);
            _instanceFactory = configuration.GetFactory(_context);
        }

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
                _context.HandleException(new PoolObjectNullReferenceException());
                return;
            }

            var instance = _instanceFactory.MakePassiveInstance();
            OnObjectReleased(instance.Obj);
        }

        protected override void HandleException(Exception exception) => _context.HandleException(exception);

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
    }
}