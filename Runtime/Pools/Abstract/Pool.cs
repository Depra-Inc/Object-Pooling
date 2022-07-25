using System;
using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Runtime.Pooled.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Interfaces;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.Pools.Abstract
{
    public abstract class Pool<T> : PoolBase, IPool<T> where T : IPooled
    {
        public abstract int CountActive { get; }
        
        public abstract int CountInactive { get;}

        public override int CountAll => CountActive + CountInactive;

        public Type BaseType { get; }
        
        public abstract T Request();
        
        public abstract void Release(T item);
        
        public override IPooled RequestPooled() => Request();
        
        public override void ReleasePooled(IPooled pooled)
        {
            if (pooled is not T objAsT)
            {
                Debug.Log("Error trying to free a pool object " + pooled + " of type " + pooled.GetType());
                return;
            }

            Release(objAsT);
        }
        
        protected Pool(object key) : base(key)
        {
            BaseType = typeof(T);
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
    }
}