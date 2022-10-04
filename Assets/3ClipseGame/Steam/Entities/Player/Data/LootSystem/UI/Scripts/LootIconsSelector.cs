using _3ClipseGame.Steam.Core.GameStates.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.UI.Scripts
{
    [RequireComponent(typeof(LootDisplay))]
    [RequireComponent(typeof(SelectedLootChaser))]
    
    public class LootIconsSelector : MonoBehaviour
    {
        #region Public

        public LootIcon.LootIcon GetCurrentSelectedLoot()
        {
            return _currentSelectedLoot;
        }

        #endregion
        
        #region Initialiation

        private LootDisplay _lootDisplay;
        private LootHighlighter _lootHighlighter;
        private SelectedLootChaser _lootChaser;

        private LootIcon.LootIcon _currentSelectedLoot;
        
        private void Awake()
        {
            _lootDisplay = GetComponent<LootDisplay>();
            _lootChaser = GetComponent<SelectedLootChaser>();
            _lootHighlighter = new LootHighlighter();
        }

        #endregion

        #region EventsSubscription

        private void OnEnable()
        {
            Game.Instance.HUDInputHandler.LootScrolled += OnLootScrolled;
        }

        private void OnDisable()
        {
            Game.Instance.HUDInputHandler.LootScrolled -= OnLootScrolled;
        }

        #endregion

        #region LootListIncreasedHandler

        public void SelectIconIfFirst(LootIcon.LootIcon icon)
        {
            if (_currentSelectedLoot != null) return;

            SwitchCurrentSelectedLoot(icon);
        }

        #endregion

        #region LootListDecreasedHandler

        public void ChangeSelectedIconIfDeleting(LootIcon.LootIcon retiringIcon)
        {
            if (_currentSelectedLoot == retiringIcon) 
                SwitchIconToClosest();
        }
        
        private LootIcon.LootIcon GetClosestToCurrentIcon()
        {
            var closestIcon = _lootDisplay.GetPreviousObject(_currentSelectedLoot);
            if (closestIcon == _currentSelectedLoot)
                closestIcon = _lootDisplay.GetNextObject(_currentSelectedLoot);

            return closestIcon;
        }

        #endregion

        private void OnLootScrolled(float scrollValue)
        {
            var newIcon = scrollValue < 0f 
                ? _lootDisplay.GetNextObject(_currentSelectedLoot) 
                : _lootDisplay.GetPreviousObject(_currentSelectedLoot);
            
            SwitchCurrentSelectedLoot(newIcon);
        }
        
        private void SwitchCurrentSelectedLoot(LootIcon.LootIcon newIcon)
        {
            ChangeView(newIcon);
        }

        private void ChangeView(LootIcon.LootIcon newIcon)
        {
            var previousLoot = _currentSelectedLoot;
            _currentSelectedLoot = newIcon;
            _lootHighlighter.SwitchHighlightedIcon(previousLoot, _currentSelectedLoot);
            _lootChaser.EditScroll(newIcon);
        }

        private void SwitchIconToClosest()
        {
            var newSelected = GetClosestToCurrentIcon();
            SwitchCurrentSelectedLoot(newSelected);
        }
    }
}
