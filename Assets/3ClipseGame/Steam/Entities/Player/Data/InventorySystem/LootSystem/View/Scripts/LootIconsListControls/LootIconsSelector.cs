using System;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts.LootIconsListControls
{
    [RequireComponent(typeof(LootScroller))]
    [RequireComponent(typeof(LootHighlighter))]
    public class LootIconsSelector : MonoBehaviour
    {
        public PickableLoot CurrentSelectedLoot { get; private set; }
        public event Action<PickableLoot> SelectedLootChanged;

        [SerializeField] private InputAction _scrollAction;

        private LootDisplay _lootDisplay;

        private void Awake()
        {
            _lootDisplay = GetComponent<LootDisplay>();
            _scrollAction.Enable();
        }
        
        private void OnEnable()
        {
            EnableLootDisplayEvents();
            EnableScrollEvents();
        }

        private void OnDisable()
        {
            DisableLootDisplayEvents();
            DisableScrollEvents();
        }

        private void EnableLootDisplayEvents()
        {
            _lootDisplay.LootDisplayListIncreased += OnLootDisplayListIncreased;
            _lootDisplay.LootDisplayListDecreasing += OnLootDisplayListDecreasing;
        }
        
        private void DisableLootDisplayEvents()
        {
            _lootDisplay.LootDisplayListIncreased -= OnLootDisplayListIncreased;
            _lootDisplay.LootDisplayListDecreasing -= OnLootDisplayListDecreasing;
        }
        
        private void EnableScrollEvents()
        {
            _scrollAction.started += OnScroll;
        }
        
        private void DisableScrollEvents()
        {
            _scrollAction.started -= OnScroll;
        }

        private void OnLootDisplayListIncreased(PickableLoot newLoot)
        {
            if (CurrentSelectedLoot == null)
            {
                SwitchCurrentSelectedLoot(newLoot);
            }
        }

        private void OnLootDisplayListDecreasing(PickableLoot retiredLoot)
        {
            if (CurrentSelectedLoot == retiredLoot)
            {
                var newSelected = GetClosestToCurrentIcon();
                SwitchCurrentSelectedLoot(newSelected);
            }
        }

        private PickableLoot GetClosestToCurrentIcon()
        {
            return _lootDisplay.GetPreviousLootObject(CurrentSelectedLoot) 
                   ?? _lootDisplay.GetNextLootObject(CurrentSelectedLoot);
        }

        private void OnScroll(InputAction.CallbackContext context)
        {
            var newSelected = GetNewSelected(context);
            SwitchCurrentSelectedLoot(newSelected);
        }

        private PickableLoot GetNewSelected(InputAction.CallbackContext context)
        {
            var scrollValue = context.ReadValue<float>();
            return GetSelectedAfterScroll(scrollValue);
        }
        
        private PickableLoot GetSelectedAfterScroll(float scrollValue)
        {
            var nextLootObject = scrollValue > 0
                ? _lootDisplay.GetPreviousLootObject(CurrentSelectedLoot)
                : _lootDisplay.GetNextLootObject(CurrentSelectedLoot);

            nextLootObject ??= CurrentSelectedLoot;

            return nextLootObject;
        }

        private void SwitchCurrentSelectedLoot(PickableLoot newLoot)
        {
            var previousLoot = CurrentSelectedLoot;
            CurrentSelectedLoot = newLoot;
            SelectedLootChanged?.Invoke(previousLoot);
        }
    }
}
