// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Depra.ObjectPooling.Application.Guard
{
    public abstract class PoolGuard
    {
        public abstract void HandleException(Exception exception);
    }
}