// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Depra.ObjectPooling.Domain.Entities
{
    public interface IPool
    {
        int CountAll { get; }
        
        IPooled RequestPooled();
        
        void ReleasePooled(IPooled pooled);
    }
}