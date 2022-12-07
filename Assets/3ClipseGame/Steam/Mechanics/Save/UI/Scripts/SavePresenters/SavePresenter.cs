using System;
using Packages.LeanTween.Presets;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.Mechanics.Save.UI.Scripts.SavePresenters
{
    public abstract class SavePresenter : MonoBehaviour,
        IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject _hoverHighlightObject;
        [SerializeField] private GameObject _selectedHighlightObject;

        public event Action<SavePresenter, Sprite> Selected;
        private bool _isSelected;
        
        public abstract void Use();

        protected abstract Sprite GetImage();

        public void OnPointerClick(PointerEventData eventData) => Select();

        public void OnPointerEnter(PointerEventData eventData) => Highlight();
        
        public void OnPointerExit(PointerEventData eventData) => Unhighlight();
        
        private void Select()
        {
            var scaleComponent = GetScaleComponent(_selectedHighlightObject);
            scaleComponent.ScaleUp();
            Unhighlight();
            _isSelected = true;
            
            var image = GetImage();
            Selected?.Invoke(this, image);
        }

        public void Unselect()
        {
            var scaleComponent = GetScaleComponent(_selectedHighlightObject);
            scaleComponent.ScaleDown();
            _isSelected = false;
        }
        
        private void Highlight()
        {
            if(_isSelected) return;
            var scaleComponent = GetScaleComponent(_hoverHighlightObject);
            scaleComponent.ScaleUp();
        }

        private void Unhighlight()
        {
            var scaleComponent = GetScaleComponent(_hoverHighlightObject);
            scaleComponent.ScaleDown();
        }

        private SmoothScale GetScaleComponent(GameObject reference)
        {
            var highlightGameObject = reference.gameObject;
            var scaleComponent = highlightGameObject.GetComponent<SmoothScale>();
            return scaleComponent;
        }
    }
}