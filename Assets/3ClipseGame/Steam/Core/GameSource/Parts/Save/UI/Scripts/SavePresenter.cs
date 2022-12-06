using UnityEngine;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Scripts
{
    public abstract class SavePresenter : MonoBehaviour,
        IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected GameObject _hoverHighlightObject;
        [SerializeField] protected GameObject _selectedHighlightObject;

        protected bool IsSelected;
        
        public abstract void Use();

        public abstract void Select();
        
        public abstract void Unselect();

        protected abstract void Highlight();

        protected abstract void Unhighlight();
        
        public abstract void OnPointerClick(PointerEventData eventData);

        public void OnPointerEnter(PointerEventData eventData) => Highlight();
        
        public void OnPointerExit(PointerEventData eventData) => Unhighlight();
    }
}