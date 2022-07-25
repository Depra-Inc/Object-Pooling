using JetBrains.Annotations;

namespace Depra.ObjectPooling.Runtime.Pools.Interfaces
{
    [PublicAPI]
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