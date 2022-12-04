// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Domain.Entities;
using Depra.ObjectPooling.Domain.Exceptions;
using Depra.ObjectPooling.Domain.Structs;

namespace Depra.ObjectPooling.Application.Pools.Abstract
{
    public abstract class Pool<T> : PoolBase, IPool<T>, IPoolHandle<T> where T : IPooled
    {
        protected Pool(object key) : base(key) => BaseType = typeof(T);
        
        public abstract int CountActive { get; }
        public abstract int CountInactive { get; }
        public override int CountAll => CountActive + CountInactive;

        public Type BaseType { get; }

        public abstract T Request();

        public abstract void Release(T item);
        
        public void ReturnInstanceToPool(PooledInstance<T> instance, bool reRegisterForFinalization) =>
            Release(instance.Obj);

        public override IPooled RequestPooled() => Request();
        
        public override void ReleasePooled(IPooled pooled)
        {
            if (!(pooled is T objAsT))
            {
                HandleException(new PoolObjectTypeMismatchException(pooled.GetType(), typeof(T)));
                return;
            }

            Release(objAsT);
        }

        protected abstract void HandleException(Exception exception);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnObjectRequested(T obj) => obj.OnPoolGet();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnObjectReleased(T obj) => obj.OnPoolSleep();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnObjectReused(T obj) => obj.OnPoolReuse();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnObjectCreated(T obj) => obj.OnPoolCreate(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnFreeObjectAdded(T obj) => obj.OnPoolSleep();
    }
}