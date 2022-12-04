// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Depra.ObjectPooling.Domain.Exceptions
{
    public sealed class NotEnoughActiveObjectsInPoolException : Exception
    {
        private const string MESSAGE_FORMAT = "Error trying to passivate a object {0} from pool {1}";

        public NotEnoughActiveObjectsInPoolException(object poolKey, Type objectType) :
            base(string.Format(MESSAGE_FORMAT, objectType, poolKey)) { }
    }
}