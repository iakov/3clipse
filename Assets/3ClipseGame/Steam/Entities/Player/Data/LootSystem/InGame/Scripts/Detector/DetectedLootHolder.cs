using System;
using System.Collections.Generic;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.LootComponent;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.Detector
{
    public class DetectedLootHolder
    {
        public event Action<PickableLoot> LootRemoved;
        public event Action<PickableLoot> LootAdded;
        
        private List<PickableLoot> _detectedLoot;

        private DetectedLootHolder()
        {
            _detectedLoot = new List<PickableLoot>();
        }

        public static DetectedLootHolder Empty()
        {
            return new DetectedLootHolder();
        }

        public bool TryAddLoot(PickableLoot lootComponent)
        {
            if (IsInList(lootComponent)) return false;
            
            AddLoot(lootComponent);
            return true;
        }
        
        private void AddLoot(PickableLoot lootComponent)
        {
            _detectedLoot.Add(lootComponent);
            lootComponent.Disappeared += RemoveLoot;
            LootAdded?.Invoke(lootComponent);
        }

        public bool TryRemoveLoot(PickableLoot lootComponent)
        {
            if (!IsInList(lootComponent)) return false;

            RemoveLoot(lootComponent);
            return true;
        }

        private bool IsInList(PickableLoot lootComponent)
        {
            return _detectedLoot.Contains(lootComponent);
        }

        private void RemoveLoot(PickableLoot lootComponent)
        {
            _detectedLoot.Remove(lootComponent);
            lootComponent.Disappeared -= RemoveLoot;
            LootRemoved?.Invoke(lootComponent);
        }
    }
}