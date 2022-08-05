using System;
using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem;
using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.ScriptableObjects
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

            var rigidbody = pooledObject.GetComponent<Rigidbody>();
            var visualEffect = pooledObject.GetComponentInChildren<VisualEffect>();
            
            visualEffect.SetVector4("Shine Color", new Vector4(shaderColor.r, shaderColor.g, shaderColor.b, 0));

            if (!pooledObject.TryGetComponent<PickableLoot>(out var lootComponent)) lootComponent = pooledObject.AddComponent<PickableLoot>();
            lootComponent.Resource = this;
            lootComponent.Amount = amount;

            pooledObject.transform.position = position + Vector3.up;
            rigidbody.freezeRotation = true;
            
            pooledObject.SetActive(true);
            
            rigidbody.AddForce(new Vector3(Random.Range(-1f, 1f) * _dropStrength, _dropStrength, Random.Range(-1f, 1f) * _dropStrength));
        }

        #endregion
    }
}
