using System;
using System.Collections.Generic;
using System.Linq;
using _3ClipseGame.Steam.Mechanics.InventorySystem.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.InventorySystem.ResourceInventorySystem.InGame.Scripts
{
    [CreateAssetMenu(fileName = "New Resource Inventory", menuName = "Inventory/Resources/Resource Inventory")]
    public class ResourceInventory : ItemInventory<Resource, ResourceSlot>
    {
        public override event Action<ResourceSlot> SlotChanged;
        private List<ResourceSlot> _slots;
        
        private void OnEnable()
        {
            _slots ??= new List<ResourceSlot>();
        }
        
        public override IEnumerable<ResourceSlot> GetSlots()
        {
            return _slots.AsEnumerable();
        }
        
        public override bool TryRemoveItem(Resource item, int amount = 1)
        {
            var resourceSlot = _slots.Find(slot => slot.GetItem() == item);
            if (!resourceSlot.TryTakeAmount(amount)) return false;
            if (!resourceSlot.GetIsEmpty()) return true;
            _slots.Remove(resourceSlot);
            SlotChanged?.Invoke(resourceSlot);
            return true;
        }

        public override bool TryAddItem(Resource item, int amount = 1)
        {
            int amountLeft;
            
            if (!TryFindResourceSlot(item, out var itemSlot)) itemSlot = AddResourceSlot(item, amount, out amountLeft);
            else itemSlot.AddAmount(amount, out amountLeft);

            SlotChanged?.Invoke(itemSlot);

            return amountLeft == 0;
        }

        private bool TryFindResourceSlot(Resource resource, out ResourceSlot slotPresenter)
        {
            foreach (var slot in _slots.Where(slot => slot.GetItem() == resource))
            {
                slotPresenter = slot;
                return true;
            }

            slotPresenter = null;
            return false;
        }

        private ResourceSlot AddResourceSlot(Resource item, int amount, out int amountLeft)
        {
            var newSlot = new ResourceSlot(item);
            newSlot.AddAmount(amount, out amountLeft);
            _slots.Add(newSlot);
            return newSlot;
        }
    }
}
