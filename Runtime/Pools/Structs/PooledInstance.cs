using System;
using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Runtime.Pools.Interfaces;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.Pools.Structs
{
    public struct PooledInstance<T> : IDisposable where T : IPooled
    {
        private readonly IPool _pool;
        
        public T Obj { get; }

        public bool ActiveSelf { get; private set; }

        public float ActiveTime { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PooledInstance(IPool pool, T obj) : this()
        {
            _pool = pool;
            Obj = obj;
            SetActive(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetActive(bool value)
        {
            ActiveSelf = value;
            ActiveTime = value ? Time.realtimeSinceStartup : 0.0f;
        }

        public void Dispose()
        {
            _pool.FreeObject(Obj);
        }
    }
}