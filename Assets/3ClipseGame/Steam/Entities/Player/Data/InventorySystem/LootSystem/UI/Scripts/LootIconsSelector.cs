using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.UI.Scripts
{
    [RequireComponent(typeof(LootScrollHandler))]
    [RequireComponent(typeof(LootDisplay))]
    [RequireComponent(typeof(SelectedLootChaser))]
    
    public class LootIconsSelector : MonoBehaviour
    {
        #region Public

        public LootIcon GetCurrentSelectedLoot()
        {
            return _currentSelectedLoot;
        }

        #endregion

        #region Initialiation

        private LootDisplay _lootDisplay;
        private LootScrollHandler _lootScrollHandler;
        private LootHighlighter _lootHighlighter;
        private SelectedLootChaser _selectedLootChaser;

        private LootIcon _currentSelectedLoot;
        
        private void Awake()
        {
            _lootDisplay = GetComponent<LootDisplay>();
            _lootScrollHandler = GetComponent<LootScrollHandler>();
            _selectedLootChaser = GetComponent<SelectedLootChaser>();
            _lootHighlighter = new LootHighlighter(this);
        }

        #endregion

        #region EventsSubscription

        private void OnEnable()
        {
            _lootScrollHandler.Scrolled += SwitchCurrentSelectedLoot;
        }

        private void OnDisable()
        {
            _lootScrollHandler.Scrolled -= SwitchCurrentSelectedLoot;
        }

        #endregion

        #region LootListIncreasedHandler

        public void SelectIconIfFirst(LootIcon icon)
        {
            if (_currentSelectedLoot != null) return;
            
            SwitchCurrentSelectedLoot(icon);
        }

        #endregion

        #region LootListDecreasedHandler

        public void ChangeSelectedIconIfDeleting(LootIcon retiringIcon)
        {
            if (_currentSelectedLoot != retiringIcon) return;
            
            var newSelected = GetClosestToCurrentIcon();
            SwitchCurrentSelectedLoot(newSelected);
        }
        
        private LootIcon GetClosestToCurrentIcon()
        {
            return _lootDisplay.GetPreviousObject(_currentSelectedLoot)
                   ?? _lootDisplay.GetNextObject(_currentSelectedLoot);
        }

        #endregion
        
        private void SwitchCurrentSelectedLoot(LootIcon newLoot)
        {
            var previousLoot = _currentSelectedLoot;
            _currentSelectedLoot = newLoot;
            _lootHighlighter.SwitchHighlightedIcon(previousLoot, newLoot);
            _selectedLootChaser.EditScroll(newLoot);
        }
    }
}
