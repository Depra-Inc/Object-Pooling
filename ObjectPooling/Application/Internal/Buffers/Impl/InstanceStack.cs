// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Application.Internal.Buffers.Interfaces;
using Depra.ObjectPooling.Domain.Entities;
using Depra.ObjectPooling.Domain.Structs;

namespace Depra.ObjectPooling.Application.Internal.Buffers.Impl
{
    internal sealed class InstanceStack<T> : IInstanceBuffer<T> where T : IPooled
    {
        private readonly Stack<PooledInstance<T>> _instances;

        public InstanceStack(int capacity) => _instances = new Stack<PooledInstance<T>>(capacity);

        public int Count => _instances.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(ref PooledInstance<T> instance) => _instances.Contains(instance);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddInstance(ref PooledInstance<T> instance) => _instances.Push(instance);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PooledInstance<T> GetInstance() => _instances.Pop();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<PooledInstance<T>> GetAll() => _instances;

        public void Clear() => _instances.Clear();
    }
}