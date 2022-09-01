using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.Model.Scripts
{
    [CreateAssetMenu(fileName = "New Resource Inventory", menuName = "Inventory/Resources/Resource Inventory")]
    public class ResourceInventory : ScriptableObject
    {
        #region PublicFields

        public List<ResourceSlot> Slots;
        public event Action<ResourceSlot> ItemAdded;

        #endregion

        #region MonoBehaviourMethods

        private void OnEnable()
        {
            Slots ??= new List<ResourceSlot>();
        }

        #endregion

        #region PublicMethods

        public bool RemoveItem(Resource item, int amount)
        {
            var resourceSlot = Slots.Find(slot => slot.Resource == item);
            if (!resourceSlot.TryTakeAmount(amount)) return false;
            if (!resourceSlot.IsEmpty) return true;
            Slots.Remove(resourceSlot);
            ItemAdded?.Invoke(resourceSlot);
            return true;
        }

        public void AddItem(Resource item, int amount)
        {
            int amountLeft;
            
            if (!TryFindResourceSlot(item, out var itemSlot)) itemSlot = AddResourceSlot(item, amount, out amountLeft);
            else itemSlot.AddAmount(amount, out amountLeft);

            ItemAdded?.Invoke(itemSlot);

            if (amountLeft == 0) return;
            
            //Send to storage on campfire
            Debug.Log("New " + amountLeft + " " + item.name + " added to remote storage");
        }

        private bool TryFindResourceSlot(Resource resource, out ResourceSlot slotPresenter)
        {
            foreach (var slot in Slots.Where(slot => slot.Resource == resource))
            {
                slotPresenter = slot;
                return true;
            }

            slotPresenter = null;
            return false;
        }

        private ResourceSlot AddResourceSlot(Resource item, int amount, out int amountLeft)
        {
            var newSlot = new ResourceSlot();
            newSlot.Resource = item;
            newSlot.AddAmount(amount, out amountLeft);
            Slots.Add(newSlot);
            return newSlot;
        }

        #endregion
    }
}
