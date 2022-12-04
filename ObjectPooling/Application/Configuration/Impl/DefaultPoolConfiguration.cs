// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Depra.ObjectPooling.Application.Factories.Obj;
using Depra.ObjectPooling.Application.Guard;
using Depra.ObjectPooling.Application.Internal.Buffers.Impl;
using Depra.ObjectPooling.Domain.Entities;

namespace Depra.ObjectPooling.Application.Configuration.Impl
{
    public class DefaultPoolConfiguration<T> : PoolConfiguration<T> where T : IPooled
    {
        private const BorrowStrategy STRATEGY = BorrowStrategy.LIFO;

        private static IPooledObjectFactory<T> CreateObjectFactory(Func<T> createFunc,
            Action<T> onRequest,
            Action<T> onRelease,
            Action<T> onDestroy) =>
            new CustomPooledObjectFactory<T>(createFunc, onRequest, onRelease, onDestroy);

        public DefaultPoolConfiguration(int capacity,
            Func<T> createFunc,
            Action<T> onRequest,
            Action<T> onRelease,
            Action<T> onDestroy) :
            base(CreateObjectFactory(createFunc, onRequest, onRelease, onDestroy), STRATEGY, capacity) =>
            Guard = new SystemPoolGuard();

        protected override PoolGuard Guard { get; }
    }
}