// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Depra.ObjectPooling.Application.Internal.Buffers.Interfaces;
using Depra.ObjectPooling.Domain.Entities;
using Depra.ObjectPooling.Domain.Structs;

namespace Depra.ObjectPooling.Application.Internal.Buffers.Impl
{
    internal class CircularInstanceBuffer<T> : IInstanceBuffer<T> where T : IPooled
    {
        public int Count { get; }
        
        public bool Contains(ref PooledInstance<T> instance)
        {
            throw new System.NotImplementedException();
        }

        public void AddInstance(ref PooledInstance<T> instance)
        {
            throw new System.NotImplementedException();
        }

        public PooledInstance<T> GetInstance()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PooledInstance<T>> GetAll()
        {
            throw new System.NotImplementedException();
        }
        
        public void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}