using Depra.ObjectPooling.Runtime.Common;
using Depra.ObjectPooling.Runtime.Pooled.UnityObjects.Abstract;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.Pooled.UnityObjects.Impl
{
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu(Constants.FrameworkPath + Constants.FrameworkName + "Rigidbody Cleanup")]
    public class RigidbodyCleanup : PooledMonoBehavior
    {
        [SerializeField] private Rigidbody _rigidbody;

        public override void OnPoolReuse()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        private void Awake()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            if (_rigidbody == false)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }
        }
    }
}