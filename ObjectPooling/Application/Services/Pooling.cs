// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Depra.ObjectPooling.Application.Services
{
    public static class Pooling
    {
        private static IPoolService _poolService;

        static Pooling()
        {
            _poolService = new PoolService();
        }
        
        public static T RequestObject<T>()
        {
            return default;
        }
    }
}