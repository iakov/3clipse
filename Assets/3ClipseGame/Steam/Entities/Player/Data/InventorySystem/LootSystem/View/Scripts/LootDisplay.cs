using System;
using System.Collections.Generic;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Detector;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts
{
    public class LootDisplay : MonoBehaviour
    {
        public Dictionary<PickableLoot, LootIcon> DisplayedLootAndItsIcons = new();
        public event Action LootDisplayListDecreased;
        public event Action LootDisplayListIncreased; 

        [SerializeField] private LootDetector _lootDetector;
        [SerializeField] private LootIcon _lootIconPrefab;
        [SerializeField] private VerticalLayoutGroup _iconsParent;

        private void OnEnable()
        {
            _lootDetector.NewLootDetected += AddNewIcon;
            _lootDetector.LootRetired += RemoveIcon;
        }

        private void OnDisable()
        {
            _lootDetector.NewLootDetected -= AddNewIcon;
            _lootDetector.LootRetired -= RemoveIcon;
        }

        private void AddNewIcon(PickableLoot newLoot)
        {
            var newIcon = InstantiateNewIcon();
            newIcon.SwitchTrack(newLoot);
            DisplayedLootAndItsIcons.Add(newLoot, newIcon);
            LootDisplayListIncreased?.Invoke();
        }

        private LootIcon InstantiateNewIcon()
        {
            return Instantiate(_lootIconPrefab, _iconsParent.transform);
        }

        private void RemoveIcon(PickableLoot retiredLoot)
        {
            Destroy(DisplayedLootAndItsIcons[retiredLoot].gameObject);
            DisplayedLootAndItsIcons.Remove(retiredLoot);
            LootDisplayListDecreased?.Invoke();
        }
    }
}