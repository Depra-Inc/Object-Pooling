// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Depra.ObjectPooling.Application.Internal.Exceptions
{
    internal sealed class PoolNotBeRegisteredException : Exception
    {
        private const string MESSAGE_FORMAT = "Pool with key {0} is not be registred!";

        public PoolNotBeRegisteredException(object poolKey) : base(string.Format(MESSAGE_FORMAT, poolKey)) { }
    }
}