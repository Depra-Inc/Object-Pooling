// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Reflection;

namespace Depra.ObjectPooling.Domain.Exceptions
{
    public class PoolObjectTypeMismatchException : Exception
    {
        private const string MESSAGE_FORMAT = "Error trying to free a pool object {0} of type {1}";

        public PoolObjectTypeMismatchException(MemberInfo factType, MemberInfo expectedType) :
            base(string.Format(MESSAGE_FORMAT, factType.Name, expectedType.Name)) { }
    }
}