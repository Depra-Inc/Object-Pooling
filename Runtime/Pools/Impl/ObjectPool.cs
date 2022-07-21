using System;
using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Runtime.Buffers.Interfaces;
using Depra.ObjectPooling.Runtime.Exceptions;
using Depra.ObjectPooling.Runtime.Factories.Abstract;
using Depra.ObjectPooling.Runtime.Helpers;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Abstract;
using Depra.ObjectPooling.Runtime.Pools.Structs;

namespace Depra.ObjectPooling.Runtime.Pools.Impl
{
    public class ObjectPool<T> : PoolBase<T> where T : IPooled
    {
        private readonly PooledObjectFactory<T> _objectFactory;
        private readonly IInstanceBuffer<PooledInstance<T>> _buffer;
        private readonly ExceptionHandlingRule _exceptionHandlingRule;

        private bool _disposed;

        public override int CountAll { get; protected set; }
        public override int CountInactive => _buffer.FreeCount;

        public override T RequestObject()
        {
            var obj = CountInactive > 0 ? ReuseObject() : IncrementPool();
            OnObjectRequested(obj);

            return obj;
        }

        public override void FreeObject(T obj)
        {
            if (obj == null)
            {
                _exceptionHandlingRule.HandleException(new NullReferenceException());
                return;
            }
            
            OnObjectFree(obj);
            _objectFactory.OnDisableObject(Key, obj);

            if (_buffer.TryRemoveUsed(out var instance))
            {
                instance.SetActive(false);
                _buffer.AddFree(instance);
            }
            else
            {
                _exceptionHandlingRule.HandleException(new NotEnoughFreeObjectsPoolError(Key, obj.GetType()));
            }
        }

        public void Clear()
        {
            var allInstancesInUse = _buffer.GetAllInUse();
            foreach (var instance in allInstancesInUse)
            {
                _objectFactory.DestroyObject(Key, instance.Obj);
            }

            _buffer.Clear();
            CountAll = 0;
        }

        public void AddFreeObject(T obj)
        {
            var instance = obj.ToInstance(this);
            _buffer.AddFree(instance);

            OnFreeObjectAdded(obj);
        }

        public ObjectPool(object key, IInstanceBuffer<PooledInstance<T>> buffer, PooledObjectFactory<T> objectFactory,
            ExceptionHandlingRule exceptionHandlingRule) : base(key)
        {
            _buffer = buffer;
            _objectFactory = objectFactory;
            _exceptionHandlingRule = exceptionHandlingRule;
        }

        protected T ReuseObject()
        {
            if (_buffer.TryRemoveFree(out var instance))
            {
                return UseInstance(instance);
            }
            
            _exceptionHandlingRule.HandleException(new NullReferenceException());
            return default;
        }

        protected T UseInstance(PooledInstance<T> instance)
        {
            _buffer.AddInUse(instance);
            
            instance.SetActive(true);
            
            var obj = instance.Obj;
            _objectFactory.OnEnableObject(Key, obj);

            return obj;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing == false || _disposed)
            {
                return;
            }

            Clear();
            _disposed = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T IncrementPool()
        {
            var obj = _objectFactory.CreateObject(Key);
            OnObjectCreated(obj);
            UseInstance(obj.ToInstance(this));

            return obj;
        }
    }
}