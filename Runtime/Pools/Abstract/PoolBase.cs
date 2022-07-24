using System;
using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Interfaces;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.Pools.Abstract
{
    /// <summary>
    /// Provides basic features of pool.
    /// </summary>
    public abstract class PoolBase<T> : IPool, IDisposable where T : IPooled
    {
        private bool _disposed;
        
        public object Key { get; }

        public Type BaseType { get; }

        public abstract int CountActive { get; }
        
        public abstract int CountInactive { get;}

        public int CountAll => CountActive + CountInactive;
        
        public abstract T RequestObject();

        public abstract void ReleaseObject(T obj);

        public abstract void Clear();

        IPooled IPool.RequestObject() => RequestObject();

        void IPool.FreeObject(IPooled obj)
        {
            if (obj is not T objAsT)
            {
                Debug.Log("Error trying to free a pool object " + obj + " of type " + obj.GetType());
                return;
            }

            ReleaseObject(objAsT);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnObjectRequested(T obj)
        {
            obj.OnPoolGet();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnObjectReleased(T obj)
        {
            obj.OnPoolSleep();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnObjectReused(T obj)
        {
            obj.OnPoolReuse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnObjectCreated(T obj)
        {
            obj.OnPoolCreate(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnFreeObjectAdded(T obj)
        {
            obj.OnPoolSleep();
        }

        protected PoolBase(object key)
        {
            Key = key;
            BaseType = typeof(T);
        }

        ~PoolBase() => Dispose(false);

        public void Dispose() => Dispose(true);
        
        private void Dispose(bool disposing)
        {
            if (disposing == false || _disposed)
            {
                return;
            }

            Clear();
            _disposed = true;
        }
    }
}