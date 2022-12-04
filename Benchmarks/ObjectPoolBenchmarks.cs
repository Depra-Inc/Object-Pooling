// Copyright © 2022 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using BenchmarkDotNet.Attributes;
using Depra.ObjectPooling.Application.Configuration.Impl;
using Depra.ObjectPooling.Application.Pools.Impl;
using Depra.ObjectPooling.Domain.Extensions;

namespace Depra.ObjectPooling.Benchmarks
{
    public class ObjectPoolBenchmarks
    {
        private const int POOL_CAPACITY = 100;

        private TestPooled _lastObject;
        private ObjectPool<TestPooled> _objectPool;

        [IterationSetup]
        public void Setup()
        {
            _objectPool = CreatePool();
            _objectPool.Request();
        }

        [Benchmark]
        public void RequestObject() => _objectPool.Request();

        [Benchmark]
        public void ReleaseObject() => _objectPool.Release(_lastObject);

        [Benchmark]
        public void WarmUp() => _objectPool.WarmUp(POOL_CAPACITY);

        private ObjectPool<TestPooled> CreatePool()
        {
            var configuration = new DefaultPoolConfiguration<TestPooled>(POOL_CAPACITY,
                CreatePooledObject, OnRequest, OnRelease, OnDestroy);

            var pool = new ObjectPool<TestPooled>("Test", configuration);

            return pool;
        }

        private TestPooled CreatePooledObject() => _lastObject = new TestPooled();

        private void OnRequest(TestPooled pooledObject) { }

        private void OnRelease(TestPooled pooledObject) { }

        private void OnDestroy(TestPooled pooledObject) { }
    }
}