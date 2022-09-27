using System;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.Detector
{
    [RequireComponent(typeof(SphereCollider))]
    
    public class LootDetector : MonoBehaviour
    {
        #region Public

        public event Action<PickableLoot> LootDetected;
        public event Action<PickableLoot> LootRetired;

        #endregion

        #region Initialization

        private DetectedLootHolder _detectedLootHolder;

        private void Awake()
        {
            _detectedLootHolder = DetectedLootHolder.Empty();
            GetComponent<SphereCollider>().isTrigger = true;
        }

        private void OnEnable()
        {
            _detectedLootHolder.LootAdded += OnLootAdded;
            _detectedLootHolder.LootRemoved += OnLootRemoved;
        }
        
        private void OnDisable()
        {
            _detectedLootHolder.LootAdded -= OnLootAdded;
            _detectedLootHolder.LootRemoved -= OnLootRemoved;
        }

        private void OnLootAdded(PickableLoot loot)
        {
            LootDetected?.Invoke(loot);
        }
        
        private void OnLootRemoved(PickableLoot loot)
        {
            LootRetired?.Invoke(loot);
        }

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if(!IsPickableLoot(other, out var loot)) return;
            
            _detectedLootHolder.TryAddLoot(loot);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!IsPickableLoot(other, out var loot)) return;
            _detectedLootHolder.TryRemoveLoot(loot);
        }

        private bool IsPickableLoot(Collider other, out PickableLoot loot)
        {
            return other.TryGetComponent<PickableLoot>(out loot);
        }
    }
}
