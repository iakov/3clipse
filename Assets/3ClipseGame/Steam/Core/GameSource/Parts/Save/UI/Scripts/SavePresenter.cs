using UnityEngine;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Scripts
{
    public abstract class SavePresenter : MonoBehaviour,
        IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform _hoverHighlight;

        public abstract void OnPointerClick(PointerEventData eventData);

        public void OnPointerEnter(PointerEventData eventData)
        {
            var highlightGameObject = _hoverHighlight.gameObject;
            highlightGameObject.SetActive(true);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            var highlightGameObject = _hoverHighlight.gameObject;
            highlightGameObject.SetActive(false);
        }
    }
}