// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.ObjectPooling.Application.Configuration.Interfaces;
using Depra.ObjectPooling.Application.Factories.Instance;
using Depra.ObjectPooling.Application.Factories.Obj;
using Depra.ObjectPooling.Application.Guard;
using Depra.ObjectPooling.Application.Internal.Buffers.Impl;
using Depra.ObjectPooling.Application.Pools.Abstract;
using Depra.ObjectPooling.Application.Pools.Impl;
using Depra.ObjectPooling.Application.Pools.Interfaces;
using Depra.ObjectPooling.Domain.Entities;

namespace Depra.ObjectPooling.Application.Configuration.Impl
{
    public class PoolConfiguration<T> : IPoolConfiguration<T> where T : IPooled
    {
        private readonly int _capacity;
        private readonly BorrowStrategy _borrowStrategy;
        private readonly IPooledObjectFactory<T> _objectFactory;

        public PoolConfiguration(IPooledObjectFactory<T> objectFactory, BorrowStrategy borrowStrategy, int capacity)
        {
            _capacity = capacity;
            _borrowStrategy = borrowStrategy;
            _objectFactory = objectFactory;
        }

        protected virtual PoolGuard Guard => new SystemPoolGuard();

        public IPoolContext<T> GetContext(Pool<T> pool) =>
            new PoolContext<T>(pool, Guard, _borrowStrategy, _capacity);

        public IPooledInstanceFactory<T> GetFactory(IPoolContext<T> context) =>
            new PooledInstanceFactory<T>(context, _objectFactory);
    }
}