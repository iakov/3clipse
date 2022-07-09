using _3ClipseGame.Steam.Global.Input.Scripts;
using _3ClipseGame.Steam.Global.UI.Scripts.TabSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Global.Input.HUDInput
{
    public class HUDInputHandler : InputHandler
    {
        #region Serialization

        [Header("Menu Tabs")]
        [SerializeField] private TabButton menuInventoryTab;
        [SerializeField] private TabButton menuMainTab;
        [SerializeField] private TabButton menuSkillsTab;

        [Header("HUD Components")] 
        [SerializeField] private GameObject elementalWheel;

        [Header("Events")]
        [SerializeField] private UnityEvent<TabButton> switchModeToMenu;
        
        #endregion

        #region Initialization

        private HUDInputActions _hudInputActions;

        #endregion

        #region MonoBehaviourMethods

        private void Awake() => _hudInputActions = new HUDInputActions();
        private void OnEnable() => Enable();
        private void OnDisable() => Disable();

        #endregion

        #region PublicMethods

        public override void Enable()
        {
            _hudInputActions.Enable();
            _hudInputActions.HUDActions.Enable();

            _hudInputActions.HUDActions.ToggleMainMenu.started += _ => { switchModeToMenu?.Invoke(menuMainTab);};
            _hudInputActions.HUDActions.ToggleInventoryMenu.started += _ => {switchModeToMenu?.Invoke(menuInventoryTab);};
            _hudInputActions.HUDActions.ToggleSkillsMenu.started += _ => {switchModeToMenu?.Invoke(menuSkillsTab);};

            _hudInputActions.HUDActions.ShowElementalWheel.started += OnToggleElementalWheel;
            _hudInputActions.HUDActions.ShowElementalWheel.canceled += OnToggleElementalWheel;
        }

        public override void Disable()
        {
            _hudInputActions.HUDActions.Disable();
            _hudInputActions.Disable();
        }

        #endregion

        #region PrivateMethods
        
        private void OnToggleElementalWheel(InputAction.CallbackContext context) =>
            elementalWheel.SetActive(context.ReadValueAsButton());

        #endregion
    }
}
