using System;
using _3ClipseGame.Steam.Globals.UI.Scripts.TabSystem;
using _3ClipseGame.Steam.UI.Scripts.TabSystem;
using UnityEngine;

namespace _3ClipseGame.Steam.Input.MenuInput
{
    public class MenuInputHandler : MonoBehaviour
    {
        [SerializeField] private SwitchTabGroup switchTabGroup;
        private MenuInputActions _menuInputActions;

        private void OnEnable()
        {
            _menuInputActions = new MenuInputActions();
            
            _menuInputActions.Enable();
            _menuInputActions.MenuActions.Enable();

            _menuInputActions.MenuActions.Exit.started += context => { switchTabGroup.SwitchTabToHUD(); };
        }

        private void OnDisable()
        {
            _menuInputActions.MenuActions.Disable();
            _menuInputActions.Disable();
        }
    }
}
