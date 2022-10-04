using System;
using _3ClipseGame.Steam.Global.Input.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Core.Input.HUDInput
{
    public class HUDInputHandler : InputHandler
    {
        #region Serialization

        [Header("HUD Components")] 
        [SerializeField] private GameObject elementalWheel;

        [Header("Events")]
        [SerializeField] private UnityEvent switchModeToMenu;
        
        #endregion

        #region Public

        public event UnityAction SwitchingModeToMenu
        {
            add => switchModeToMenu.AddListener(value);
            remove => switchModeToMenu.RemoveListener(value);
        }

        public event Action LootInteracted;
        public event Action<float> LootScrolled;

        #endregion

        #region Initialization

        private HUDInputActions _hudInputActions;

        #endregion

        #region MonoBehaviourMethods

        private void Awake() => _hudInputActions = new HUDInputActions();

        private void OnEnable()
        {
            _hudInputActions.Enable();
            _hudInputActions.HUDActions.Enable();

            _hudInputActions.HUDActions.ToggleMainMenu.started += SwitchModeToMain;

            _hudInputActions.HUDActions.ShowElementalWheel.started += OnToggleElementalWheel;
            _hudInputActions.HUDActions.ShowElementalWheel.canceled += OnToggleElementalWheel;

            _hudInputActions.HUDActions.LootInteraction.started += OnLootInteracted;

            _hudInputActions.HUDActions.LootScroll.started += OnLootScrolled;
        }

        private void OnDisable()
        {
            _hudInputActions.HUDActions.Disable();
            _hudInputActions.Disable();
        }

        #endregion

        #region PublicMethods

        public override void Enable() => OnEnable();

        public override void Disable() => OnDisable();

        #endregion

        #region PrivateMethods
        
        private void OnToggleElementalWheel(InputAction.CallbackContext context) 
            => elementalWheel.SetActive(context.ReadValueAsButton());

        private void SwitchModeToMain(InputAction.CallbackContext context)
            => switchModeToMenu?.Invoke();

        private void OnLootInteracted(InputAction.CallbackContext context)
            => LootInteracted?.Invoke();

        private void OnLootScrolled(InputAction.CallbackContext context)
            => LootScrolled?.Invoke(context.ReadValue<float>());

        #endregion
    }
}
