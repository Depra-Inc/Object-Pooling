using System;
using System.Collections.Generic;
using Depra.ObjectPooling.Runtime.Processors.Abstract;

namespace Depra.ObjectPooling.Runtime.Processors.Impl
{
    public class ClassInstanceProcessor<TClass> : InstanceProcessor<TClass> where TClass : class, new()
    {
        private readonly Dictionary<object, TClass> _instances;

        public override TClass CreateInstance(object key)
        {
            if (_instances.TryGetValue(key, out var instance) == false)
            {
                instance = new TClass();
                _instances.Add(key, instance);
            }

            OnDisableInstance(key, instance);

            return instance;
        }

        public override void DestroyInstance(object key, TClass instance)
        {
            DestroyClass(instance);

            if (_instances.TryGetValue(key, out var actualInstance) == false)
            {
                return;
            }
            
            if (instance == actualInstance)
            {
                _instances.Remove(key);
            }
        }

        public ClassInstanceProcessor()
        {
            _instances = new Dictionary<object, TClass>();
        }

        private static void DestroyClass(TClass @class)
        {
            if (@class is IDisposable disposableInstance)
            {
                disposableInstance.Dispose();
            }
        }
    }
}