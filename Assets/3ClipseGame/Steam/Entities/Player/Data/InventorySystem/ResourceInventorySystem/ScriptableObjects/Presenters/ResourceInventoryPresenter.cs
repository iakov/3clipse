using System.Collections.Generic;
using System.Linq;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ScriptableObjects.Resources.Presenters;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.ResourceInventorySystem.ScriptableObjects.Presenters
{
    public class ResourceInventoryPresenter : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private ResourceInventory resourceInventory;
        [SerializeField] private RectTransform panel;
        [SerializeField] private GameObject slotPrefab;

        #endregion

        #region PrivateFields

        private List<ResourceSlotPresenter> _slots = new();

        #endregion

        #region MonoBehaviourMethods

        private void OnEnable()
        {
            resourceInventory.ItemAdded += OnItemAdded;
            UpdateIcons();
        }

        private void OnDisable() => resourceInventory.ItemAdded -= OnItemAdded;

        #endregion

        #region PrivateMethods

        private void OnItemAdded(ResourceSlot resourceSlot)
        {
            ChangeData(resourceSlot);
        }
        
        private void ChangeData(ResourceSlot resourceSlot)
        {
            foreach (var slot in _slots.Where(slot => slot.ResourceSlot == resourceSlot))
            {
                slot.UpdateView();
                if(slot.ResourceSlot.IsEmpty) UpdateIcons();
                return;
            }
            
            _slots.Add(Instantiate(slotPrefab, panel).GetComponent<ResourceSlotPresenter>());
            _slots[^1].ResourceSlot = resourceSlot;
            _slots[^1].UpdateView();
        }

        private void UpdateIcons()
        {
            foreach(var slot in _slots) Destroy(slot.gameObject);
            _slots.Clear();
            foreach (var slot in resourceInventory.Slots)
            {
                _slots.Add(Instantiate(slotPrefab, panel).GetComponent<ResourceSlotPresenter>());
                _slots[^1].ResourceSlot = slot;
                _slots[^1].UpdateView();
            }
        }
        
        #endregion
    }
}
