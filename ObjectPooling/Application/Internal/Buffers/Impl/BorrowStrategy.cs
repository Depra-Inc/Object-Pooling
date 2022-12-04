// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Depra.ObjectPooling.Application.Internal.Buffers.Impl
{
    public enum BorrowStrategy
    {
        /// <summary>
        /// Last in first out (stack).
        /// </summary>
        LIFO,

        /// <summary>
        /// First in first out (queue).
        /// </summary>
        FIFO,

        /// <summary>
        /// Random out.
        /// </summary>
        Random
    }
}