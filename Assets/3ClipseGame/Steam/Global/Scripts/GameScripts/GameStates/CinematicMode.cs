using System.Collections.Generic;
using _3ClipseGame.Steam.Global.Input.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Global.GameScripts.GameStates
{
    public class CinematicMode : GameMode
    {
        #region Input

        [Header("Input")]
        [SerializeField] private List<InputHandler> listInputHandlers;

        #endregion

        #region GameModeMethods
        
        public override void StartEnable()
        {
            uiManager.SwitchMenu(false);
            uiManager.SwitchHUD(false);
            pointerManager.SwitchPointerMode(CursorLockMode.Locked);

            foreach (var inputHandler in listInputHandlers) inputHandler.Disable();
        }

        public override void Disable(){}
        #endregion
    }
}
