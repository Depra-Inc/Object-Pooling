// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.ObjectPooling.Domain.Structs;

namespace Depra.ObjectPooling.Domain.Entities
{
    /// <summary>
    /// Exposes a way to return objects to the pool.
    /// </summary>
    public interface IPoolHandle<T> where T : IPooled
    {
        void ReturnInstanceToPool(PooledInstance<T> instance, bool reRegisterForFinalization);
    }
}