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

        #endregion

        #region MonoBehaviourMethods

        private void OnEnable()
        {
            Slots ??= new List<ResourceSlot>();
        }

        #endregion

        #region PublicMethods

        public bool AddItem(Resource item, int amount, out int amountLeft)
        {
            amountLeft = amount;

            if (!TryFindResourceSlot(item, out var itemSlot)) itemSlot = AddResourceSlot(item, amount, out amountLeft);
            else itemSlot.AddAmount(amount, out amountLeft);

            ItemAdded?.Invoke(itemSlot);

            return amountLeft == 0;
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
