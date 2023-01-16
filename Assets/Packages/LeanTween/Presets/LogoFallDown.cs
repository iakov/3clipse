using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.GameScenes.StartScreen.Scripts.Tweening
{
    public class LogoFallDown : MonoBehaviour
    {
        [SerializeField] private float _slideDownDistance;
        [SerializeField] private float _slideDownTime;
        [SerializeField] private float _timeOffset;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(_timeOffset);
            
            var canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null) throw new SerializationException("CanvasGroup is required for fade out");

            var finalPosition = transform.position + Vector3.down * _slideDownDistance;
            gameObject.LeanMoveY(finalPosition.y, _slideDownTime);
            canvasGroup.LeanAlpha(1f, _slideDownTime);
        }
    }
}
