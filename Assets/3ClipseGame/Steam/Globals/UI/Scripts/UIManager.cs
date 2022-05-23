using System;
using _3ClipseGame.Steam.Globals.UI.Scripts.TabSystem;
using _3ClipseGame.Steam.UI.Scripts.TabSystem;
using UnityEngine;

namespace _3ClipseGame.Steam.Globals.UI.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [Header("Game Mode Tabs")]
        [SerializeField] private GameObject menuTab;
        [SerializeField] private GameObject hudTab;

        [Header("Menu Mode Tabs")] 
        [SerializeField] private TabGroup menuTabGroup;
        [SerializeField] private TabButton mainMenuButton;

        [NonSerialized] public TabButton LastTabButton;

        public void SwitchUIToMenu()
        {
            menuTab.SetActive(true);
            hudTab.SetActive(false);
            
            menuTabGroup.OnTabClicked(LastTabButton);
        }

        public void SwitchUIToHUD()
        {
            menuTab.SetActive(false);
            hudTab.SetActive(true);

            LastTabButton = mainMenuButton;
        }
    }
}
