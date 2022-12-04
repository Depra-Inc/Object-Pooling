// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.ObjectPooling.Application.Factories.Instance;
using Depra.ObjectPooling.Application.Pools.Abstract;
using Depra.ObjectPooling.Application.Pools.Interfaces;
using Depra.ObjectPooling.Domain.Entities;

namespace Depra.ObjectPooling.Application.Configuration.Interfaces
{
    public interface IPoolConfiguration<T> where T : IPooled
    {
        IPoolContext<T> GetContext(Pool<T> pool);

        IPooledInstanceFactory<T> GetFactory(IPoolContext<T> context);
    }
}