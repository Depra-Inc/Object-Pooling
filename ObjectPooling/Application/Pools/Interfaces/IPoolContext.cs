// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Depra.ObjectPooling.Application.Internal.Buffers.Interfaces;
using Depra.ObjectPooling.Application.Pools.Abstract;
using Depra.ObjectPooling.Domain.Entities;
using Depra.ObjectPooling.Domain.Structs;

namespace Depra.ObjectPooling.Application.Pools.Interfaces
{
    public interface IPoolContext<T> where T : IPooled
    {
        Pool<T> Pool { get; }

        IInstanceBuffer<T> ActiveInstances { get; }
        
        IInstanceBuffer<T> PassiveInstances { get; }

        void Clear(Action<PooledInstance<T>> onClear);

        void HandleException(Exception exception);
    }
}