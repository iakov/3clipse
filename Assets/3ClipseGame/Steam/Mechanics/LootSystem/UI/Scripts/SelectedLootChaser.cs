using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Mechanics.LootSystem.UI.Scripts
{
    [RequireComponent(typeof(LootIconsSelector))]
    [RequireComponent(typeof(LootIconsSelector))]
    
    public class SelectedLootChaser : MonoBehaviour
    {
        #region Initialization

        [Header("Components")] 
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Mask _viewport;
        [SerializeField] private Scrollbar _scrollbar;
        
        [Header("Prefabs")]
        [SerializeField] private RectTransform _lootIcon;
        
        private VerticalLayoutGroup _verticalLayout;
        
        private void Awake()
        {
            _verticalLayout = GetComponent<VerticalLayoutGroup>();
        }

        #endregion

        #region Public

        public void EditScroll(LootIcon.LootIcon currentIcon)
        {
            StartCoroutine(TryScrollWithDelay(currentIcon));
        }

        #endregion

        #region ScrollDown
        
        private IEnumerator TryScrollWithDelay(LootIcon.LootIcon currentIcon)
        {
            yield return null;
            
            ScrollDownIfNeeded(currentIcon);
            ScrollUpIfNeeded(currentIcon);
        }
        
        private void ScrollDownIfNeeded(LootIcon.LootIcon currentIcon)
        {
            try
            {
                TryScrollDown(currentIcon);
            }
            catch (Exception e)
            {
                //ignored
            }
        }

        private void TryScrollDown(LootIcon.LootIcon currentIcon)
        {
            var currentIconLowerBound = GetIconLowerBound(currentIcon);
            var viewportLowerBound = GetViewportLowerBound();

            ScrollDownIfRequired(currentIconLowerBound, viewportLowerBound);
        }
        
        private float GetIconLowerBound(LootIcon.LootIcon icon)
        {
            var iconCenter = GetVerticalIconCenter(icon);
            var iconHeight = GetIconHeight();
            return iconCenter + iconHeight / 2;
        }

        private float GetViewportLowerBound()
        {
            return GetContentUpperBound() + GetCurrentLootDisplayHeight();
        }
        
        private float GetContentUpperBound()
        {
            var verticalLayoutTransform = _verticalLayout.GetComponent<RectTransform>();
            return verticalLayoutTransform.anchoredPosition.y;
        }

        private float GetCurrentLootDisplayHeight()
        {
            var viewportHeight = _viewport.GetComponent<RectTransform>().rect.height;
            var contentHeight = _verticalLayout.GetComponent<RectTransform>().rect.height;
            
            return viewportHeight > contentHeight
                ? contentHeight
                : viewportHeight;
        }
        
        private void ScrollDownIfRequired(float iconLowerBound, float viewportLowerBound)
        {
            if(viewportLowerBound < iconLowerBound)
            {
                MoveDownByIconHeight();
            }
        }
        
        private void MoveDownByIconHeight()
        {
            var deltaMovePercent = ConvertHeightIntoScrollPercentage(GetIconHeight());
            _scrollRect.verticalNormalizedPosition -= deltaMovePercent;
        }

        #endregion

        #region ScrollUp

        private void ScrollUpIfNeeded(LootIcon.LootIcon currentIcon)
        {
            try
            {
                TryScrollUp(currentIcon);
            }
            catch (Exception _)
            {
                // ignored
            }
        }

        private void TryScrollUp(LootIcon.LootIcon currentIcon)
        {
            var currentIconUpperBound = GetIconUpperBound(currentIcon);
            var viewportUpperBound = GetViewportUpperBound();

            ScrollUpIfRequired(currentIconUpperBound, viewportUpperBound);
        }

        private float GetIconUpperBound(LootIcon.LootIcon icon)
        {
            var iconCenter = GetVerticalIconCenter(icon);
            var iconHeight = GetIconHeight();
            
            return iconCenter - iconHeight / 2;
        }

        private float GetViewportUpperBound()
        {
            return GetContentUpperBound();
        }

        private void ScrollUpIfRequired(float iconUpperBound, float viewportUpperBound)
        {
            if(viewportUpperBound > iconUpperBound)
            {
                MoveUpByIconHeight();
            }
        }

        private void MoveUpByIconHeight()
        {
            var deltaMovePercent = ConvertHeightIntoScrollPercentage(GetIconHeight());
            _scrollRect.verticalNormalizedPosition += deltaMovePercent;
        }

        #endregion
        
        private float GetVerticalIconCenter(LootIcon.LootIcon icon)
        {
            var rectTransform = icon.GetComponent<RectTransform>();
            return Mathf.Abs(rectTransform.anchoredPosition.y);
        }
        
        private float GetIconHeight()
        {
            return _lootIcon.rect.height;
        }

        private float GetFullContentHeight()
        {
            var verticalLayoutRect = _verticalLayout.GetComponent<RectTransform>();
            return verticalLayoutRect.rect.height;
        }

        private float ConvertHeightIntoScrollPercentage(float value)
        {
            return value / GetFullContentHeight() / (1 - _scrollbar.size);
        }
    }
}