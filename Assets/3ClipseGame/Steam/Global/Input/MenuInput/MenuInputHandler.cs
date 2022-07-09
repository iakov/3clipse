using _3ClipseGame.Steam.Global.Input.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace _3ClipseGame.Steam.Global.Input.MenuInput
{
    public class MenuInputHandler : InputHandler
    {
        #region Serialization

        [SerializeField] private UnityEvent switchModeToHUD;

        #endregion

        #region Initialization

        private MenuInputActions _menuInputActions;

        #endregion

        #region MonoBehaviourMethods

        private void Awake() => _menuInputActions = new MenuInputActions();
        private void OnEnable() => Enable();
        private void OnDisable() => Disable();

        #endregion

        #region PublicMethods

        public override void Enable()
        {
            _menuInputActions.Enable();
            _menuInputActions.MenuActions.Enable();

            _menuInputActions.MenuActions.Exit.started += _ => { switchModeToHUD?.Invoke(); };
        }

        public override void Disable()
        {
            _menuInputActions.MenuActions.Disable();
            _menuInputActions.Disable();
        }

        #endregion
    }
}
