// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Depra.ObjectPooling.Application.Guard
{
    public class SystemPoolGuard : PoolGuard
    {
        public override void HandleException(Exception exception) => throw exception;
    }
}