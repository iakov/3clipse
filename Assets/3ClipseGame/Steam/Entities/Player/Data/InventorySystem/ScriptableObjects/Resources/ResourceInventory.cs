using System;
using System.Collections.Generic;
using System.Linq;
using _3ClipseGame.Steam.Entities.Player.Data.Inventory.Scripts.ScriptableObjects.Resources.Item;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ScriptableObjects.Resources
{
    [CreateAssetMenu(fileName = "New Resource Inventory", menuName = "Inventory/Resources/Resource Inventory")]
    public class ResourceInventory : ScriptableObject
    {
        #region PublicFields

        public List<ResourceSlot> Slots;
        public event Action<ResourceSlot> ItemAdded;
        public int slotsAmount;

        #endregion

        #region MonoBehaviourMethods

        private void OnEnable()
        {
            Slots ??= new List<ResourceSlot>();
            while (Slots.Count != slotsAmount) Slots.Add(new ResourceSlot());
        }

        #endregion

        #region PublicMethods

        public bool AddItem(Resource item, int amount, out int amountLeft)
        {
            amountLeft = amount;
            
            while (amountLeft != 0)
            {
                if (!TryFindResourceSlot(item, out var itemSlot) & !TryFindEmptySlot(out var emptySlot)) return false;
                var currentSlot = itemSlot ?? emptySlot;
                currentSlot.Resource = item;
                currentSlot.AddAmount(amountLeft, out amountLeft);
                ItemAdded?.Invoke(currentSlot);
            }
            
            return true;
        }

        public bool TryFindResourceSlot(Resource resource, out ResourceSlot slotPresenter)
        {
            foreach (var slot in Slots.Where(slot => slot.Resource == resource && !slot.IsFull))
            {
                slotPresenter = slot;
                return true;
            }

            slotPresenter = null;
            return false;
        }

        public bool TryFindEmptySlot(out ResourceSlot slotPresenter)
        { 
            foreach (var slot in Slots.Where(slot => slot.IsEmpty))
            {
                slotPresenter = slot;
                return true;
            }

            slotPresenter = null;
            return false;
        }

        #endregion
    }
}
