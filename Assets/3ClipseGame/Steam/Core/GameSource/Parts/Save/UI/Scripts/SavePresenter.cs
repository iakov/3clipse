using _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Visuals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Scripts
{
    public abstract class SavePresenter : MonoBehaviour,
        IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject _hoverHighlightObject;
        [SerializeField] private GameObject _selectedHighlightObject;

        private bool _isSelected;

        public abstract void Use();

        public abstract void OnPointerClick(PointerEventData eventData);

        public void OnPointerEnter(PointerEventData eventData)  => Hover();
        
        public void OnPointerExit(PointerEventData eventData) => UnHover();

        private void Hover()
        {
            if(_isSelected) return;
            var highlightGameObject = _hoverHighlightObject.gameObject;
            highlightGameObject.SetActive(true);
        }

        private void UnHover()
        {
            var highlightGameObject = _hoverHighlightObject.gameObject;
            var unscaleSlowly = highlightGameObject.GetComponent<ScaleUpOnEnable>();
            unscaleSlowly.ScaleDown();
        }
        
        public void Activate()
        {
            _isSelected = true;
            UnHover();
            _selectedHighlightObject.SetActive(true);
        }

        public void Deactivate()
        {
            _isSelected = false;
            var unscaleSlowly = _selectedHighlightObject.GetComponent<ScaleUpOnEnable>();
            unscaleSlowly.ScaleDown();
        }
    }
}