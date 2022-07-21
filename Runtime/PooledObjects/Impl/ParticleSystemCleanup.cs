using Depra.ObjectPooling.Runtime.Common;
using Depra.ObjectPooling.Runtime.PooledObjects.Abstract;
using Depra.ObjectPooling.Runtime.Pools.Abstract;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.PooledObjects.Impl
{
    [RequireComponent(typeof(ParticleSystem))]
    [AddComponentMenu(Constants.FrameworkPath + Constants.FrameworkName + "Particle System Cleanup")]
    public class ParticleSystemCleanup : PooledMonoBehavior
    {
        [SerializeField]
        private ParticleSystemStopBehavior _resetBehaviour = ParticleSystemStopBehavior.StopEmittingAndClear;

        [SerializeField] private ParticleSystem _particleSystem;

        public override void OnPoolReuse()
        {
            _particleSystem.Stop(true, _resetBehaviour);
            _particleSystem.time = 0f;
        }

        public override void OnPoolGet()
        {
            if (_particleSystem.main.playOnAwake)
            {
                _particleSystem.Play();
            }
        }

        public override void OnPoolSleep()
        {
            if (_particleSystem.isPlaying)
            {
                _particleSystem.Stop();
            }
        }

        private void Awake()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            if (_particleSystem == false)
            {
                _particleSystem = GetComponent<ParticleSystem>();
            }
        }
    }
}