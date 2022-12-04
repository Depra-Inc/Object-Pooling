// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using Depra.ObjectPooling.Application.Internal.Exceptions;
using Depra.ObjectPooling.Domain.Entities;

namespace Depra.ObjectPooling.Application.Services
{
    public sealed class PoolService : IPoolService
    {
        private readonly IDictionary<object, IPool> _poolsByKey;

        public PoolService()
        {
            _poolsByKey = new Dictionary<object, IPool>();
        }

        public void RegisterPool(object poolKey, IPool pool)
        {
            if (_poolsByKey.ContainsKey(poolKey))
            {
                throw new PoolAlreadyRegisteredException(poolKey);
            }

            _poolsByKey.Add(poolKey, pool);
        }

        public void UnregisterPool(object poolKey)
        {
            if (_poolsByKey.ContainsKey(poolKey) == false)
            {
                throw new PoolNotBeRegisteredException(poolKey);
            }

            _poolsByKey.Remove(poolKey);
        }
    }
}