using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen
{
    public class HeadLookAt : MonoBehaviour
    {
        private Animator _animator;
        [Range(0f, 1f)] [SerializeField] private float _weight = 0.5f;
        [SerializeField] private bool _isAIKActive = true;
        [SerializeField] private GameObject _lookAtObject;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        private void OnAnimatorIK(int layerIndex)
        {
            if (!_isAIKActive) return;
             
            _animator.SetLookAtWeight(_weight);
            var lookAtPoint = _lookAtObject.transform.position;
            _animator.SetLookAtPosition(lookAtPoint);
        }
    }
}
