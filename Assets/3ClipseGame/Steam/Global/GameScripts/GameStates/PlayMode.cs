using _3ClipseGame.Steam.Global.Input.CameraInput;
using _3ClipseGame.Steam.Global.Input.HUDInput;
using _3ClipseGame.Steam.Global.Input.PlayerInput;
using _3ClipseGame.Steam.Global.StateDrivenCamera;
using UnityEngine;

namespace _3ClipseGame.Steam.Global.GameScripts.GameStates
{
    public class PlayMode : GameMode
    {
        #region Serialization

        [Header("Input")]
        [SerializeField] private MovementInputHandler movementInputHandler;
        [SerializeField] private HUDInputHandler hudInputHandler;
        [SerializeField] private CameraControllsHandler cameraControlsHandler;
        
        #endregion

        #region Initialization

        private CameraAnimatorController.CameraType _previousCameraType = CameraAnimatorController.CameraType.MainCharacter;

        #endregion
        
        #region GameModeMethods

        public override void StartEnable()
        {
            cameraAnimatorController.SwitchCamera(_previousCameraType);
            blendBegan?.Invoke();
            StartCoroutine(TrackBlendCompletion());
            
            pointerManager.SwitchPointerMode(CursorLockMode.Locked);
            hudInputHandler.Enable();
            uiManager.SwitchMenu(false);
            Time.timeScale = timeScale;

            BlendCompleted += EndEnable;
        }
        
        public override void Disable()
        {
            movementInputHandler.Disable();
            hudInputHandler.Disable();
            cameraControlsHandler.Disable();

            _previousCameraType = cameraAnimatorController.GetCurrentStateName();
        }

        #endregion

        #region PrivateMethods
        
        private void EndEnable()
        {
            movementInputHandler.Enable();
            uiManager.SwitchHUD(true);
            cameraControlsHandler.Enable();

            BlendCompleted -= EndEnable;
        }

        #endregion
    }
}
