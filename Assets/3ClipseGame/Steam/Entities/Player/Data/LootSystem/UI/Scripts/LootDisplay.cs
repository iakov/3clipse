using System;
using System.Collections.Specialized;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.Detector;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.UI.Scripts
{
    [RequireComponent(typeof(LootIconsSelector))]
    
    public class LootDisplay : MonoBehaviour
    {
        #region Public

        public LootIcon.LootIcon GetPreviousObject(LootIcon.LootIcon current)
        {
            var currentIndex = GetIndexWithException(current);
            
            var isFirst = currentIndex == 0;
            var isException = currentIndex < 0;
            
            return  isFirst || isException
                ? GetIconByIndex(currentIndex) 
                : GetIconByIndex(currentIndex - 1);
        }

        public LootIcon.LootIcon GetNextObject(LootIcon.LootIcon current)
        {
            var currentIndex = GetIndexWithException(current);

            var isLast = currentIndex == _displayedLoot.Count - 1;
            var isException = currentIndex < 0;
            
            return isLast || isException
                ? GetIconByIndex(currentIndex)
                : GetIconByIndex(currentIndex + 1);
        }

        public LootIcon.LootIcon GetIconByObject(PickableLoot loot)
        {
            try
            {
                return _displayedLoot[loot] as LootIcon.LootIcon;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return null;
            }
        }

        public LootIcon.LootIcon GetIconByIndex(int index)
        {
            if (index >= _displayedLoot.Count) return _displayedLoot[_displayedLoot.Count - 1] as LootIcon.LootIcon;
            if (_displayedLoot.Count == 0) return null;
            if (index < 0 && _displayedLoot.Count > 0) return _displayedLoot[0] as LootIcon.LootIcon;
            return _displayedLoot[index] as LootIcon.LootIcon;
        }

        #endregion

        #region Serialization

        [SerializeField] private LootDetector _lootDetector;
        [SerializeField] private GameObject _lootIconPrefab;
        [SerializeField] private VerticalLayoutGroup _iconsParent;
        
        #endregion

        #region Initialization

        private OrderedDictionary _displayedLoot;
        private LootIconsSelector _lootIconsSelector;

        private void Awake()
        {
            _displayedLoot = new OrderedDictionary();
            _lootIconsSelector = GetComponent<LootIconsSelector>();
        }

        #endregion

        #region EventsSubscription

        private void OnEnable()
        {
            _lootDetector.LootDetected += AddIcon;
            _lootDetector.LootRetired += RemoveIcon;
        }

        private void OnDisable()
        {
            _lootDetector.LootDetected -= AddIcon;
            _lootDetector.LootRetired -= RemoveIcon;
        }

        #endregion

        #region NewLootDetectedHandler

        private void AddIcon(PickableLoot newLoot)
        {
            var newIcon = InitializeIcon(newLoot);
            _lootIconsSelector.SelectIconIfFirst(newIcon);
        }

        private LootIcon.LootIcon InitializeIcon(PickableLoot newLoot)
        {
            var newIcon = InstantiateNewIcon();
            newIcon.SwitchTrack(newLoot);
            _displayedLoot.Add(newLoot, newIcon);
            return newIcon;
        }

        private LootIcon.LootIcon InstantiateNewIcon()
        {
            var newObject = Instantiate(_lootIconPrefab, _iconsParent.transform);
            return newObject.GetComponent<LootIcon.LootIcon>();
        }

        #endregion

        #region LootRetiredHandler

        private void RemoveIcon(PickableLoot retiredLoot)
        {
            var retiringIcon = GetIconByObject(retiredLoot);
            _lootIconsSelector.ChangeSelectedIconIfDeleting(retiringIcon);
            DeleteIcon(retiredLoot);
        }

        private void DeleteIcon(PickableLoot retiredLoot)
        {
            var icon = GetIconByObject(retiredLoot);
            _displayedLoot.Remove(retiredLoot);
            Destroy(icon.gameObject);
        }

        #endregion

        private int GetIndexWithException(LootIcon.LootIcon icon)
        {
            var currentIndex = FindIconsIndex(icon);
            return currentIndex;
        }

        private int FindIconsIndex(LootIcon.LootIcon icon)
        {
            for (var i = 0; i < _displayedLoot.Count; i++)
            {
                var currentEnumeratorIcon = GetIconByIndex(i);
                if (AreEqual(icon, currentEnumeratorIcon)) return i;
            }

            return -1;
        }

        private bool AreEqual(LootIcon.LootIcon first, LootIcon.LootIcon second)
        {
            return first == second;
        }
    }
}