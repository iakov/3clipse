using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

namespace Packages.LeanTween.Presets
{
    public class FadeInScript : MonoBehaviour
    {
        [SerializeField] private float _fadeDuration;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null) throw new SerializationException("CanvasGroup is required for fade out");
        }

        public IEnumerator FadeOut()
        {
            _canvasGroup.LeanAlpha(0f, _fadeDuration)
                .setEaseInCubic();
            yield return new WaitForSeconds(_fadeDuration);
        }

        public IEnumerator FadeIn()
        {
            _canvasGroup.LeanAlpha(1f, _fadeDuration)
                .setEaseOutCirc();
            yield return new WaitForSeconds(_fadeDuration);
        }
    }
}
