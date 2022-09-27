using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.Detector
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

        public bool TryAddLoot([NotNull] PickableLoot lootComponent)
        {
            if (Contains(lootComponent)) return false;
            
            AddLoot(lootComponent);
            return true;
        }

        private void AddLoot([NotNull] PickableLoot lootComponent)
        {
            _detectedLoot.Add(lootComponent);
            lootComponent.Disappeared += RemoveLoot;
            LootAdded?.Invoke(lootComponent);
        }

        public bool TryRemoveLoot([NotNull] PickableLoot lootComponent)
        {
            if (!Contains(lootComponent)) return false;

            RemoveLoot(lootComponent);
            return true;
        }

        public bool Contains([NotNull] PickableLoot lootComponent)
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