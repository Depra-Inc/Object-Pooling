// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Depra.ObjectPooling.Domain.Entities
{
    public interface IPool<T>
    {
        int CountAll { get; }

        /// <summary>
        /// Returns an object from the pool.
        /// </summary>
        T Request();

        /// <summary>
        /// Releases an object back to the pool.
        /// </summary>
        /// <param name="obj">Object to release.</param>
        void Release(T obj);
    }
}