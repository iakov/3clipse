using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts.Panels
{
    public class SavesPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CinemachineDollyCart _cart;
        [SerializeField] private CinemachineSmoothPath _dollyTrack;
        [SerializeField] private AnimationCurve _cartSpeedDistance;
        [SerializeField] private RectTransform _panelVisual;
        [SerializeField] private CursorScript _cursorScript;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _cursorScript.Switch(false, true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _cursorScript.Switch(true, true);
        }
        
        public void Enable()
        {
            Cursor.visible = false;
            StartCoroutine(CartSpeedRoutine());
        }
        
        private IEnumerator CartSpeedRoutine()
        {
            var time = 0f;

            while (_dollyTrack.EvaluatePosition(_dollyTrack.MaxPos) != _cart.transform.position)
            {
                var currentSpeed = _cartSpeedDistance.Evaluate(time);
                _cart.m_Speed = currentSpeed;
                time += Time.deltaTime;
                yield return null;
            }

            _panelVisual.gameObject.SetActive(true);
            _cursorScript.Switch(true, true);
        }
    }
}
