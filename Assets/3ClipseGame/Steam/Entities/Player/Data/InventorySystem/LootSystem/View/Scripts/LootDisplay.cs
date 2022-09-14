using System;
using System.Collections.Specialized;
using System.Linq;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Detector;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts
{
    public class LootDisplay : MonoBehaviour
    {
        public event Action<PickableLoot> LootDisplayListDecreasing;
        public event Action<PickableLoot> LootDisplayListDecreased;

        public event Action<PickableLoot> LootDisplayListIncreasing;
        public event Action<PickableLoot> LootDisplayListIncreased;

        [SerializeField] private LootDetector _lootDetector;
        [SerializeField] private LootIcon _lootIconPrefab;
        [SerializeField] private VerticalLayoutGroup _iconsParent;

        private OrderedDictionary _displayedLoot = new();

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

        public LootIcon GetPreviousObject(LootIcon current)
        {
            var currentIndex = GetIndexWithException(current);
            
            var isFirst = currentIndex == 0;
            var isException = currentIndex < 0;
            
            return  isFirst || isException
                ? null 
                : GetIconByIndex(currentIndex - 1);
        }

        public LootIcon GetNextObject(LootIcon current)
        {
            var currentIndex = GetIndexWithException(current);

            var isLast = currentIndex == _displayedLoot.Count - 1;
            var isException = currentIndex < 0;
            
            return isLast || isException
                ? null
                : GetIconByIndex(currentIndex + 1);
        }

        public LootIcon GetIconByObject(PickableLoot loot)
        {
            return _displayedLoot[loot] as LootIcon;
        }

        public LootIcon GetIconByIndex(int index)
        {
            return _displayedLoot[index] as LootIcon;
        }

        private void AddNewIcon(PickableLoot newLoot)
        {
            LootDisplayListIncreasing?.Invoke(newLoot);
            InitializeIcon(newLoot);
            LootDisplayListIncreased?.Invoke(newLoot);
        }

        private void InitializeIcon(PickableLoot newLoot)
        {
            var newIcon = InstantiateNewIcon();
            newIcon.SwitchTrack(newLoot);
            _displayedLoot.Add(newLoot, newIcon);
        }

        private LootIcon InstantiateNewIcon()
        {
            var newObject = Instantiate(_lootIconPrefab.gameObject, _iconsParent.transform);
            return newObject.GetComponent<LootIcon>();
        }

        private void RemoveIcon(PickableLoot retiredLoot)
        {
            LootDisplayListDecreasing?.Invoke(retiredLoot);
            DeleteIcon(retiredLoot);
            LootDisplayListDecreased?.Invoke(retiredLoot);
        }

        private void DeleteIcon(PickableLoot retiredLoot)
        {
            var icon = GetIconByObject(retiredLoot);
            _displayedLoot.Remove(retiredLoot);
            Destroy(icon.gameObject);
        }

        private int GetIndexWithException(LootIcon icon)
        {
            var currentIndex = FindIconsIndex(icon);
            CatchIndexException(currentIndex);
            return currentIndex;
        }

        private int FindIconsIndex(LootIcon icon)
        {
            for (var i = 0; i < _displayedLoot.Count; i++)
            {
                var currentEnumeratorIcon = GetIconByIndex(i);
                if (AreEqual(icon, currentEnumeratorIcon)) return i;
            }

            return -1;
        }

        private void CatchIndexException(int index)
        {
            if(index == -1)
            {
                Debug.LogWarning("Trying to find non-existing in this context icon");
            }
        }

        private bool AreEqual(LootIcon first, LootIcon second)
        {
            return first == second;
        }
    }
}