using System.Collections.Generic;
using System.Linq;
using _3ClipseGame.Steam.UI.Scripts.TabSystem;
using UnityEngine;

namespace _3ClipseGame.Steam.Globals.UI.Scripts.TabSystem
{
    public class TabGroup : MonoBehaviour
    {
        private List<TabButton> _tabButtons;
        [SerializeField] private Sprite tabIdle;
        [SerializeField] private Sprite tabHover;
        [SerializeField] private Sprite tabActive;

        private TabButton _currentActiveButton;

        public void Subscribe(TabButton button)
        {
            _tabButtons ??= new List<TabButton>();
            _tabButtons.Add(button);
        }

        public void OnTabEnter(TabButton button)
        {
            ResetTabs();
            if (button == _currentActiveButton) return;
            button.backgroundImage.sprite = tabHover;
        }

        public void OnTabExit() => ResetTabs();
        

        public void OnTabClicked(TabButton button)
        {
            ResetTabs();
            foreach (var tabButton in _tabButtons.Where(tabButton => tabButton != button))
            {
                tabButton.SetTabActive(false);
                tabButton.backgroundImage.sprite = tabIdle;
            }
            
            button.SetTabActive(true);
            _currentActiveButton = button;
            button.backgroundImage.sprite = tabActive;
        }

        private void ResetTabs()
        {
            foreach (var button in _tabButtons.Where(button => button != _currentActiveButton))
                button.backgroundImage.sprite = tabIdle;
        }
    }
}
