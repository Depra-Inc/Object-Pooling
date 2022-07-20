using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Runtime.Pools.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Structs;

namespace Depra.ObjectPooling.Runtime.Helpers
{
    public static class InstanceHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PooledInstance<T> ToInstance<T>(this T @object, IPool pool) where T : IPooled
        {
            var instance = new PooledInstance<T>(pool, @object);
            return instance;
        }
    }
}