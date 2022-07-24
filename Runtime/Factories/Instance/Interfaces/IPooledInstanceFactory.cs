using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Structs;

namespace Depra.ObjectPooling.Runtime.Factories.Instance.Interfaces
{
    public interface IPooledInstanceFactory<T> where T : IPooled
    {
        PooledInstance<T> MakeActiveInstance(out bool reuse);

        PooledInstance<T> MakePassiveInstance(T obj);
        
        PooledInstance<T> MakePassiveInstance();

        void DestroyInstance(ref PooledInstance<T> instance);

        void ActivateInstance(ref PooledInstance<T> instance);

        void PassivateInstance(ref PooledInstance<T> instance);
    }
}