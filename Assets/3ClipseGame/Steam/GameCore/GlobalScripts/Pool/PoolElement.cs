using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.GlobalScripts.Pool
{
    public class PoolElement : MonoBehaviour
    {
        [SerializeField] private Pool _pool;
        
        public void ReturnToPool()
        {
            _pool.PutObjectInPool(gameObject);
        }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }
    }
}
