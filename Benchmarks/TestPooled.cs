// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Domain.Entities;

namespace Depra.ObjectPooling.Benchmarks
{
    internal class TestPooled : IPooled
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPoolReuse() { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPoolCreate(IPool pool) { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPoolGet() { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPoolSleep() { }
    }
}