using System;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Interfaces;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.Pools.Abstract
{
    public abstract class PoolBase<T> : IPool, IDisposable where T : IPooled
    {
        public object Key { get; }

        public Type BaseType { get; }

        public abstract int CountInactive { get; }

        public abstract int CountAll { get; protected set; }

        public int CountActive => CountAll - CountInactive;

        public abstract T RequestObject();

        public abstract void FreeObject(T obj);

        IPooled IPool.RequestObject() => RequestObject();

        void IPool.FreeObject(IPooled obj)
        {
            if (obj is not T objAsT)
            {
                Debug.Log("Error trying to free a pool object " + obj + " of type " + obj.GetType());
                return;
            }

            FreeObject(objAsT);
        }

        protected void OnObjectRequested(T obj)
        {
            obj.OnPoolGet();
        }

        protected void OnObjectFree(T obj)
        {
            obj.OnPoolSleep();
        }

        protected void OnObjectCreated(T obj)
        {
            CountAll++;
            obj.OnPoolCreate(this);
        }

        protected void OnFreeObjectAdded(T obj)
        {
            CountAll++;
            obj.OnPoolSleep();
        }

        protected PoolBase(object key)
        {
            Key = key;
            BaseType = typeof(T);
        }

        ~PoolBase() => Dispose(false);

        public void Dispose() => Dispose(true);

        protected abstract void Dispose(bool disposing);
    }
}