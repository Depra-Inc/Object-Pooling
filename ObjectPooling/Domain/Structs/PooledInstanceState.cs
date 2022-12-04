// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Depra.ObjectPooling.Domain.Structs
{
    /// <summary>
    /// Describes the state of a pooled object.
    /// </summary>
    public enum PooledInstanceState
    {
        /// <summary>
        /// The object is inside the pool, waiting to be used.
        /// </summary>
        Available = 0,

        /// <summary>
        /// The object is outside the pool, waiting to return to the pool.
        /// </summary>
        Unavailable = 1,

        /// <summary>
        /// The object has been disposed and cannot be used anymore.
        /// </summary>
        Disposed = 2
    }
}