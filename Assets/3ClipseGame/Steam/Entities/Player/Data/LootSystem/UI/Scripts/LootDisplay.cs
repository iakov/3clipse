using System.Collections.Specialized;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.Detector;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.LootComponent;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.UI.Scripts
{
    [RequireComponent(typeof(LootIconsSelector))]
    
    public class LootDisplay : MonoBehaviour
    {
        #region Public

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

        #endregion

        #region Serialization

        [SerializeField] private LootDetector _lootDetector;
        [SerializeField] private LootIcon _lootIconPrefab;
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

        private LootIcon InitializeIcon(PickableLoot newLoot)
        {
            var newIcon = InstantiateNewIcon();
            newIcon.SwitchTrack(newLoot);
            _displayedLoot.Add(newLoot, newIcon);
            return newIcon;
        }

        private LootIcon InstantiateNewIcon()
        {
            var newObject = Instantiate(_lootIconPrefab.gameObject, _iconsParent.transform);
            return newObject.GetComponent<LootIcon>();
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