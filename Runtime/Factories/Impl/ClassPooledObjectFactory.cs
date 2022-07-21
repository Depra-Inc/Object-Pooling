using System;
using System.Collections.Generic;
using Depra.ObjectPooling.Runtime.Factories.Abstract;

namespace Depra.ObjectPooling.Runtime.Factories.Impl
{
    public class ClassPooledObjectFactory<TClass> : PooledObjectFactory<TClass> where TClass : class, new()
    {
        private readonly object[] _constructorArgs;
        private readonly Dictionary<object, TClass> _instances;

        public override TClass CreateObject(object key)
        {
            if (_instances.TryGetValue(key, out var instance) == false)
            {
                instance = CreateClass(_constructorArgs);
                _instances.Add(key, instance);
            }

            OnDisableObject(key, instance);

            return instance;
        }

        public override void DestroyObject(object key, TClass instance)
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