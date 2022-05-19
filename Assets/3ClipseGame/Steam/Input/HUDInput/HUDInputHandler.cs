using _3ClipseGame.Steam.UI.Scripts.TabSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Input.HUDInput
{
    public class HUDInputHandler : MonoBehaviour
    {
        [SerializeField] private SwitchTabGroup switchTabGroup;
        
        [SerializeField] private TabButton menuInventoryTab;
        [SerializeField] private TabButton menuMainTab;
        [SerializeField] private TabButton menuSkillsTab;
        
        private HUDInputActions _hudInputActions;

        private void OnEnable()
        {
            _hudInputActions = new HUDInputActions();
            
            _hudInputActions.Enable();
            _hudInputActions.HUDActions.Enable();

            _hudInputActions.HUDActions.ToggleMainMenu.started += OnToggleMainMenu;
            _hudInputActions.HUDActions.ToggleInventoryMenu.started += OnToggleInventoryMenu;
            _hudInputActions.HUDActions.ToggleSkillsMenu.started += OnToggleSkillsMenu;
        }

        private void OnDisable()
        {
            _hudInputActions.HUDActions.Disable();
            _hudInputActions.Disable();
        }

        private void OnToggleMainMenu(InputAction.CallbackContext context) => switchTabGroup.SwitchTabToMenu(menuMainTab);
        private void OnToggleInventoryMenu(InputAction.CallbackContext context) => switchTabGroup.SwitchTabToMenu(menuInventoryTab);
        private void OnToggleSkillsMenu(InputAction.CallbackContext context) => switchTabGroup.SwitchTabToMenu(menuSkillsTab);
    }
}
