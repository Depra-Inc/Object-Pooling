using System;
using Depra.ObjectPooling.Runtime.Pooled.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Interfaces;

namespace Depra.ObjectPooling.Runtime.Pools.Abstract
{
    /// <summary>
    /// Provides basic features of pool.
    /// </summary>
    public abstract class PoolBase : IPool, IDisposable
    {
        private bool _disposed;
        
        public object Key { get; }

        public abstract IPooled RequestPooled();

        public abstract void ReleasePooled(IPooled pooled);

        public abstract int CountAll { get; }

        public abstract void Clear();

        protected PoolBase(object key)
        {
            Key = key;
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