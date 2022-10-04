using UnityEngine;

namespace _3ClipseGame.Steam.Core.Scripts.Pool
{
    public class PoolElement : MonoBehaviour
    {
        #region Public

        public void ReturnToPool()
        {
            _pool.PutObjectInPool(gameObject);
        }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        [SerializeField] private Pool _pool;

        #endregion
    }
}
