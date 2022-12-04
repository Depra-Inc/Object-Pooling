// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Depra.ObjectPooling.Domain.Exceptions
{
    public sealed class NotEnoughFreeObjectsInPoolException : Exception
    {
        private const string MESSAGE_FORMAT = "Error trying to free a object {0} from pool {1}";

        public NotEnoughFreeObjectsInPoolException(object poolKey, Type objectType) :
            base(string.Format(MESSAGE_FORMAT, objectType, poolKey)) { }
    }
}