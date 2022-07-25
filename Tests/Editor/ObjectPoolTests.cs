using System;
using System.Linq;
using Depra.ObjectPooling.Runtime.Configuration.Impl;
using Depra.ObjectPooling.Runtime.Extensions;
using Depra.ObjectPooling.Runtime.Pools.Objects;
using NUnit.Framework;

namespace Depra.ObjectPooling.Tests.Editor
{
    public class ObjectPoolTests
    {
        private const string Key = "TestObjectPool";
        private const int DefaultCapacity = 10;

        private ObjectPool<TestPooled> _objectPool;

        [SetUp]
        public void SetUp()
        {
            var configuration = new DefaultPoolConfiguration<TestPooled>(DefaultCapacity, 
                CreatePooledObject, null, null, null);

            _objectPool = new ObjectPool<TestPooled>(Key, configuration);
        }

        [TearDown]
        public void TearDown()
        {
            _objectPool.Dispose();
        }

        [Test]
        public void Warm_Up()
        {
            _objectPool.WarmUp(30);

            Assert.AreEqual(30, _objectPool.CountAll);
            Assert.AreEqual(0, _objectPool.CountActive);
            Assert.AreEqual(30, _objectPool.CountInactive);
        }

        [Test]
        public void Request_Object()
        {
            var obj = _objectPool.Request();

            Assert.IsNotNull(obj);
            Assert.AreEqual(true, obj.Created);
            Assert.AreEqual(1, _objectPool.CountActive);
            Assert.AreEqual(0, _objectPool.CountInactive);
        }

        [Test]
        public void Release_Object()
        {
            const int count = 2;
            var collection = CreateCollectionOfPooledObjects(count);
            var lastObject = collection.Last();

            _objectPool.AddFreeRange(collection);
            _objectPool.Request();
            _objectPool.Request();
            _objectPool.Release(lastObject);

            Assert.AreEqual(true, lastObject.Free);
            Assert.AreEqual(count - 1, _objectPool.CountActive);
            Assert.AreEqual(count - 1, _objectPool.CountInactive);
        }

        [Test]
        public void Clear()
        {
            _objectPool.Request();

            Assert.AreNotEqual(0, _objectPool.CountAll);

            _objectPool.Clear();

            Assert.AreEqual(0, _objectPool.CountAll);
        }

        [Test]
        public void Add_Free_Objects_Range()
        {
            const int count = 20;
            var collection = CreateCollectionOfPooledObjects(count);
            _objectPool.AddFreeRange(collection);

            Assert.AreEqual(0, _objectPool.CountActive);
            Assert.AreEqual(count, _objectPool.CountAll);
            Assert.AreEqual(count, _objectPool.CountInactive);
            Array.ForEach(collection, pooled => Assert.AreEqual(true, pooled.Free));
        }

        [Test]
        public void Request_Objects_Range()
        {
            const int count = 30;
            _objectPool.RequestRange(count);

            Assert.AreEqual(count, _objectPool.CountAll);
            Assert.AreEqual(0, _objectPool.CountInactive);
            Assert.AreEqual(count, _objectPool.CountActive);
        }

        [Test]
        public void Release_Objects_Range()
        {
            const int count = 10;

            var collection = CreateCollectionOfPooledObjects(count);
            _objectPool.AddFreeRange(collection);

            _ = _objectPool.RequestRange(count / 2);

            var collectionForFree = collection.AsSpan()[..(count / 2)].ToArray();
            _objectPool.ReleaseRange(collectionForFree);

            Assert.AreEqual(0, _objectPool.CountActive);
            Assert.AreEqual(count, _objectPool.CountAll);
            Assert.AreEqual(count, _objectPool.CountInactive);
        }

        private static TestPooled CreatePooledObject()
        {
            return new TestPooled();
        }

        private static TestPooled[] CreateCollectionOfPooledObjects(int count)
        {
            var objectCollection = new TestPooled[count];
            for (var i = 0; i < objectCollection.Length; i++)
            {
                objectCollection[i] = CreatePooledObject();
            }

            return objectCollection;
        }
    }
}