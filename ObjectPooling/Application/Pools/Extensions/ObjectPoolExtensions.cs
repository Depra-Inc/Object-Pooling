// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Depra.ObjectPooling.Application.Pools.Impl;
using Depra.ObjectPooling.Domain.Entities;

namespace Depra.ObjectPooling.Application.Pools.Extensions
{
    public static class ObjectPoolExtensions
    {
        public static void AddFreeRange<T>(this ObjectPool<T> pool, IEnumerable<T> collection) where T : IPooled
        {
            foreach (var item in collection)
            {
                pool.AddInactive(item);
            }
        }
    }
}