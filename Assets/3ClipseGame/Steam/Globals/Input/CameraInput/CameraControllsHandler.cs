using Cinemachine;
using UnityEngine;

namespace _3ClipseGame.Steam.Globals.Input.CameraInput
{
    public class CameraControllsHandler : MonoBehaviour
    {
        private CinemachineFreeLook _freeLookCamera;
        private CameraControlls _cameraControlls;
        
        private float _beforeDisableYValue;
        private float _beforeDisableXValue;

        private void Awake()
        {
            _cameraControlls = new CameraControlls();
            _freeLookCamera = GetComponent<CinemachineFreeLook>();
        } 
        private void OnEnable() => Enable(); 
        private void OnDisable() => Disable();

        public void Enable()
        {
            _cameraControlls.Enable();

            if (_beforeDisableXValue == 0 && _beforeDisableYValue == 0) return;
            
            _freeLookCamera.m_XAxis.m_MaxSpeed = _beforeDisableXValue;
            _freeLookCamera.m_YAxis.m_MaxSpeed = _beforeDisableYValue;
        }

        public void Disable()
        {
            _cameraControlls.Disable();
            
            if (_freeLookCamera.m_XAxis.Value == 0f && _freeLookCamera.m_YAxis.Value == 0f) return;

            _beforeDisableXValue = _freeLookCamera.m_XAxis.m_MaxSpeed;
            _beforeDisableYValue = _freeLookCamera.m_YAxis.m_MaxSpeed;

            _freeLookCamera.m_XAxis.m_MaxSpeed = 0f;
            _freeLookCamera.m_YAxis.m_MaxSpeed = 0f;
        }
    }
}
