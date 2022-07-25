using Depra.ObjectPooling.Runtime.Common;
using Depra.ObjectPooling.Runtime.Pooled.UnityObjects.Abstract;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.Pooled.UnityObjects.Impl
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu(Constants.FrameworkPath + Constants.FrameworkName + "Rigidbody2D Cleanup")]
    public class Rigidbody2DCleanup : PooledMonoBehavior
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        public override void OnPoolReuse()
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
        }

        private void Awake()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            if (_rigidbody == false)
            {
                _rigidbody = GetComponent<Rigidbody2D>();
            }
        }
    }
}