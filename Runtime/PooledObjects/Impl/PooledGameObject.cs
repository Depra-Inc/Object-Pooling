using Depra.ObjectPooling.Runtime.Common;
using Depra.ObjectPooling.Runtime.PooledObjects.Abstract;
using Depra.ObjectPooling.Runtime.Pools.Abstract;
using Depra.ObjectPooling.Runtime.Pools.Interfaces;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.PooledObjects.Impl
{
    public class PooledGameObjectWithChildren : MonoBehaviour
    {
        
    }
    
    [AddComponentMenu(Constants.FrameworkPath + Constants.ModulePath + "Pooled Game Object")]
    public class PooledGameObject : PooledMonoBehavior
    {
        private IPool _owner;
        private bool _needRecycle;

        private void LateUpdate()
        {
            if (_needRecycle)
            {
                _needRecycle = false;
            }
        }

        public override void OnPoolCreate(IPool pool)
        {
            if (_owner == null)
            {
                _owner = pool;
            }
            else
            {
                _owner.FreeObject(this);
            }
        }

        public override void OnPoolReuse()
        {
            _needRecycle = true;
        }
    }
}