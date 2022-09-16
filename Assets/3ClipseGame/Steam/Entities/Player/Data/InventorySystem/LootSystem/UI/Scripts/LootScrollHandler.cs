using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.UI.Scripts
{
    [RequireComponent(typeof(LootIconsSelector))]
    [RequireComponent(typeof(LootDisplay))]

    public class LootScrollHandler : MonoBehaviour
    {
        #region Public

        public event Action<LootIcon> Scrolled;

        #endregion

        #region Serialization

        [SerializeField] private InputAction _scrollAction;

        #endregion
        
        #region Initialization

        private LootDisplay _lootDisplay;
        private LootIconsSelector _lootIconsSelector;

        private void Awake()
        {
            _lootDisplay = GetComponent<LootDisplay>();
            _lootIconsSelector = GetComponent<LootIconsSelector>();

            _scrollAction.Enable();
        }
        
        #endregion

        #region EventsSubscription

        private void OnEnable()
        {
            EnableScrollEvents();
        }

        private void OnDisable()
        {
            DisableScrollEvents();
        }

        private void EnableScrollEvents()
        {
            _scrollAction.started += OnScroll;
        }

        private void DisableScrollEvents()
        {
            _scrollAction.started -= OnScroll;
        }

        #endregion

        #region ScrollHandler
        
        private void OnScroll(InputAction.CallbackContext context)
        {
            var newSelected = GetNewSelected(context);
            InvokeEventIfNewSelectedIsDifferent(newSelected);
        }

        #endregion
        
        private void InvokeEventIfNewSelectedIsDifferent(LootIcon newSelected)
        {
            if (newSelected != _lootIconsSelector.GetCurrentSelectedLoot())
            {
                Scrolled?.Invoke(newSelected);
            }
        }

        private LootIcon GetNewSelected(InputAction.CallbackContext context)
        {
            var scrollValue = context.ReadValue<float>();
            return GetSelectedAfterScroll(scrollValue);
        }

        private LootIcon GetSelectedAfterScroll(float scrollValue)
        {
            var nextLootObject = GetNewIcon(scrollValue);
            nextLootObject ??= _lootIconsSelector.GetCurrentSelectedLoot();

            return nextLootObject;
        }

        private LootIcon GetNewIcon(float scrollValue)
        {
            var currentSelectedSlot = _lootIconsSelector.GetCurrentSelectedLoot();
            if (currentSelectedSlot == null) return null;
            
            return scrollValue > 0
                ? _lootDisplay.GetPreviousObject(currentSelectedSlot)
                : _lootDisplay.GetNextObject(currentSelectedSlot);
        }
    }
}