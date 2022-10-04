using System;
using System.Collections.Generic;
using _3ClipseGame.Steam.Core.Scripts.Pool;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem
{
    public class LootPool : Pool
    {
        #region SerializeFields

        [SerializeField] private int poolAmount = 10;
        [SerializeField] private GameObject poolObjectPrefab;

        #endregion

        #region Initialization

        private Queue<GameObject> _pooledObjects;
        private List<GameObject> _unPooledObjects;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _pooledObjects = new Queue<GameObject>();
            _unPooledObjects = new List<GameObject>();
        }

        private void Start()
        {
            if (poolObjectPrefab == null) 
                poolObjectPrefab = new GameObject();
            InstantiateLootObjects();
        }

        private void InstantiateLootObjects()
        {
            var i = 0;
            while (i < poolAmount)
            {
                InstantiateObject();
                i++;
            }
        }

        private void InstantiateObject()
        {
            var newObject = CreateDisabled();
            AddPickableComponents(newObject);
            AddPoolElementComponents(newObject);
            _pooledObjects.Enqueue(newObject);
        }
        
        private GameObject CreateDisabled()
        {
            var newObject = Instantiate(poolObjectPrefab, _transform);
            newObject.SetActive(false);
            return newObject;
        }

        private void AddPickableComponents(GameObject newObject)
        {
            if(newObject.TryGetComponent<DePooledPickableLoot>(out var loot)) Destroy(loot);
            newObject.AddComponent<PooledPickableLoot>();
        }

        private void AddPoolElementComponents(GameObject newObject)
        {
            var element = newObject.GetComponent<PoolElement>() 
                          ?? newObject.AddComponent<PoolElement>();
            element.SetPool(this);
        }

        #endregion

        #region PublicMethods

        public override GameObject GetPoolObject()
        {
            var result = _pooledObjects.Dequeue();
            _unPooledObjects.Add(result);
            return result;
        }

        public override void PutObjectInPool(GameObject poolObject)
        {
            if (!_unPooledObjects.Contains(poolObject)) throw new Exception("Trying to pool object which wasn't the part of pool");
            
            poolObject.SetActive(false);
            poolObject.transform.SetParent(_transform);
            _pooledObjects.Enqueue(poolObject);
        }

        #endregion
    }
}
