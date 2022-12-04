// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Depra.ObjectPooling.Application.Internal.Exceptions
{
    internal sealed class PoolAlreadyRegisteredException : Exception
    {
        private const string MESSAGE_FORMAT = "Pool with key {0} already registered!";

        public PoolAlreadyRegisteredException(object poolKey) : base(string.Format(MESSAGE_FORMAT, poolKey)) { }
    }
}