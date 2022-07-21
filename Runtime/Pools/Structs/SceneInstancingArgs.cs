using UnityEngine;

namespace Depra.ObjectPooling.Runtime.Pools.Structs
{
    public readonly struct SceneInstancingArgs
    {
        public Vector3 Position { get; }

        public Quaternion Rotation { get; }

        public Transform Parent { get; }

        public SceneInstancingArgs(Vector3 position, Quaternion rotation, Transform parent)
        {
            Position = position;
            Rotation = rotation;
            Parent = parent;
        }
    }
}