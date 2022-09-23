using System;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.Scripts;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.InGame.Scripts
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Inventory/Resources/Items/Resource")]
    public class Resource : Item
    {
        #region Serialization

        [SerializeField] private int _maximumAmountInSlot = 99;
        [SerializeField] private Color _shaderColor;

        [SerializeField] private float _lateralDropStrength = 100f;
        [SerializeField] private float _verticalDropStrength = 100f;

        #endregion

        #region Public

        public int GetMaximumAmountInSlot()
        {
            return _maximumAmountInSlot;
        }
        
        public void Instantiate(GameObject loot)
        {
            SetVFXParameters(loot.gameObject);
            ActivateLoot(loot.gameObject);
            SetRigidbodyParameters(loot.gameObject);
        }

        #endregion

        private void SetVFXParameters(GameObject loot)
        {
            var visualEffectComponent = GetVisualEffectComponent(loot);
            visualEffectComponent.SetVector4("Shine Color", new Vector4(_shaderColor.r, _shaderColor.g, _shaderColor.b, 0));
        }

        private VisualEffect GetVisualEffectComponent(GameObject loot)
        {
            var visualEffectComponent = loot.GetComponentInChildren<VisualEffect>();
            if(visualEffectComponent == null) throw new FormatException("Pool Object doesnt have Visual Effect attached to any of its children");

            return visualEffectComponent;
        }

        private void ActivateLoot(GameObject loot)
        {
            loot.SetActive(true);
        }

        private void SetRigidbodyParameters(GameObject loot)
        {
            var rigidbodyComponent = loot.GetComponent<Rigidbody>();
            if (!rigidbodyComponent) throw new FormatException("Pool object doesnt have Rigidbody attached to it");
            rigidbodyComponent.freezeRotation = true;
            rigidbodyComponent.AddForce(new Vector3(Random.Range(-1f, 1f) * _lateralDropStrength, _verticalDropStrength,
                Random.Range(-1f, 1f) * _lateralDropStrength));
        }
    }
}
