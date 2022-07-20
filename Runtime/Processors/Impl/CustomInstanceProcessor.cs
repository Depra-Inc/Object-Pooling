using System;
using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Runtime.Processors.Abstract;

namespace Depra.ObjectPooling.Runtime.Processors.Impl
{
    public class CustomInstanceProcessor<T> : InstanceProcessor<T>
    {
        private readonly Func<T> _createFunc;
        private readonly Action<T> _onRequest;
        private readonly Action<T> _onRelease;
        private readonly Action<T> _onDestroy;

        public override T CreateInstance(object key) => _createFunc.Invoke();

        public override void DestroyInstance(object key, T instance) => _onDestroy?.Invoke(instance);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void OnEnableInstance(object key, T instance) => _onRequest?.Invoke(instance);

        public override void OnDisableInstance(object key, T instance) => _onRelease?.Invoke(instance);

        public CustomInstanceProcessor(Func<T> createFunc, Action<T> onRequest, Action<T> onRelease,
            Action<T> onDestroy)
        {
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            _onRequest = onRequest;
            _onRelease = onRelease;
            _onDestroy = onDestroy;
        }
    }
}