using Cinemachine;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs.CameraInput
{
    [RequireComponent(typeof(CinemachineFreeLook))]
    public class CameraInputHandler : InputHandler
    {
        private CinemachineFreeLook _camera;

        private void Awake()
        {
            _camera = GetComponent<CinemachineFreeLook>();
        }
        
        public override void Enable()
        {
            _camera.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
        }

        public override void Disable()
        {
            _camera.m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetNoRoll;
        }
    }
}
