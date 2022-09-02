using System;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.Model.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.View.Scripts
{
    public class ResourceSlotView : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private Image _imageComponent;
        [SerializeField] private Text _textComponent;

        #endregion

        #region PrivateFields

        private ResourceSlot _currentDisplayedSlot;

        #endregion

        #region MonoBehaviourMethods

        private void Start()
        {
            UpdateView();
        }

        private void OnDisable()
        {
            _currentDisplayedSlot.AmountChanged -= UpdateView;
        }

        #endregion

        #region PublicMethods

        public void SwitchTrackedSlot(ResourceSlot slot)
        {
            if (_currentDisplayedSlot != null) _currentDisplayedSlot.AmountChanged -= UpdateView;
            
            _currentDisplayedSlot = slot ?? throw new ArgumentException("New tracked slot is null");
            _currentDisplayedSlot.AmountChanged += UpdateView;
            
            UpdateView();
        }

        #endregion

        #region PrivateMethods

        private void UpdateView()
        {
            if (_currentDisplayedSlot.IsEmpty) return;
            
            _imageComponent.sprite = _currentDisplayedSlot.Resource.UIImage;
            _textComponent.text = "x" + _currentDisplayedSlot.CurrentAmount;
        }

        #endregion
    }
}
