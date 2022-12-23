using System;
using _3ClipseGame.Steam.GameMechanics.InventorySystem.ResourceInventorySystem.InGame.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.GameMechanics.InventorySystem.ResourceInventorySystem.UI.Scripts
{
    public class ResourceSlotView : MonoBehaviour
    {
        [SerializeField] private Image _imageComponent;
        [SerializeField] private Text _textComponent;

        private ResourceSlot _currentDisplayedSlot;

        private void Start()
        {
            UpdateView();
        }

        private void OnDisable()
        {
            _currentDisplayedSlot.AmountChanged -= UpdateView;
        }
        
        public void SwitchTrackedSlot(ResourceSlot slot)
        {
            if (_currentDisplayedSlot != null) _currentDisplayedSlot.AmountChanged -= UpdateView;
            
            _currentDisplayedSlot = slot ?? throw new ArgumentException("New tracked slot is null");
            _currentDisplayedSlot.AmountChanged += UpdateView;
            
            UpdateView();
        }

        private void UpdateView()
        {
            if (_currentDisplayedSlot.GetIsEmpty()) return;
            
            _imageComponent.sprite = _currentDisplayedSlot.GetItem().UIImage;
            _textComponent.text = "x" + _currentDisplayedSlot.GetAmount();
        }
    }
}
