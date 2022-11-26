using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen
{
    public class HighlightOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Color _primalColor;
        [SerializeField] private Color _newColor;
        [SerializeField] private float _hoverSize;
        [SerializeField] private float _hoverTime;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _text.color = _newColor;
            var hoverVector = new Vector3(_hoverSize, _hoverSize, _hoverSize);
            gameObject.LeanScale(hoverVector,_hoverTime);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _text.color = _primalColor;
            gameObject.LeanScale(Vector3.one, _hoverTime);
        }
    }
}
