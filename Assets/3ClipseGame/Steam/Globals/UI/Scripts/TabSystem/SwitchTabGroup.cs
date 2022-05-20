using _3ClipseGame.Steam.Globals.Scripts;
using _3ClipseGame.Steam.UI.Scripts.TabSystem;
using UnityEngine;

namespace _3ClipseGame.Steam.Globals.UI.Scripts.TabSystem
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
            Game.Instance.CameraManager.SwitchCamera(CameraManager.CameraType.MainMenu);
            menuTabGroup.OnTabClicked(activeTab);
        }

        public void SwitchTabToHUD()
        {
            menuTab.SetActive(false);
            hudTab.SetActive(true);
            
            Game.Instance.InputManager.MoveInputHandler.Activate();
            Game.Instance.CursorScript.SwitchCursorMode(CursorLockMode.Locked);
            Game.Instance.CameraManager.SwitchCamera(CameraManager.CameraType.MainCharacter);
        }
    }
}
