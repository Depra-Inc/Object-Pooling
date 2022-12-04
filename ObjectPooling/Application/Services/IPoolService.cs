// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.ObjectPooling.Domain.Entities;

namespace Depra.ObjectPooling.Application.Services
{
    public interface IPoolService
    {
        void RegisterPool(object poolKey, IPool pool);

        void UnregisterPool(object poolKey);
    }
}