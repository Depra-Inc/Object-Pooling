using System.Collections.Generic;
using Depra.ObjectPooling.Runtime.Factories.Abstract;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.Factories.Impl
{
    public class PrefabPooledObjectFactory<T> : PooledObjectFactory<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly IDictionary<object, Transform> _containers;

        public override T CreateObject(object key) =>
            CreateObject(key, Vector3.zero, Quaternion.identity, null);

        public override void DestroyObject(object key, T instance)
        {
            DestroyGameObject(instance);

            if (_containers.TryGetValue(key, out var container) == false)
            {
                return;
            }

            if (container.childCount != 0)
            {
                return;
            }

            DestroyGameObject(container.gameObject);
            _containers.Remove(key);
        }

        public override void OnDisableObject(object key, T instance)
        {
            if (_containers.TryGetValue(key, out var container))
            {
                instance.transform.SetParent(container);
            }

            instance.gameObject.SetActive(false);
        }

        public T CreateObject(object key, Vector3 position, Quaternion rotation, Transform parent)
        {
            T instance;

            if (_containers.TryGetValue(key, out _) == false)
            {
                var container = new GameObject($"Pool - {key}");
                instance = container.AddComponent<T>();
                AddContainer(key, instance);
            }

            instance = Object.Instantiate(_prefab, position, rotation, parent);
            instance.name = $"{_prefab.name}(Clone)";

            OnDisableObject(key, instance);

            return instance;
        }

        public PrefabPooledObjectFactory(T prefab)
        {
            _prefab = prefab;
            _containers = new Dictionary<object, Transform>();
        }

        private static void DestroyGameObject(Object gameObject)
        {
#if UNITY_EDITOR
            Object.DestroyImmediate(gameObject);
#else
            Object.Destroy(gameObject);
#endif
        }

        private void AddContainer(object key, T instance)
        {
            var container = instance.transform;
            _containers.Add(key, container);
        }
    }
}