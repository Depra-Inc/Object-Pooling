using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Runtime.Configuration.Impl;
using Depra.ObjectPooling.Runtime.Factories.Obj.Impl;
using Depra.ObjectPooling.Runtime.Pooled.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Structs;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.Pools.Objects
{
    public class UnityObjectPool<T> : ObjectPool<T> where T : MonoBehaviour, IPooled
    {
        private readonly PrefabPooledObjectFactory<T> _pooledObjectFactory;

        public T RequestObject(Vector3 position, Quaternion rotation, Transform parent) =>
            RequestObject(new SceneInstancingArgs(position, rotation, parent));

        public T RequestObject(SceneInstancingArgs args)
        {
            var obj = Request();
            PrepareObjectTransform(obj.transform, args);

            return obj;
        }

        public UnityObjectPool(object key, PrefabPoolConfiguration<T> configuration) : base(key, configuration)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T CreateObject(SceneInstancingArgs args)
        {
            var obj = _pooledObjectFactory.CreateObject(Key, args.Position, args.Rotation, args.Parent);
            OnObjectCreated(obj);
            
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PrepareObjectTransform(Transform transform, SceneInstancingArgs args)
        {
            transform.SetPositionAndRotation(args.Position, args.Rotation);
            if (args.Parent != transform.parent)
            {
                transform.SetParent(args.Parent);
            }
        }
    }
}