using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Runtime.Buffers.Interfaces;
using Depra.ObjectPooling.Runtime.Helpers;
using Depra.ObjectPooling.Runtime.Pools.Abstract;
using Depra.ObjectPooling.Runtime.Pools.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Structs;
using Depra.ObjectPooling.Runtime.Processors.Abstract;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.Pools.Impl
{
    public class ObjectPool<T> : PoolBase<T> where T : IPooled
    {
        private readonly InstanceProcessor<T> _processor;
        private readonly IInstanceBuffer<PooledInstance<T>> _buffer;

        private bool _disposed;

        public override int CountInactive => _buffer.FreeCount;

        public override T RequestObject()
        {
            var obj = _buffer.HasFree ? _buffer.RemoveFree().Obj : CreateInstance();
            obj.OnPoolGet();

            var instance = obj.ToInstance(this);
            _buffer.AddInUse(instance);
            
            _processor.OnEnableInstance(Key, obj);

            return obj;
        }

        public override void FreeObject(T obj)
        {
            if (_buffer.HasInUse == false)
            {
                Debug.LogWarning($"Error trying to free a pool object {obj}");
                return;
            }

            obj.OnPoolSleep();

            var instance = _buffer.RemoveInUse();
            _buffer.AddFree(instance);

            _processor.OnDisableInstance(Key, obj);
        }

        public void AddObject(T obj)
        {
            var instance = obj.ToInstance(this);
            _buffer.AddFree(instance);
            CountAll++;
            
            obj.OnPoolSleep();
        }

        public void WarmUp(int count)
        {
            var array = new T[count];
            for (var i = 0; i < count; i++)
            {
                array[i] = RequestObject();
            }

            foreach (var el in array)
            {
                FreeObject(el);
            }
        }

        public void Clear()
        {
            var allInstancesInUse = _buffer.GetAllInUse();
            foreach (var instance in allInstancesInUse)
            {
                _processor.DestroyInstance(Key, instance.Obj);
            }

            _buffer.Clear();
            CountAll = 0;
        }

        public ObjectPool(object key, IInstanceBuffer<PooledInstance<T>> buffer, InstanceProcessor<T> processor, int defaultCapacity) : base(key)
        {
            _buffer = buffer;
            _processor = processor;
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
        private T CreateInstance()
        {
            var instance = _processor.CreateInstance(Key);
            CountAll++;

            instance.OnPoolCreate(this);

            return instance;
        }
    }
}