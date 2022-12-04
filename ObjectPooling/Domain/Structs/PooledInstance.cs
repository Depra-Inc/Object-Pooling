// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Domain.Entities;

namespace Depra.ObjectPooling.Domain.Structs
{
    public readonly struct PooledInstance<T> : IDisposable where T : IPooled
    {
        private readonly IPoolHandle<T> _poolHandle;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal PooledInstance(IPoolHandle<T> pool, T obj) : this()
        {
            Obj = obj;
            _poolHandle = pool;
            Info = new PooledInstanceInfo(obj.GetHashCode());

            Deactivate();
        }

        public T Obj { get; }

        public PooledInstanceInfo Info { get; }

        internal void Activate() => Info.OnActivate();

        internal void Deactivate() => Info.OnDeactivate();

        public void Dispose() => _poolHandle.ReturnInstanceToPool(this, true);
    }
}