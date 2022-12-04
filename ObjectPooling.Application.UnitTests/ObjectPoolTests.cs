// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Linq;
using Depra.ObjectPooling.Application.Configuration.Impl;
using Depra.ObjectPooling.Application.Factories.Obj;
using Depra.ObjectPooling.Application.Internal.Buffers.Impl;
using Depra.ObjectPooling.Application.Pools.Extensions;
using Depra.ObjectPooling.Application.Pools.Impl;
using Depra.ObjectPooling.Domain.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Depra.ObjectPooling.Application.UnitTests
{
    public class ObjectPoolTests
    {
        private const int POOL_CAPACITY = 10;
        private const string KEY = nameof(ObjectPoolTests);

        private ObjectPool<TestPooled> _objectPool;

        [SetUp]
        public void Setup()
        {
            _objectPool = new ObjectPool<TestPooled>(KEY, new PoolConfiguration<TestPooled>(
                objectFactory: new CustomPooledObjectFactory<TestPooled>(CreatePooledObject, null, null, null),
                borrowStrategy: BorrowStrategy.FIFO,
                capacity: POOL_CAPACITY));
        }

        [TearDown]
        public void TearDown()
        {
            _objectPool.Dispose();
        }

        [Test]
        public void Warm_Up()
        {
            // Assert.
            const int ELEMENT_COUNT = 30;

            // Act.
            _objectPool.WarmUp(ELEMENT_COUNT);

            // Assert.
            _objectPool.CountAll.Should().Be(ELEMENT_COUNT);
            _objectPool.CountActive.Should().Be(0);
            _objectPool.CountInactive.Should().Be(ELEMENT_COUNT);
        }

        [Test]
        public void Request_Object()
        {
            // Act.
            var obj = _objectPool.Request();

            // Asset.
            obj.Should().NotBeNull();
            obj.Created.Should().BeTrue();
            _objectPool.CountActive.Should().Be(1);
            _objectPool.CountInactive.Should().Be(0);
        }

        [Test]
        public void Release_Object()
        {
            // Arrange.
            const int COUNT = 2;
            var collection = CreateCollectionOfPooledObjects(COUNT);
            var lastObject = collection.Last();
            _objectPool.AddFreeRange(collection);
            _objectPool.Request();
            _objectPool.Request();

            // Act.
            _objectPool.Release(lastObject);

            // Asset.
            lastObject.Free.Should().BeTrue();
            _objectPool.CountActive.Should().Be(COUNT - 1);
            _objectPool.CountInactive.Should().Be(COUNT - 1);
        }

        [Test]
        public void Clear()
        {
            // Arrange.
            void Arrange() => _objectPool.Request();

            // Act.
            void Act() => _objectPool.Clear();

            // Arrange.
            Arrange();
            _objectPool.CountAll.Should().BeGreaterThan(0);
            Act();
            _objectPool.CountAll.Should().Be(0);
        }

        [Test]
        public void Add_Free_Objects_Range()
        {
            // Arrange.
            const int COUNT = 20;
            var collection = CreateCollectionOfPooledObjects(COUNT);
            
            // Act.
            _objectPool.AddFreeRange(collection);

            // Assert.
            _objectPool.CountActive.Should().Be(0);
            _objectPool.CountAll.Should().Be(COUNT);
            _objectPool.CountInactive.Should().Be(COUNT);
            collection.Should().AllSatisfy(pooled => pooled.Free.Should().BeTrue());
        }

        [Test]
        public void Request_Objects_Range()
        {
            // Arrange.
            const int COUNT = 30;

            // Act.
            _objectPool.RequestRange(COUNT);

            // Assert.
            _objectPool.CountAll.Should().Be(COUNT);
            _objectPool.CountInactive.Should().Be(0);
            _objectPool.CountActive.Should().Be(COUNT);
        }

        [Test]
        public void Release_Objects_Range()
        {
            // Arrange.
            const int COUNT = 10;
            var collection = CreateCollectionOfPooledObjects(COUNT);
            _objectPool.AddFreeRange(collection);
            _ = _objectPool.RequestRange(COUNT / 2);
            var collectionForFree = collection.AsSpan()[..(COUNT / 2)].ToArray();
            
            // Act.
            _objectPool.ReleaseRange(collectionForFree);

            // Assert.
            _objectPool.CountActive.Should().Be(0);
            _objectPool.CountAll.Should().Be(COUNT);
            _objectPool.CountInactive.Should().Be(COUNT);
        }

        private static TestPooled CreatePooledObject() => new();

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