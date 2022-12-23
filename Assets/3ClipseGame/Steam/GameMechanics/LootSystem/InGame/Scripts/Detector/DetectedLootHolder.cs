using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using _3ClipseGame.Steam.GameMechanics.LootSystem.InGame.Scripts.LootComponent;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.LootSystem.InGame.Scripts.Detector
{
    public class DetectedLootHolder
    {
        #region Public

        public event Action<PickableLoot> LootRemoved;
        public event Action<PickableLoot> LootAdded;
        
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
        
        public bool TryRemoveLoot([NotNull] PickableLoot lootComponent)
        {
            if (!Contains(lootComponent)) return false;

            RemoveLoot(lootComponent);
            return true;
        }

        public bool Contains([NotNull] PickableLoot lootComponent)
        {
            return _detectedLoot.Contains(lootComponent.gameObject);
        }

        #endregion

        #region Initialization

        private List<GameObject> _detectedLoot;

        private DetectedLootHolder()
        {
            _detectedLoot = new List<GameObject>();
        }

        #endregion

        private void AddLoot([NotNull] PickableLoot lootComponent)
        {
            _detectedLoot.Add(lootComponent.gameObject);
            lootComponent.Disappeared += RemoveLoot;
            LootAdded?.Invoke(lootComponent);
        }

        private void RemoveLoot(PickableLoot lootComponent)
        {
            _detectedLoot.Remove(lootComponent.gameObject);
            lootComponent.Disappeared -= RemoveLoot;
            LootRemoved?.Invoke(lootComponent);
        }
    }
}