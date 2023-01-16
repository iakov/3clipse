using UnityEngine;

namespace Packages.LeanTween.Presets
{
    public class SmoothScale : MonoBehaviour
    {
        [SerializeField] private float _scaleTime;
        [SerializeField] private Vector3 _finalScale;

        public void ScaleDown()
        {
            gameObject.LeanCancel();
            gameObject.LeanScale(Vector3.zero, _scaleTime);
        }

        public void ScaleUp()
        {
            gameObject.LeanCancel();
            gameObject.LeanScale(_finalScale, _scaleTime);
        }

        private void OnDestroy()
        {
            gameObject.LeanCancel();
        }
    }
}
