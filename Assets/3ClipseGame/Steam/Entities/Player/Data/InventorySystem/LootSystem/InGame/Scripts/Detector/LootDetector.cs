using System;
using System.Collections.Generic;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.LootComponent;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.Detector
{
    [RequireComponent(typeof(SphereCollider))]
    
    public class LootDetector : MonoBehaviour
    {
        #region Public

        public event Action<PickableLoot> NewLootDetected;
        public event Action<PickableLoot> LootRetired;

        #endregion

        #region Initialization

        private List<PickableLoot> _detectedLoot;

        private void Awake()
        {
            _detectedLoot = new List<PickableLoot>();
        }

        #endregion

        #region Detection

        private void OnTriggerEnter(Collider other)
        {
            if(!IsUnderEnterConditions(other)) return;
            AddObjectToDetected(other.GetComponent<PickableLoot>());
        }

        private bool IsUnderEnterConditions(Collider other)
        {
            return IsPickableLoot(other, out var lootComponent) 
                   && !IsInList(lootComponent);
        }

        private void AddObjectToDetected(PickableLoot lootComponent)
        {
            _detectedLoot.Add(lootComponent);
            lootComponent.Disappeared += RemoveObjectFromDetected;
            NewLootDetected?.Invoke(lootComponent);
        }

        #endregion

        #region UnDetection

        private void OnTriggerExit(Collider other)
        {
            if (!IsUnderExitConditions(other)) return;
            RemoveObjectFromDetected(other.GetComponent<PickableLoot>());
        }

        private bool IsUnderExitConditions(Collider other)
        {
            return IsPickableLoot(other, out var lootComponent)
                   && IsInList(lootComponent);
        }

        private void RemoveObjectFromDetected(PickableLoot lootComponent)
        {
            _detectedLoot.Remove(lootComponent);
            lootComponent.Disappeared -= RemoveObjectFromDetected;
            LootRetired?.Invoke(lootComponent);
        }

        #endregion

        private bool IsPickableLoot(Collider other, out PickableLoot loot)
        {
            return other.TryGetComponent<PickableLoot>(out loot);
        }

        private bool IsInList(PickableLoot lootComponent)
        {
            return _detectedLoot.Contains(lootComponent);
        }
    }
}
