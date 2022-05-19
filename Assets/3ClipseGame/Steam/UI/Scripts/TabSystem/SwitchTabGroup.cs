using _3ClipseGame.Steam.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.UI.Scripts.TabSystem
{
    public class SwitchTabGroup : MonoBehaviour
    {
        [SerializeField] private GameObject menuTab;
        [SerializeField] private GameObject hudTab;
        [SerializeField] private TabGroup menuTabGroup;

        public void SwitchTabToMenu(TabButton activeTab)
        {
            menuTab.SetActive(true);
            hudTab.SetActive(false);
            
            Game.Instance.InputManager.MoveInputHandler.Deactivate();
            Game.Instance.CursorScript.SwitchCursorMode(CursorLockMode.Confined);
            menuTabGroup.OnTabClicked(activeTab);
        }

        public void SwitchTabToHUD()
        {
            menuTab.SetActive(false);
            hudTab.SetActive(true);
            
            Game.Instance.InputManager.MoveInputHandler.Activate();
            Game.Instance.CursorScript.SwitchCursorMode(CursorLockMode.Locked);
        }
    }
}
