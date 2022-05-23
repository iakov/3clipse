using _3ClipseGame.Steam.UI.Scripts.TabSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Globals.Input.HUDInput
{
    public class HUDInputHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent<TabButton> switchModeToMenu;
        
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

            _hudInputActions.HUDActions.ToggleMainMenu.started += _ => { switchModeToMenu?.Invoke(menuMainTab);};
            _hudInputActions.HUDActions.ToggleInventoryMenu.started += _ => {switchModeToMenu?.Invoke(menuInventoryTab);};
            _hudInputActions.HUDActions.ToggleSkillsMenu.started += _ => {switchModeToMenu?.Invoke(menuSkillsTab);};

            _hudInputActions.HUDActions.ShowElementalWheel.started += OnToggleElementalWheel;
            _hudInputActions.HUDActions.ShowElementalWheel.canceled += OnToggleElementalWheel;
        }

        private void OnDisable()
        {
            _hudInputActions.HUDActions.Disable();
            _hudInputActions.Disable();
        }
        
        private void OnToggleElementalWheel(InputAction.CallbackContext context) => elementalWheel.SetActive(context.ReadValueAsButton());
    }
}
