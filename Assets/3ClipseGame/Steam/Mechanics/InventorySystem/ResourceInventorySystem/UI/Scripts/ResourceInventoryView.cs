using System.Collections.Generic;
using _3ClipseGame.Steam.Mechanics.InventorySystem.ResourceInventorySystem.InGame.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.InventorySystem.ResourceInventorySystem.UI.Scripts
{
    public class ResourceInventoryView : MonoBehaviour
    {
        [SerializeField] private ResourceInventory _inventoryStorage;
        [SerializeField] private RectTransform _slotViewPrefab;

        private Dictionary<ResourceSlot, ResourceSlotView> _slots = new();

        private void OnEnable()
        {
            _inventoryStorage.SlotChanged += OnSlotChanged;
            foreach (var slot in _inventoryStorage.GetSlots()) OnSlotChanged(slot);
        }

        private void OnDisable()
        {
            _inventoryStorage.SlotChanged -= OnSlotChanged;
        }

        private void OnSlotChanged(ResourceSlot resource)
        {
            if (_slots.ContainsKey(resource)) return;

            var newSlotView = Instantiate(_slotViewPrefab, GetComponent<RectTransform>()).GetComponent<ResourceSlotView>();
            newSlotView.SwitchTrackedSlot(resource);
            _slots.Add(resource, newSlotView);
        }
    }
}
