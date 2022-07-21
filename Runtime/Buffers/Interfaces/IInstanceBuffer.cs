using System;
using System.Collections.Generic;

namespace Depra.ObjectPooling.Runtime.Buffers.Interfaces
{
    public interface IInstanceBuffer<T> : IDisposable
    {
        int FreeCount { get; }
        int UsedCount { get; }

        void AddFree(T value);

        bool TryRemoveFree(out T free);

        void AddInUse(T value);

        bool TryRemoveUsed(out T used);

        IReadOnlyCollection<T> GetAllFree();

        IReadOnlyCollection<T> GetAllInUse();

        void Clear();
    }
}