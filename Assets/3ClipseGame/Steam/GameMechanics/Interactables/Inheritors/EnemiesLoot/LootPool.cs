using System;
using System.Collections.Generic;
using _3ClipseGame.Steam.GameCore.GlobalScripts.Pool;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.EnemiesLoot.KeyLogic;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.EnemiesLoot
{
    public class LootPool : Pool
    {
        [SerializeField] private int poolAmount = 10;
        [SerializeField] private GameObject poolObjectPrefab;

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
            if(!newObject.TryGetComponent<LootInteractable>(out var loot)) 
                newObject.AddComponent<LootInteractable>();
        }

        private void AddPoolElementComponents(GameObject newObject)
        {
            var element = newObject.GetComponent<PoolElement>() 
                          ?? newObject.AddComponent<PoolElement>();
            element.SetPool(this);
        }
        
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
    }
}
