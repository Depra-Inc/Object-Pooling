// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Depra.ObjectPooling.Domain.Entities
{
    /// <summary>
    /// Classes that implement <see cref="IPooled"/> will receive calls from the <see cref="IPool"/>.
    /// </summary>
    public interface IPooled
    {
        /// <summary>
        /// Invoked when the object is instantiated.
        /// </summary>
        void OnPoolCreate(IPool pool);

        /// <summary>
        /// Invoked when the object is grabbed from the pool.
        /// </summary>s
        void OnPoolGet();

        /// <summary>
        /// Invoked when the object is released back to the pool.
        /// </summary>
        void OnPoolSleep();
        
        /// <summary>
        /// Invoked when the object is reused.
        /// </summary>
        void OnPoolReuse();
    }
}