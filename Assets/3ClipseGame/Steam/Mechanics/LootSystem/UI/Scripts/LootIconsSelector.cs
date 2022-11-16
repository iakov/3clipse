using _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.HUDInput;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.UI.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.LootSystem.UI.Scripts
{
    [RequireComponent(typeof(LootDisplay))]
    [RequireComponent(typeof(SelectedLootChaser))]
    
    public class LootIconsSelector : MonoBehaviour
    {
        [SerializeField] private HUDInputProcessor _hudHandler;
        public Entities.Player.Data.LootSystem.UI.Scripts.LootIcon.LootIcon GetCurrentSelectedLoot()
        {
            return _currentSelectedLoot;
        }
        
        private LootDisplay _lootDisplay;
        private LootHighlighter _lootHighlighter;
        private SelectedLootChaser _lootChaser;

        private Entities.Player.Data.LootSystem.UI.Scripts.LootIcon.LootIcon _currentSelectedLoot;
        
        private void Awake()
        {
            _lootDisplay = GetComponent<LootDisplay>();
            _lootChaser = GetComponent<SelectedLootChaser>();
            _lootHighlighter = new LootHighlighter();
        }
        
        private void OnEnable()
        {
            _hudHandler.LootScrolled += OnLootScrolled;
        }

        private void OnDisable()
        {
            _hudHandler.LootScrolled -= OnLootScrolled;
        }

        #region LootListIncreasedHandler

        public void SelectIconIfFirst(Entities.Player.Data.LootSystem.UI.Scripts.LootIcon.LootIcon icon)
        {
            if (_currentSelectedLoot != null) return;

            SwitchCurrentSelectedLoot(icon);
        }

        #endregion

        #region LootListDecreasedHandler

        public void ChangeSelectedIconIfDeleting(Entities.Player.Data.LootSystem.UI.Scripts.LootIcon.LootIcon retiringIcon)
        {
            if (_currentSelectedLoot == retiringIcon) 
                SwitchIconToClosest();
        }
        
        private Entities.Player.Data.LootSystem.UI.Scripts.LootIcon.LootIcon GetClosestToCurrentIcon()
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
        
        private void SwitchCurrentSelectedLoot(Entities.Player.Data.LootSystem.UI.Scripts.LootIcon.LootIcon newIcon)
        {
            ChangeView(newIcon);
        }

        private void ChangeView(Entities.Player.Data.LootSystem.UI.Scripts.LootIcon.LootIcon newIcon)
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
