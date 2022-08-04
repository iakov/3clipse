using _3ClipseGame.Steam.Global.GameScripts.GameStates;
using _3ClipseGame.Steam.Global.Input.MenuInput;
using _3ClipseGame.Steam.Global.StateDrivenCamera;
using _3ClipseGame.Steam.Global.UI.Scripts.TabSystem;
using Cinemachine;
using UnityEngine;

namespace _3ClipseGame.Steam.Global.Scripts.GameScripts.GameStates
{
    public class MenuMode : GameMode
    {
        #region Serialization

        [Header("Input")] 
        [SerializeField] private MenuInputHandler menuInputHandler;
        [SerializeField] private TabGroup menuTabGroup;

        [Header("Camera")] 
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        #endregion
        
        #region Initialization

        private _3ClipseGame.Steam.Global.UI.Scripts.TabSystem.TabButton _activeTabButton;
        
        #endregion

        #region GameModeMethods

        public void EnableWithTab(_3ClipseGame.Steam.Global.UI.Scripts.TabSystem.TabButton button)
        {
            StartEnable();
            _activeTabButton = button;
        }
        
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
            menuTabGroup.OnTabClicked(_activeTabButton);
            pointerManager.SwitchPointerMode(CursorLockMode.Confined);
        }

        #endregion
    }
}
