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
        private SelectedLootChaser _lootChaser;

        private LootIcon _currentSelectedLoot;
        
        private void Awake()
        {
            _lootDisplay = GetComponent<LootDisplay>();
            _lootScrollHandler = GetComponent<LootScrollHandler>();
            _lootChaser = GetComponent<SelectedLootChaser>();
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
            SwitchIconToClosest();
        }
        
        private LootIcon GetClosestToCurrentIcon()
        {
            return _lootDisplay.GetPreviousObject(_currentSelectedLoot)
                   ?? _lootDisplay.GetNextObject(_currentSelectedLoot);
        }

        #endregion
        
        private void SwitchCurrentSelectedLoot(LootIcon newIcon)
        {
            ChangeView(newIcon);
        }

        private void ChangeView(LootIcon newIcon)
        {
            var previousLoot = _currentSelectedLoot;
            _currentSelectedLoot = newIcon;
            _lootHighlighter.SwitchHighlightedIcon(previousLoot, newIcon);
            _lootChaser.EditScroll(newIcon);
        }

        private void SwitchIconToClosest()
        {
            var newSelected = GetClosestToCurrentIcon();
            SwitchCurrentSelectedLoot(newSelected);
        }
    }
}
