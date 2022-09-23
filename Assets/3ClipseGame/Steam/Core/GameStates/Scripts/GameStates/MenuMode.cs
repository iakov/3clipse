using _3ClipseGame.Steam.Core.GameStates.Scripts.GameStates;
using _3ClipseGame.Steam.Core.Scripts.GameScripts.GameStates;
using _3ClipseGame.Steam.Global.Input.MenuInput;
using _3ClipseGame.Steam.Global.StateDrivenCamera;
using Cinemachine;
using UnityEngine;

namespace _3ClipseGame.Steam.Global.Scripts.GameScripts.GameStates
{
    public class MenuMode : GameMode
    {
        #region Serialization

        [Header("Input")] 
        [SerializeField] private MenuInputHandler menuInputHandler;

        [Header("Camera")] 
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        #endregion

        #region GameModeMethods
        
        public override void StartEnable()
        {
            cameraAnimatorController.SwitchCamera(CameraAnimatorController.CameraType.MainMenu);
            blendBegan?.Invoke();
            StartCoroutine(TrackBlendCompletion(virtualCamera));
            
            uiManager.SwitchHUD(false);
            Time.timeScale = timeScale;

            BlendCompleted += EndEnable;
        }

        public override void Disable()
        {
            menuInputHandler.Disable();
        }

        #endregion

        #region PrivateMethods

        private void EndEnable()
        {
            menuInputHandler.Enable();
            uiManager.SwitchMenu(true);
            pointerManager.SwitchPointerMode(CursorLockMode.Confined);
        }

        #endregion
    }
}
