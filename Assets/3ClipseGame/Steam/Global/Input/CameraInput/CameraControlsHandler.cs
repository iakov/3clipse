using _3ClipseGame.Steam.Global.Input.Scripts;
using Cinemachine;

namespace _3ClipseGame.Steam.Global.Input.CameraInput
{
    public class CameraControlsHandler : InputHandler
    {
        #region Initialization

        private CinemachineStateDrivenCamera _stateDrivenCamera;

        private CinemachineFreeLook _freeLookCamera
        {
            get
            {
                if (_stateDrivenCamera == null) return null;

                var freeLookCamera = _stateDrivenCamera.LiveChild.VirtualCameraGameObject.GetComponent<CinemachineFreeLook>();
                
                return freeLookCamera == null ? null : freeLookCamera;
            }
        }
        
        private CameraControlls _cameraControlls;
        
        private float _beforeDisableYValue;
        private float _beforeDisableXValue;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _cameraControlls = new CameraControlls();
            _stateDrivenCamera = GetComponent<CinemachineStateDrivenCamera>();
        } 
        
        private void OnEnable() => Enable(); 
        private void OnDisable() => Disable();

        #endregion

        #region PublicMethods

        public override void Enable()
        {
            _cameraControlls.Enable();

            if (_beforeDisableXValue == 0 && _beforeDisableYValue == 0) return;
            
            _freeLookCamera.m_XAxis.m_MaxSpeed = _beforeDisableXValue;
            _freeLookCamera.m_YAxis.m_MaxSpeed = _beforeDisableYValue;
        }

        public override void Disable()
        {
            _cameraControlls.Disable();
            
            if (_freeLookCamera.m_XAxis.Value == 0f && _freeLookCamera.m_YAxis.Value == 0f) return;

            _beforeDisableXValue = _freeLookCamera.m_XAxis.m_MaxSpeed;
            _beforeDisableYValue = _freeLookCamera.m_YAxis.m_MaxSpeed;

            _freeLookCamera.m_XAxis.m_MaxSpeed = 0f;
            _freeLookCamera.m_YAxis.m_MaxSpeed = 0f;
        }

        #endregion
    }
}
