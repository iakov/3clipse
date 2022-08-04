using System;
using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.ResourceInventorySystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Inventory/Resources/Items/Resource")]
    public class Resource : Item
    {
        #region SerializeFields

        [SerializeField] private int maximumAmountInSlot = 99;
        [SerializeField] private Color shaderColor;

        #endregion

        #region PublicGetters

        public int MaximumAmountInSlot => maximumAmountInSlot;

        #endregion
        
        #region PrivateFields

        private float _dropStrength = 100f;

        #endregion
        
        #region PublicMethods

        public void DropOnGround(GameObject pooledObject, int amount, Vector3 position, Transform parent = null)
        {
            if(!pooledObject.GetComponent<Renderer>()) throw new Exception("Prefab must have Renderer on it");
            if (!pooledObject.GetComponent<Rigidbody>()) throw new Exception("Prefab must have Rigidbody on it");

            pooledObject.transform.parent = parent;
            pooledObject.SetActive(true);

            var rigidbody = pooledObject.GetComponent<Rigidbody>();
            var renderer = pooledObject.GetComponent<Renderer>();

            if (!pooledObject.TryGetComponent<PickableLoot>(out var lootComponent)) lootComponent = pooledObject.AddComponent<PickableLoot>();
            lootComponent.Resource = this;
            lootComponent.Amount = amount;

            rigidbody.position = position + Vector3.up;
            rigidbody.AddForce(new Vector3(Random.Range(-1f, 1f) * _dropStrength, _dropStrength, Random.Range(-1f, 1f) * _dropStrength));
            rigidbody.freezeRotation = true;
        }

        #endregion
    }
}
