using System;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker;
using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.Model.Scripts
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Inventory/Resources/Items/Resource")]
    public class Resource : Item
    {
        #region SerializeFields

        [SerializeField] private int _maximumAmountInSlot = 99;
        [SerializeField] private Color _shaderColor;

        [SerializeField] private float _lateralDropStrength = 100f;
        [SerializeField] private float _verticalDropStrength = 100f;

        #endregion

        #region PublicGetters

        public int MaximumAmountInSlot => _maximumAmountInSlot;

        #endregion
        
        #region PublicMethods

        public void Instantiate(PickableLoot poolObject)
        {
            var visualEffectComponent = poolObject.GetComponentInChildren<VisualEffect>(); 
            if (!visualEffectComponent) throw new FormatException("Pool Object doesnt have Visual Effect attached to any of its children");
            visualEffectComponent.SetVector4("Shine Color", new Vector4(_shaderColor.r, _shaderColor.g, _shaderColor.b, 0));
            
            poolObject.gameObject.SetActive(true);

            var rigidbodyComponent = poolObject.GetComponent<Rigidbody>();
            if (!rigidbodyComponent) throw new FormatException("Pool object doesnt have Rigidbody attached to it");
            rigidbodyComponent.freezeRotation = true;
            rigidbodyComponent.AddForce(new Vector3(Random.Range(-1f, 1f) * _lateralDropStrength, _verticalDropStrength, Random.Range(-1f, 1f) * _lateralDropStrength));
        }

        #endregion
    }
}
