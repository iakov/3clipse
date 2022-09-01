using System;
using System.Collections.Generic;
using UnityEngine;

namespace _3ClipseGame.Steam.Global.Scripts.Pool
{
    public class ObjectPool : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private GameObject poolObjectPrefab;
        [SerializeField] private int poolAmount = 10;

        #endregion

        #region PrivateFields

        private Queue<GameObject> _pooledObjects = new();
        private List<GameObject> _unPooledObjects = new();
        private Transform _transform;

        #endregion

        #region MonoBehaviourMethods

        private void Awake() => _transform = GetComponent<Transform>();
        private void Start() => InstantiateLootObjects();
        private void InstantiateLootObjects()
        {
            var i = 0;
            while (i < poolAmount)
            {
                var newObject = Instantiate(poolObjectPrefab, _transform);
                newObject.SetActive(false);
                _pooledObjects.Enqueue(newObject);
                i++;
            }
        }

        #endregion

        #region PublicMethods

        public GameObject GetPoolObject()
        {
            var result = _pooledObjects.Dequeue();
            _unPooledObjects.Add(result);
            return result;
        }

        public void PutObjectInPool(GameObject poolObject)
        {
            if (!_unPooledObjects.Contains(poolObject)) throw new Exception("Trying to pool object which wasn't the part of pool");
            
            poolObject.SetActive(false);
            poolObject.transform.SetParent(_transform);
            _pooledObjects.Enqueue(poolObject);
        }

        #endregion
    }
}