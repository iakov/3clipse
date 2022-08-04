using System;
using System.Collections.Generic;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts.Pool
{
    public class ObjectPool : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private GameObject poolObjectPrefab;
        [SerializeField] private int poolAmount = 10;

        #endregion

        #region PrivateFields

        private List<GameObject> _pooledObjects = new();
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
                _pooledObjects.Add(Instantiate(poolObjectPrefab, _transform));
                _pooledObjects[i].SetActive(false);
                i++;
            }
        }

        #endregion

        #region PublicMethods

        public GameObject GetPoolObject()
        {
            var result = _pooledObjects[0];
            _pooledObjects.Remove(result);
            _unPooledObjects.Add(result);
            return result;
        }

        public void PutObjectInPool(GameObject poolObject)
        {
            if (!_unPooledObjects.Contains(poolObject)) throw new Exception("Trying to pool object which wasn't the part of pool");
            
            poolObject.SetActive(false);
            poolObject.transform.SetParent(_transform);
            _pooledObjects.Add(poolObject);
        }

        #endregion
    }
}