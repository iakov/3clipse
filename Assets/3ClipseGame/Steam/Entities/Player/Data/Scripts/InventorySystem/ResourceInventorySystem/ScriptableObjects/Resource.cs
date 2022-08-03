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

        #endregion

        #region PublicGetters

        public int MaximumAmountInSlot => maximumAmountInSlot;

        #endregion
        
        #region PrivateFields

        private float _dropStrength = 100f;

        #endregion
        
        #region PublicMethods

        public void DropOnGround(Vector3 position, int amount)
        {
            if(amount == 0) return;
            
            if (LootPrefab == null) throw new Exception("Object prefab is null");
            
            if (!LootPrefab.GetComponent<Rigidbody>()) throw new Exception("Prefab must have Rigidbody on it");
            
            var instantiatedObject = Instantiate(LootPrefab, position, Quaternion.identity);
            var rigidbody = instantiatedObject.GetComponent<Rigidbody>();

            if (!instantiatedObject.TryGetComponent<PickableLoot>(out var lootComponent)) lootComponent = instantiatedObject.AddComponent<PickableLoot>();
            lootComponent.Resource = this;
            lootComponent.Amount = amount;

            rigidbody.position = position + Vector3.up;
            rigidbody.AddForce(new Vector3(Random.Range(-1f, 1f) * _dropStrength, _dropStrength, Random.Range(-1f, 1f) * _dropStrength));
            rigidbody.freezeRotation = true;
        }

        #endregion
    }
}
