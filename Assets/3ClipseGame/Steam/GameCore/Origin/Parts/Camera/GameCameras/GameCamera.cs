using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Camera.GameCameras
{
    public class GameCamera : MonoBehaviour
    {
        [SerializeField] private CameraType _cameraType;
        [SerializeField] private string _animatorStateName;
        [SerializeField] private Animator _cameraAnimator;

        public CameraType GetCameraType() => _cameraType;
        
        public void Enable()
        {
            _cameraAnimator.Play(_animatorStateName);
        }
    }
}