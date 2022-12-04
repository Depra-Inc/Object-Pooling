// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Domain;
using Depra.ObjectPooling.Domain.Entities;
using Depra.ObjectPooling.Domain.Structs;

namespace Depra.ObjectPooling.Application.Internal.Helpers
{
    public static class InstanceHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PooledInstance<T> ToInstance<T>(this T @object, IPoolHandle<T> pool) where T : IPooled
        {
            var instance = new PooledInstance<T>(pool, @object);
            return instance;
        }
    }
}