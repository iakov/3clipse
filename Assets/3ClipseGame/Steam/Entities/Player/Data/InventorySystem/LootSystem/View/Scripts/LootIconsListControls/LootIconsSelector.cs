using System;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts.LootIconsListControls
{
    [RequireComponent(typeof(LootScrollHandler))]
    [RequireComponent(typeof(LootDisplay))]
    
    public class LootIconsSelector : MonoBehaviour
    {
        public event Action<LootIcon, LootIcon> SelectedLootChanged;

        private LootDisplay _lootDisplay;
        private LootScrollHandler _lootScrollHandler;
        private LootScrollHandler _scrollHandler;

        private LootIcon _currentSelectedLoot;
        
        private void Awake()
        {
            _lootDisplay = GetComponent<LootDisplay>();
            _lootScrollHandler = GetComponent<LootScrollHandler>();
        }

        private void OnEnable()
        {
            _lootDisplay.LootDisplayListDecreasing += OnLootDisplayListDecreasing;
            _lootDisplay.LootDisplayListIncreased += OnLootDisplayListIncreased;
            _lootScrollHandler.Scrolled += SwitchCurrentSelectedLoot;
        }

        private void OnDisable()
        {
            _lootDisplay.LootDisplayListDecreasing -= OnLootDisplayListDecreasing;
            _lootDisplay.LootDisplayListIncreased -= OnLootDisplayListIncreased;
            _lootScrollHandler.Scrolled -= SwitchCurrentSelectedLoot;
        }

        public LootIcon GetCurrentSelectedLoot()
        {
            return _currentSelectedLoot;
        }

        private void OnLootDisplayListIncreased(PickableLoot newLoot)
        {
            if (_currentSelectedLoot != null) return;
            
            var icon = _lootDisplay.GetIconByObject(newLoot);
            SwitchCurrentSelectedLoot(icon);
        }

        private void OnLootDisplayListDecreasing(PickableLoot retiredLoot)
        {
            var retiredLootIcon = _lootDisplay.GetIconByObject(retiredLoot);
            if (_currentSelectedLoot != retiredLootIcon) return;
            
            var newSelected = GetClosestToCurrentIcon();
            SwitchCurrentSelectedLoot(newSelected);
        }

        private LootIcon GetClosestToCurrentIcon()
        {
            return _lootDisplay.GetPreviousObject(_currentSelectedLoot)
                   ?? _lootDisplay.GetNextObject(_currentSelectedLoot);
        }

        private void SwitchCurrentSelectedLoot(LootIcon newLoot)
        {
            var previousLoot = _currentSelectedLoot;
            _currentSelectedLoot = newLoot;
            SelectedLootChanged?.Invoke(previousLoot, _currentSelectedLoot);
        }
    }
}
