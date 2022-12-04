// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Depra.ObjectPooling.Domain.Entities;

namespace Depra.ObjectPooling.Application.Pools.Abstract
{
    /// <summary>
    /// Provides basic features of pool.
    /// </summary>
    public abstract class PoolBase : IPool, IDisposable
    {
        private bool _disposed;

        protected PoolBase(object key) => Key = key;

        public object Key { get; }

        public abstract IPooled RequestPooled();

        public abstract void ReleasePooled(IPooled pooled);

        public abstract int CountAll { get; }

        public abstract void Clear();

        ~PoolBase() => Dispose(false);

        public void Dispose() => Dispose(true);

        private void Dispose(bool disposing)
        {
            if (disposing == false || _disposed)
            {
                return;
            }

            Clear();
            _disposed = true;
        }
    }
}