using System.Collections.Generic;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.Model.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.View.Scripts
{
    public class ResourceInventoryView : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private ResourceInventory _inventoryStorage;
        [SerializeField] private RectTransform _slotViewPrefab;

        #endregion

        #region PrivateFields

        private Dictionary<ResourceSlot, ResourceSlotView> _slots = new();

        #endregion

        #region MonoBehaviourMethods

        private void OnEnable()
        {
            _inventoryStorage.ItemAdded += OnItemAdded;
            foreach (var slot in _inventoryStorage.Slots) OnItemAdded(slot);
        }

        private void OnDisable()
        {
            _inventoryStorage.ItemAdded -= OnItemAdded;
        }

        #endregion

        #region PrivateMethods

        private void OnItemAdded(ResourceSlot resource)
        {
            if (_slots.ContainsKey(resource)) return;

            var newSlotView = Instantiate(_slotViewPrefab, GetComponent<RectTransform>()).GetComponent<ResourceSlotView>();
            newSlotView.SwitchTrackedSlot(resource);
            _slots.Add(resource, newSlotView);
        }

        #endregion
    }
}
