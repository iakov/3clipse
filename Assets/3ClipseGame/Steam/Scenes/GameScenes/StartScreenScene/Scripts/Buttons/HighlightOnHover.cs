using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.Scenes.GameScenes.StartScreenScene.Scripts.Buttons
{
    public class HighlightOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Color _primalColor;
        [SerializeField] private Color _newColor;
        [SerializeField] private float _hoverSize;
        [SerializeField] private float _hoverTime;

        private void OnDisable() => Unhighlight();

        public void OnPointerEnter(PointerEventData eventData) => Highlight();

        public void OnPointerExit(PointerEventData eventData) => Unhighlight();

        private void Highlight()
        {
            _text.color = _newColor;
            var hoverVector = new Vector3(_hoverSize, _hoverSize, _hoverSize);
            gameObject.LeanScale(hoverVector,_hoverTime);
        }

        private void Unhighlight()
        {
            _text.color = _primalColor;
            gameObject.LeanScale(Vector3.one, _hoverTime);
        }
    }
}
