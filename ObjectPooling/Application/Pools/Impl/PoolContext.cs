// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Depra.ObjectPooling.Application.Guard;
using Depra.ObjectPooling.Application.Internal.Buffers.Impl;
using Depra.ObjectPooling.Application.Internal.Buffers.Interfaces;
using Depra.ObjectPooling.Application.Pools.Abstract;
using Depra.ObjectPooling.Application.Pools.Interfaces;
using Depra.ObjectPooling.Domain.Entities;
using Depra.ObjectPooling.Domain.Structs;

namespace Depra.ObjectPooling.Application.Pools.Impl
{
    public sealed class PoolContext<T> : IPoolContext<T> where T : IPooled
    {
        private readonly PoolGuard _guard;

        public PoolContext(Pool<T> pool, PoolGuard guard, BorrowStrategy borrowStrategy, int capacity)
        {
            Pool = pool;
            _guard = guard;
            ActiveInstances = CreateBuffer(borrowStrategy, capacity);
            PassiveInstances = CreateBuffer(borrowStrategy, capacity);
        }

        public Pool<T> Pool { get; }

        public IInstanceBuffer<T> ActiveInstances { get; }
        public IInstanceBuffer<T> PassiveInstances { get; }

        public void Clear(Action<PooledInstance<T>> onClear)
        {
            ClearCollection(ActiveInstances, onClear);
            ClearCollection(PassiveInstances, onClear);
        }

        public void HandleException(Exception exception) => _guard.HandleException(exception);

        private static IInstanceBuffer<T> CreateBuffer(BorrowStrategy borrowStrategy, int capacity)
        {
            return borrowStrategy switch
            {
                BorrowStrategy.LIFO => new InstanceStack<T>(capacity),
                BorrowStrategy.FIFO => new InstanceQueue<T>(capacity),
                BorrowStrategy.Random => new InstanceBag<T>(capacity),
                BorrowStrategy.Circular => new CircularInstanceBuffer<T>(),
                _ => throw new ArgumentOutOfRangeException(nameof(borrowStrategy), borrowStrategy, null)
            };
        }
        
        private static void ClearCollection(IInstanceBuffer<T> instances, Action<PooledInstance<T>> onClear)
        {
            var allInstances = instances.GetAll();
            foreach (var instance in allInstances)
            {
                onClear.Invoke(instance);
            }

            instances.Clear();
        }
    }
}