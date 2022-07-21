using System.Runtime.CompilerServices;
using Depra.ObjectPooling.Runtime.Buffers.Interfaces;
using Depra.ObjectPooling.Runtime.Exceptions;
using Depra.ObjectPooling.Runtime.Factories.Impl;
using Depra.ObjectPooling.Runtime.Helpers;
using Depra.ObjectPooling.Runtime.PooledObjects.Interfaces;
using Depra.ObjectPooling.Runtime.Pools.Structs;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.Pools.Impl
{
    public class PrefabPool<T> : ObjectPool<T> where T : MonoBehaviour, IPooled
    {
        private readonly PrefabPooledObjectFactory<T> _pooledObjectFactory;

        public T RequestObject(Vector3 position, Quaternion rotation, Transform parent) =>
            RequestObject(new SceneInstancingArgs(position, rotation, parent));

        public T RequestObject(SceneInstancingArgs args)
        {
            T obj;
            if (CountInactive > 0)
            {
                obj = ReuseObject();
                PrepareObjectTransform(obj.transform, args);
            }
            else
            {
                obj = CreateObject(args);
                AddFreeObject(obj);
            }

            return obj;
        }

        public PrefabPool(object key, IInstanceBuffer<PooledInstance<T>> buffer,
            PrefabPooledObjectFactory<T> pooledObjectFactory, ExceptionHandlingRule exceptionHandlingRule) : base(key,
            buffer, pooledObjectFactory, exceptionHandlingRule)
        {
            _pooledObjectFactory = pooledObjectFactory;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T CreateObject(SceneInstancingArgs args)
        {
            var obj = _pooledObjectFactory.CreateObject(Key, args.Position, args.Rotation, args.Parent);
            OnObjectCreated(obj);
            UseInstance(obj.ToInstance(this));
            
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