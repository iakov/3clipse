using UnityEngine;
using UnityEngine.Events;

namespace _3ClipseGame.Steam.Globals.Input.MenuInput
{
    public class MenuInputHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent switchModeToHUD;
        
        private MenuInputActions _menuInputActions;

        private void OnEnable()
        {
            _menuInputActions = new MenuInputActions();
            
            _menuInputActions.Enable();
            _menuInputActions.MenuActions.Enable();

            _menuInputActions.MenuActions.Exit.started += _ => { switchModeToHUD?.Invoke(); };
        }

        private void OnDisable()
        {
            _menuInputActions.MenuActions.Disable();
            _menuInputActions.Disable();
        }
    }
}
