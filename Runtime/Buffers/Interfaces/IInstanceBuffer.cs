using System;
using System.Collections.Generic;

namespace Depra.ObjectPooling.Runtime.Buffers.Interfaces
{
    public interface IInstanceBuffer<TSource> : IDisposable
    {
        int FreeCount { get; }
        int UsedCount { get; }
        
        bool HasFree { get; }
        bool HasInUse { get; }
        
        void AddFree(TSource obj);

        TSource RemoveFree();

        void AddInUse(TSource obj);

        TSource RemoveInUse();

        IReadOnlyCollection<TSource> GetAllFree();

        IReadOnlyCollection<TSource> GetAllInUse();

        void Clear();
    }
}