using _3ClipseGame.Steam.Global.GameScripts.GameStates;
using _3ClipseGame.Steam.Global.Input.CameraInput;
using _3ClipseGame.Steam.Global.Input.HUDInput;
using _3ClipseGame.Steam.Global.Input.PlayerInput;
using _3ClipseGame.Steam.Global.StateDrivenCamera;
using Cinemachine;
using UnityEngine;

namespace _3ClipseGame.Steam.Global.Scripts.GameScripts.GameStates
{
    public class PlayMode : GameMode
    {
        #region Serialization

        [Header("Input")]
        [SerializeField] private MovementInputHandler movementInputHandler;
        [SerializeField] private HUDInputHandler hudInputHandler;
        [SerializeField] private CameraControlsHandler cameraControlsHandler;

        [Header("Cameras")] 
        [SerializeField] private CinemachineFreeLook freeLookCamera;

        private ICinemachineCamera _lastCinemachineCamera;

        #endregion

        #region Initialization

        private CameraAnimatorController.CameraType _previousCameraType = CameraAnimatorController.CameraType.MainCharacter;

        #endregion
        
        #region GameModeMethods

        public override void StartEnable()
        {
            cameraAnimatorController.SwitchCamera(_previousCameraType);
            blendBegan?.Invoke();

            if (_lastCinemachineCamera == null) StartCoroutine(TrackBlendCompletion(freeLookCamera));
            else StartCoroutine(TrackBlendCompletion(_lastCinemachineCamera));

                pointerManager.SwitchPointerMode(CursorLockMode.Locked);
            uiManager.SwitchMenu(false);
            Time.timeScale = timeScale;

            BlendCompleted += EndEnable;
        }
        
        public override void Disable()
        {
            _lastCinemachineCamera = stateDrivenCamera.LiveChild;
            
            movementInputHandler.Disable();
            hudInputHandler.Disable();
            cameraControlsHandler.Disable();

            _previousCameraType = cameraAnimatorController.GetCurrentStateName();
        }

        #endregion

        #region PrivateMethods
        
        private void EndEnable()
        {
            hudInputHandler.Enable();
            movementInputHandler.Enable();
            uiManager.SwitchHUD(true);
            cameraControlsHandler.Enable();

            BlendCompleted -= EndEnable;
        }

        #endregion
    }
}
