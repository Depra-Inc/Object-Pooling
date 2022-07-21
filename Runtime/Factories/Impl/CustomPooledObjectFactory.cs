using System;
using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Runtime.Factories.Abstract;

namespace Depra.ObjectPooling.Runtime.Factories.Impl
{
    public class CustomPooledObjectFactory<T> : PooledObjectFactory<T>
    {
        private readonly Func<T> _createFunc;
        private readonly Action<T> _onRequest;
        private readonly Action<T> _onRelease;
        private readonly Action<T> _onDestroy;

        public override T CreateObject(object key) =>
            _createFunc() ?? throw new Exception("Factory must return not null!");

        public override void DestroyObject(object key, T instance) => _onDestroy?.Invoke(instance);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void OnEnableObject(object key, T instance) => _onRequest?.Invoke(instance);

        public override void OnDisableObject(object key, T instance) => _onRelease?.Invoke(instance);

        public CustomPooledObjectFactory(Func<T> createFunc, Action<T> onRequest, Action<T> onRelease,
            Action<T> onDestroy)
        {
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            _onRequest = onRequest;
            _onRelease = onRelease;
            _onDestroy = onDestroy;
        }
    }
}