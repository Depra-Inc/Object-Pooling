// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Depra.ObjectPooling.Domain.Entities;
using Depra.ObjectPooling.Domain.Structs;

namespace Depra.ObjectPooling.Application.Internal.Buffers.Interfaces
{
    public interface IInstanceBuffer<T> where T : IPooled
    {
        int Count { get; }
        
        bool Contains(ref PooledInstance<T> instance);
        
        void AddInstance(ref PooledInstance<T> instance);

        PooledInstance<T> GetInstance();

        IEnumerable<PooledInstance<T>> GetAll();

        void Clear();
    }
}