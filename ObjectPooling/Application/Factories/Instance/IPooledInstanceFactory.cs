// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.ObjectPooling.Domain.Entities;
using Depra.ObjectPooling.Domain.Structs;

namespace Depra.ObjectPooling.Application.Factories.Instance
{
    public interface IPooledInstanceFactory<T> where T : IPooled
    {
        PooledInstance<T> MakeActiveInstance(out bool reuse);

        PooledInstance<T> MakePassiveInstance(T obj);
        
        PooledInstance<T> MakePassiveInstance();

        void DestroyInstance(ref PooledInstance<T> instance);
    }
}