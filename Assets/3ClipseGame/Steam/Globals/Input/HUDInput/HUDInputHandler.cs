using _3ClipseGame.Steam.Globals.UI.Scripts.TabSystem;
using _3ClipseGame.Steam.UI.Scripts.TabSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Input.HUDInput
{
    public class HUDInputHandler : MonoBehaviour
    {
        [SerializeField] private SwitchTabGroup switchTabGroup;
        
        [Header("Menu Tabs")]
        [SerializeField] private TabButton menuInventoryTab;
        [SerializeField] private TabButton menuMainTab;
        [SerializeField] private TabButton menuSkillsTab;

        [Header("HUD Components")] 
        [SerializeField] private GameObject elementalWheel;
        
        private HUDInputActions _hudInputActions;

        private void OnEnable()
        {
            _hudInputActions = new HUDInputActions();
            
            _hudInputActions.Enable();
            _hudInputActions.HUDActions.Enable();

            _hudInputActions.HUDActions.ToggleMainMenu.started += OnToggleMainMenu;
            _hudInputActions.HUDActions.ToggleInventoryMenu.started += OnToggleInventoryMenu;
            _hudInputActions.HUDActions.ToggleSkillsMenu.started += OnToggleSkillsMenu;

            _hudInputActions.HUDActions.ShowElementalWheel.started += OnToggleElementalWheel;
            _hudInputActions.HUDActions.ShowElementalWheel.canceled += OnToggleElementalWheel;
        }

        private void OnDisable()
        {
            _hudInputActions.HUDActions.Disable();
            _hudInputActions.Disable();
        }

        private void OnToggleMainMenu(InputAction.CallbackContext context) => switchTabGroup.SwitchTabToMenu(menuMainTab);
        private void OnToggleInventoryMenu(InputAction.CallbackContext context) => switchTabGroup.SwitchTabToMenu(menuInventoryTab);
        private void OnToggleSkillsMenu(InputAction.CallbackContext context) => switchTabGroup.SwitchTabToMenu(menuSkillsTab);
        private void OnToggleElementalWheel(InputAction.CallbackContext context) => elementalWheel.SetActive(context.ReadValueAsButton());
    }
}
