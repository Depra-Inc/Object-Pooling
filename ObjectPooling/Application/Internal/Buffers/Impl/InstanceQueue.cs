// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Application.Internal.Buffers.Interfaces;
using Depra.ObjectPooling.Domain.Entities;
using Depra.ObjectPooling.Domain.Structs;

namespace Depra.ObjectPooling.Application.Internal.Buffers.Impl
{
    internal sealed class InstanceQueue<T> : IInstanceBuffer<T> where T : IPooled
    {
        private readonly Queue<PooledInstance<T>> _instances;

        public InstanceQueue(int capacity) => _instances = new Queue<PooledInstance<T>>(capacity);

        public int Count => _instances.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(ref PooledInstance<T> instance) => _instances.Contains(instance);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddInstance(ref PooledInstance<T> instance) => _instances.Enqueue(instance);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PooledInstance<T> GetInstance() => _instances.Dequeue();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<PooledInstance<T>> GetAll() => _instances;

        public void Clear() => _instances.Clear();
    }
}