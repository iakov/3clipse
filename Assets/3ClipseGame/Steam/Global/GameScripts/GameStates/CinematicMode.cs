using _3ClipseGame.Steam.Global.Input.HUDInput;
using _3ClipseGame.Steam.Global.Input.MenuInput;
using UnityEngine;

namespace _3ClipseGame.Steam.Global.GameScripts.GameStates
{
    public class CinematicMode : GameMode
    {
        #region Input

        [Header("Input")] 
        [SerializeField] private MenuInputHandler menuInput;
        [SerializeField] private HUDInputHandler hudInput;

        #endregion

        #region GameModeMethods
        
        public override void StartEnable()
        {
            uiManager.SwitchMenu(false);
            uiManager.SwitchHUD(false);
            pointerManager.SwitchPointerMode(CursorLockMode.Locked);
            
            menuInput.Disable();
            hudInput.Disable();
        }

        public override void Disable(){}
        #endregion
    }
}
