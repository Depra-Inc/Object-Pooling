using System;
using Depra.ObjectPooling.Runtime.Factories.Obj.Impl;
using Depra.ObjectPooling.Runtime.Factories.Obj.Interfaces;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;

namespace Depra.ObjectPooling.Runtime.Configuration.Impl
{
    public class DefaultPoolConfiguration<T> : PoolConfiguration<T> where T : IPooled
    {
        private readonly Func<T> _createFunc;
        private readonly Action<T> _onRequest;
        private readonly Action<T> _onRelease;
        private readonly Action<T> _onDestroy;

        public DefaultPoolConfiguration(int capacity, 
            Func<T> createFunc, 
            Action<T> onRequest, 
            Action<T> onRelease,
            Action<T> onDestroy) : base(CreateObjectFactory(createFunc, onRequest, onRelease, onDestroy), capacity)
        {
        }

        private static IPooledObjectFactory<T> CreateObjectFactory(Func<T> createFunc,
            Action<T> onRequest,
            Action<T> onRelease, 
            Action<T> onDestroy)
        {
            return new CustomPooledObjectFactory<T>(createFunc, onRequest, onRelease, onDestroy);
        }
    }
}