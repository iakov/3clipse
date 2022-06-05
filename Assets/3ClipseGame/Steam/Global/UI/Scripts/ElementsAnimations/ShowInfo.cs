using System;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ScriptableObjects;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ScriptableObjects.Resources.Presenters;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Global.UI.Scripts.ElementsAnimations
{
    public class ShowInfo : MonoBehaviour, IPointerClickHandler
    {
        #region SerializeFields

        [SerializeField] private Color highlightColor;
        [SerializeField] private Color baseColor;
        [SerializeField] private Image image;

        #endregion

        #region PrivateFields

        private InfoDisplay _displayPanel;
        private ResourceSlotPresenter _presenter;
        private Item _item;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _displayPanel = GetComponentInParent<InfoDisplay>();
            _presenter = GetComponent<ResourceSlotPresenter>();
        }
        private void Start() => _item = _presenter.ResourceSlot.Resource;
        private void OnEnable() => _displayPanel.ItemChanged += ChangeStatus;
        private void OnDisable() => _displayPanel.ItemChanged -= ChangeStatus;

        #endregion

        #region PublicMethods

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_item ==  null) throw new ArgumentException("Slot is empty for some reason");
            _displayPanel.NewItemClicked(_item);
        }

        #endregion

        #region PrivateMethods
        
        private void ChangeStatus(Item item) => image.color = item != _item ? baseColor : highlightColor;

        #endregion
    }
}
