using System;
using System.Collections.Generic;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Detector
{
    [RequireComponent(typeof(SphereCollider))]
    public class LootDetector : MonoBehaviour
    { 
        public event Action<PickableLoot> NewLootDetected;
        public event Action<PickableLoot> LootRetired;

        private List<PickableLoot> _detectedLoot = new();

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent<PickableLoot>(out var lootComponent) || _detectedLoot.Contains(lootComponent)) return;

            _detectedLoot.Add(lootComponent);
            NewLootDetected?.Invoke(lootComponent);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.TryGetComponent<PickableLoot>(out var lootComponent) || !_detectedLoot.Contains(lootComponent)) return;

            _detectedLoot.Remove(lootComponent);
            LootRetired?.Invoke(lootComponent);
        }
    }
}
