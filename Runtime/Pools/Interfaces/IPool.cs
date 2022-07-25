using Depra.ObjectPooling.Runtime.Pooled.Interfaces;
using JetBrains.Annotations;

namespace Depra.ObjectPooling.Runtime.Pools.Interfaces
{
    [PublicAPI]
    public interface IPool
    {
        int CountAll { get; }
        
        IPooled RequestPooled();
        
        void ReleasePooled(IPooled pooled);
    }
}