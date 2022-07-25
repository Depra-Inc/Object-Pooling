using System.Collections;
using System.Linq;
using Depra.ObjectPooling.Runtime.Configuration.Impl;
using Depra.ObjectPooling.Runtime.Extensions;
using Depra.ObjectPooling.Runtime.Pooled.UnityObjects.Impl;
using Depra.ObjectPooling.Runtime.Pools.Objects;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace Depra.ObjectPooling.Tests.PlayMode
{
    public class PrefabPoolTests
    {
        private const string PrefabPath = "TestPooled";
        private const string Key = "TestPrefabPool";
        private const int DefaultCapacity = 10;

        private PooledGameObject _prefab;
        private UnityObjectPool<PooledGameObject> _unityObjectPool;

        [SetUp]
        public void SetUp()
        {
            _prefab = LoadPrefab();
            var configuration = new PrefabPoolConfiguration<PooledGameObject>(_prefab, DefaultCapacity);
            _unityObjectPool = new UnityObjectPool<PooledGameObject>(Key, configuration);
        }

        [TearDown]
        public void TearDown()
        {
            _unityObjectPool.Dispose();
            Resources.UnloadUnusedAssets();
        }

        [UnityTest]
        public IEnumerator Request_Object()
        {
            var obj = _unityObjectPool.Request();

            yield return null;

            Assert.IsNotNull(obj);
            Assert.AreEqual(1, _unityObjectPool.CountActive);
        }

        [UnityTest]
        public IEnumerator Free_Object()
        {
            const int requestedCount = 5;

            var objects = _unityObjectPool.RequestRange(requestedCount);
            var randomIndex = Random.Range(0, requestedCount);
            var randomObject = objects.ElementAt(randomIndex);

            yield return null;

            _unityObjectPool.Release((PooledGameObject)randomObject);

            yield return null;

            Assert.AreEqual(requestedCount, _unityObjectPool.CountAll);
            Assert.AreEqual(1, _unityObjectPool.CountInactive);
            Assert.AreEqual(requestedCount - 1, _unityObjectPool.CountActive);
        }

        private static PooledGameObject LoadPrefab() => Resources.Load<PooledGameObject>(PrefabPath);

        private PooledGameObject[] CreateCollectionOfPooledObjects(int count)
        {
            var objectCollection = new PooledGameObject[count];
            for (var i = 0; i < objectCollection.Length; i++)
            {
                objectCollection[i] = Object.Instantiate(_prefab);
            }

            return objectCollection;
        }
    }
}