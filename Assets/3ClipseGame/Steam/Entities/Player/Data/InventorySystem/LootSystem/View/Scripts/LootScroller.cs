using System;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts
{
    public class LootScroller : MonoBehaviour
    {
        public PickableLoot CurrentSelectedLoot { get; private set; }

        [Header("Selection")]
        [SerializeField] private InputAction _scrollAction;
        [Header("Slide view")]
        [SerializeField] private Scrollbar _scrollbar;
        [SerializeField] private RectTransform _lootIcon;
        
        private LootDisplay _lootDisplay;
        private VerticalLayoutGroup _verticalLayout;
        private ScrollRect _scrollRect;
        
        private void Awake()
        {
            _scrollAction.Enable();
            
            _lootDisplay = GetComponent<LootDisplay>();
            _verticalLayout = GetComponent<VerticalLayoutGroup>();
            _scrollRect = GetComponentInParent<ScrollRect>();
        }
        
        private void OnEnable()
        {
            EnableLootDisplayEvents();
            EnableScrollEvents();
        }

        private void EnableLootDisplayEvents()
        {
            _lootDisplay.LootDisplayListIncreased += OnLootDisplayListIncreased;
            _lootDisplay.LootDisplayListDecreased += OnLootDisplayListDecreased;
        }

        private void EnableScrollEvents()
        {
            _scrollAction.started += OnScroll;
        }
        
        private void OnDisable()
        {
            DisableLootDisplayEvents();
            DisableScrollEvents();
        }
        
        private void DisableLootDisplayEvents()
        {
            _lootDisplay.LootDisplayListIncreased -= OnLootDisplayListIncreased;
            _lootDisplay.LootDisplayListDecreased -= OnLootDisplayListDecreased;
        }
        
        private void DisableScrollEvents()
        {
            _scrollAction.started -= OnScroll;
        }

        private void OnLootDisplayListIncreased(PickableLoot newLoot)
        {
            CurrentSelectedLoot = newLoot;
        }

        private void OnLootDisplayListDecreased(PickableLoot retiredLoot)
        {
            //When decreasING set highlight to another object
        }
        
        private void OnScroll(InputAction.CallbackContext context)
        {
            var newSelected = GetSelectedAfterScroll(context.ReadValue<float>());
            SwitchCurrentSelectedLoot(newSelected);
            CorrectScroll();
        }
        
        private PickableLoot GetSelectedAfterScroll(float value)
        {
            var nextLootObject = value > 0
                ? _lootDisplay.GetPreviousLootObject(CurrentSelectedLoot)
                : _lootDisplay.GetNextLootObject(CurrentSelectedLoot);

            return nextLootObject;
        }

        private void SwitchCurrentSelectedLoot(PickableLoot newLoot)
        {
            if(newLoot == null) return;
            SwitchHighlight(newLoot);
            CurrentSelectedLoot = newLoot;
        }

        private void SwitchHighlight(PickableLoot newLoot)
        {
            SwitchCurrentHighlight(CurrentSelectedLoot,false);
            SwitchCurrentHighlight(newLoot,true);
        }
        
        private void SwitchCurrentHighlight(PickableLoot loot, bool isActive)
        {
            if (loot == null) return;
            var currentLootIcon = _lootDisplay.GetIconByObject(loot);
            if (currentLootIcon != null) currentLootIcon.SetActive(isActive);
        }
        
        private void CorrectScroll()
        {
            //Set scroll
        }
    }
}
