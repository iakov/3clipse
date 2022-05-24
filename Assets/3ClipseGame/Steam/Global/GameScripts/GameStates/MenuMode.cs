using _3ClipseGame.Steam.Global.Input.MenuInput;
using _3ClipseGame.Steam.Global.StateDrivenCamera;
using _3ClipseGame.Steam.Global.UI.Scripts.TabSystem;
using UnityEngine;

namespace _3ClipseGame.Steam.Global.GameScripts.GameStates
{
    public class MenuMode : GameMode
    {
        #region Serialization

        [Header("Input")] 
        [SerializeField] private MenuInputHandler menuInputHandler;
        [SerializeField] private TabGroup menuTabGroup;

        #endregion
        
        #region Initialization

        private TabButton _activeTabButton;
        
        #endregion

        #region GameModeMethods

        public void EnableWithTab(TabButton button)
        {
            StartEnable();
            _activeTabButton = button;
        }
        
        public override void StartEnable()
        {
            cameraAnimatorController.SwitchCamera(CameraAnimatorController.CameraType.MainMenu);
            blendBegan?.Invoke();
            StartCoroutine(TrackBlendCompletion());
            
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
