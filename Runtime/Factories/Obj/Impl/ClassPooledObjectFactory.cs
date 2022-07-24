using System;
using System.Collections.Generic;
using Depra.ObjectPooling.Runtime.Factories.Obj.Interfaces;

namespace Depra.ObjectPooling.Runtime.Factories.Obj.Impl
{
    public readonly struct ClassPooledObjectFactory<TClass> : IPooledObjectFactory<TClass> where TClass : class, new()
    {
        private readonly object[] _constructorArgs;
        private readonly Dictionary<object, TClass> _instances;

        public TClass CreateObject(object key)
        {
            if (_instances.TryGetValue(key, out var instance) == false)
            {
                instance = CreateClass(_constructorArgs);
                _instances.Add(key, instance);
            }

            OnDisableObject(key, instance);

            return instance;
        }

        public void DestroyObject(object key, TClass instance)
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

        public void OnEnableObject(object key, TClass instance)
        {
        }

        public void OnDisableObject(object key, TClass instance)
        {
        }

        public ClassPooledObjectFactory(object[] constructorArgs)
        {
            _constructorArgs = constructorArgs;
            _instances = new Dictionary<object, TClass>();
        }

        private static TClass CreateClass(object[] constructorArgs)
        {
            var newClass = constructorArgs == null
                ? Activator.CreateInstance<TClass>()
                : (TClass)Activator.CreateInstance(typeof(TClass), constructorArgs);

            return newClass;
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