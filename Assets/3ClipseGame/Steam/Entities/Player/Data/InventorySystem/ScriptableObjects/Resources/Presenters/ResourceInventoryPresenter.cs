using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ScriptableObjects.Resources.Presenters
{
    public class ResourceInventoryPresenter : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private ResourceInventory resourceInventory;
        [SerializeField] private RectTransform panel;
        [SerializeField] private GameObject slotPrefab;

        #endregion

        #region PrivateFields

        private List<GameObject> _slots = new();

        #endregion

        #region MonoBehaviourMethods

        private void Start() => RefreshIcons();
        private void OnEnable() => resourceInventory.ItemAdded += OnItemAdded;
        private void OnDisable() => resourceInventory.ItemAdded -= OnItemAdded;

        #endregion

        #region PrivateMethods

        private void OnItemAdded(ResourceSlot resourceSlot)
        {
            ChangeData(resourceSlot);
            SortIconsByName();
            ClearIcons();
            RefreshIcons();
        }
        
        private void ChangeData(ResourceSlot resourceSlot)
        {
            ResourceSlotPresenter presenter = null;
            foreach (var slot in _slots)
            {
                presenter = slot.GetComponent<ResourceSlotPresenter>();
                if(presenter.ResourceSlot == resourceSlot) presenter.UpdateView();
            }

            if (presenter == null) throw new ArgumentException("Something went wrong");
        }

        private void SortIconsByName()
        {
            //TODO: Make sort of items in alphabet order
        }

        private void ClearIcons()
        {
            foreach (var slot in _slots) Destroy(slot);
            _slots.Clear();
        }

        private void RefreshIcons()
        {
            for (var i = 0; i < resourceInventory.slotsAmount; i++)
            {
                _slots.Add( Instantiate(slotPrefab, panel));
                var resourceSlot = _slots[i].GetComponent<ResourceSlotPresenter>(); 
                resourceSlot.ResourceSlot = resourceInventory.Slots[i];
            }
        }
        
        #endregion
    }
}
