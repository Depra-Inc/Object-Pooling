using System.Collections;
using System.Linq;
using Depra.ObjectPooling.Runtime.Configuration.Impl;
using Depra.ObjectPooling.Runtime.Extensions;
using Depra.ObjectPooling.Runtime.PooledObjects.Impl;
using Depra.ObjectPooling.Runtime.Pools.Impl;
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
        private PrefabPool<PooledGameObject> _prefabPool;

        [SetUp]
        public void SetUp()
        {
            _prefab = LoadPrefab();
            var configuration = new PrefabPoolConfiguration<PooledGameObject>(_prefab, DefaultCapacity);
            _prefabPool = new PrefabPool<PooledGameObject>(Key, configuration);
        }

        [TearDown]
        public void TearDown()
        {
            _prefabPool.Dispose();
            Resources.UnloadUnusedAssets();
        }

        [UnityTest]
        public IEnumerator Request_Object()
        {
            var obj = _prefabPool.RequestObject();

            yield return null;

            Assert.IsNotNull(obj);
            Assert.AreEqual(1, _prefabPool.CountActive);
        }

        [UnityTest]
        public IEnumerator Free_Object()
        {
            const int requestedCount = 5;

            var objects = _prefabPool.RequestRange(requestedCount);
            var randomIndex = Random.Range(0, requestedCount);
            var randomObject = objects.ElementAt(randomIndex);

            yield return null;

            _prefabPool.ReleaseObject(randomObject);

            yield return null;

            Assert.AreEqual(requestedCount, _prefabPool.CountAll);
            Assert.AreEqual(1, _prefabPool.CountInactive);
            Assert.AreEqual(requestedCount - 1, _prefabPool.CountActive);
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