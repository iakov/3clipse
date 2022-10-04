using UnityEngine;

namespace _3ClipseGame.Steam.Core.Scripts.Pool
{
    public abstract class Pool : MonoBehaviour
    {
        public abstract GameObject GetPoolObject();
        public abstract void PutObjectInPool(GameObject poolObject);
    }
}