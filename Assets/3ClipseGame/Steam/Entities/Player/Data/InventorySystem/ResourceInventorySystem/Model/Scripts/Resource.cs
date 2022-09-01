using System;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Scripts;
using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem;
using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.Model.Scripts
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Inventory/Resources/Items/Resource")]
    public class Resource : Item
    {
        #region SerializeFields

        [SerializeField] private int maximumAmountInSlot = 99;
        [SerializeField] private Color shaderColor;

        [SerializeField] private float lateralDropStrength = 100f;
        [SerializeField] private float verticalDropStrength = 100f;

        #endregion

        #region PublicGetters

        public int MaximumAmountInSlot => maximumAmountInSlot;

        #endregion
        
        #region PublicMethods

        public void Instantiate(PickableLoot poolObject)
        {
            var visualEffectComponent = poolObject.GetComponentInChildren<VisualEffect>(); 
            if (!visualEffectComponent) throw new FormatException("Pool Object doesnt have Visual Effect attached to any of its children");
            visualEffectComponent.SetVector4("Shine Color", new Vector4(shaderColor.r, shaderColor.g, shaderColor.b, 0));
            
            poolObject.gameObject.SetActive(true);

            var rigidbodyComponent = poolObject.GetComponent<Rigidbody>();
            if (!rigidbodyComponent) throw new FormatException("Pool object doesnt have Rigidbody attached to it");
            rigidbodyComponent.freezeRotation = true;
            rigidbodyComponent.AddForce(new Vector3(Random.Range(-1f, 1f) * lateralDropStrength, verticalDropStrength, Random.Range(-1f, 1f) * lateralDropStrength));
        }

        #endregion
    }
}
