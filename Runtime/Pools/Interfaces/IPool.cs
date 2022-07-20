using JetBrains.Annotations;

namespace Depra.ObjectPooling.Runtime.Pools.Interfaces
{
    [PublicAPI]
    public interface IPool
    {
        int CountInactive { get; }
        
        /// <summary>
        /// Returns an object from the pool.
        /// </summary>
        IPooled RequestObject();
        
        /// <summary>
        /// Releases an object back to the pool.
        /// </summary>
        void FreeObject(IPooled obj);
    }
}