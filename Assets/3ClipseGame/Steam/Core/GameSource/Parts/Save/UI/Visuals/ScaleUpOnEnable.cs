using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Visuals
{
    public class ScaleUpOnEnable : MonoBehaviour
    {
        [SerializeField] private float _scaleTime;
        [SerializeField] private Vector3 _finalScale;

        private void OnEnable()
        {
            gameObject.LeanScale(_finalScale, _scaleTime);
        }
        
        public void ScaleDown()
        {
            gameObject.LeanScale(Vector3.zero, _scaleTime).setOnComplete(Disable);;
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
