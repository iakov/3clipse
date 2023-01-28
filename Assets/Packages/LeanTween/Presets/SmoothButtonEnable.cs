using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

namespace Packages.LeanTween.Presets
{
    public class SmoothButtonEnable : MonoBehaviour
    {
        [SerializeField] private float _slideTime;
        [SerializeField] private float _offsetTime;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(_offsetTime);
            gameObject.LeanRotate(Vector3.zero, _slideTime);
            
            var canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null) throw new SerializationException("CanvasGroup is required for fade out");
            
            canvasGroup.LeanAlpha(1f, _slideTime);
        }
    }
}
