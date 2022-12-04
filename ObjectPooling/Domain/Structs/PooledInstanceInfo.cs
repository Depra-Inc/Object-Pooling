// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Runtime.CompilerServices;

namespace Depra.ObjectPooling.Domain.Structs
{
    [Serializable]
    public struct PooledInstanceInfo : IEquatable<PooledInstanceInfo>
    {
        internal PooledInstanceInfo(int id)
        {
            Id = id;
            State = PooledInstanceState.Available;
            ActiveTime = 0;
        }

        public int Id { get; }

        public PooledInstanceState State { get; internal set; }

        public float ActiveTime { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnActivate()
        {
            State = PooledInstanceState.Available;
            //ActiveTime = Time.realtimeSinceStartup;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnDeactivate()
        {
            State = PooledInstanceState.Unavailable;
            ActiveTime = 0f;
        }

        public bool Equals(PooledInstanceInfo other) => Id == other.Id;

        public override bool Equals(object obj) => obj is PooledInstanceInfo other && Equals(other);

        public override int GetHashCode() => Id;
    }
}