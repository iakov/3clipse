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

        private List<ResourceSlotPresenter> _slots = new();

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
        }
        
        private void ChangeData(ResourceSlot resourceSlot)
        {
            foreach (var slot in _slots.Where(slot => slot.ResourceSlot == resourceSlot))
            {
                slot.UpdateView();
                return;
            }
            
            _slots.Add(Instantiate(slotPrefab, panel).GetComponent<ResourceSlotPresenter>());
            _slots[^1].ResourceSlot = resourceSlot;
            _slots[^1].UpdateView();
        }

        private void RefreshIcons()
        {
            for (var i = 0; i < resourceInventory.Slots.Count; i++)
            {
                _slots.Add( Instantiate(slotPrefab, panel).GetComponent<ResourceSlotPresenter>());
                _slots[i].ResourceSlot = resourceInventory.Slots[i];
            }
        }
        
        #endregion
    }
}
