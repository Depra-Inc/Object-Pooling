// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using Depra.ObjectPooling.Application.Internal.Buffers.Interfaces;
using Depra.ObjectPooling.Domain.Entities;
using Depra.ObjectPooling.Domain.Structs;

namespace Depra.ObjectPooling.Application.Internal.Buffers.Impl
{
    internal sealed class InstanceBag<T> : IInstanceBuffer<T> where T : IPooled
    {
        private readonly Random _random;
        private readonly List<PooledInstance<T>> _instances;

        public InstanceBag(int capacity)
        {
            _random = new Random();
            _instances = new List<PooledInstance<T>>(capacity);
        }
        
        public int Count => _instances.Count;

        public bool Contains(ref PooledInstance<T> instance) => _instances.Contains(instance);

        public void AddInstance(ref PooledInstance<T> instance) => _instances.Add(instance);
        
        public PooledInstance<T> GetInstance()
        {
            var randomIndex = GetRandomIndex();
            var randomInstance = _instances[randomIndex];
            _instances.RemoveAt(randomIndex);

            return randomInstance;
        }

        public IEnumerable<PooledInstance<T>> GetAll() => _instances;

        public void Clear() => _instances.Clear();
        
        private int GetRandomIndex()
        {
            const int MIN_INDEX = 0;
            var maxIndex = _instances.Count;
            var randomIndex = _random.Next(MIN_INDEX, maxIndex);

            return randomIndex;
        }
    }
}