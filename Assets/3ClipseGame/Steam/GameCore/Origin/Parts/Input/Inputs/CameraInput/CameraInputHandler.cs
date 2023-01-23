using Cinemachine;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs.CameraInput
{
    [RequireComponent(typeof(CinemachineFreeLook))]
    public class CameraInputHandler : InputHandler
    {
        private CinemachineFreeLook _camera;
        private Vector2 _maxSpeed;

        private void Awake()
        {
            _camera = GetComponent<CinemachineFreeLook>();
            RememberMaxSpeed();
        }
        
        public override void Enable()
        {
            _camera.m_XAxis.m_MaxSpeed = _maxSpeed.x;
            _camera.m_YAxis.m_MaxSpeed = _maxSpeed.y;
            
            _camera.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
        }

        public override void Disable()
        {
            RememberMaxSpeed();
            _camera.m_XAxis.m_MaxSpeed = 0;
            _camera.m_YAxis.m_MaxSpeed = 0;
            
            _camera.m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetNoRoll;
        }

        private void RememberMaxSpeed()
        {
            var currentXMaxSpeed = _camera.m_XAxis.m_MaxSpeed;
            var currentYMaxSpeed = _camera.m_YAxis.m_MaxSpeed;
            _maxSpeed = new Vector2(currentXMaxSpeed, currentYMaxSpeed);
        }
    }
}
