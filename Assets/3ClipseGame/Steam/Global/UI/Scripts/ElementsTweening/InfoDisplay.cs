using System;
using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Global.UI.Scripts.ElementsTweening
{
    public class InfoDisplay : MonoBehaviour
    {
        #region PublicFields

        public event Action<Item> ItemChanged; 

        #endregion

        #region SerializeFields

        [Header("Display info")]
        [SerializeField] private Image image;
        [SerializeField] private Text nameText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private RectTransform rightPanel;

        [Header("Animate parameters")] 
        [SerializeField] private float animationTime;

        #endregion

        #region PrivateFields

        private bool _isPanelShowed;
        private Item _currentItem;

        #endregion

        #region MonoBehaviourMethods

        private void OnDisable()
        {
            rightPanel.LeanMoveX(rightPanel.sizeDelta.x / 2, 0f);
            _isPanelShowed = false;
            _currentItem = null;
            ItemChanged?.Invoke(null);
        }

        #endregion

        #region PublicMethods

        public void NewItemClicked(Item item)
        {
            if (_currentItem == item) return;
            
            if (!_isPanelShowed)
            {
                rightPanel.LeanMoveX(rightPanel.sizeDelta.x / -2, animationTime);
                _isPanelShowed = true;
            }
            
            image.sprite = item.UIImage;
            nameText.text = item.Name;
            descriptionText.text = item.Description;
            ItemChanged?.Invoke(item);
        }

        #endregion
    }
}
