using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3ClipseGame.Steam.UserInterface.Menu.Scripts.TabSystem
{
    public class TabGroup : MonoBehaviour
    {

        [SerializeField] private TabButton defaultTab;
        
        private List<TabButton> _tabButtons;
        
        private TabButton _currentActiveButton;
        private TabButton _currentScopedButton;

        private void Awake()
        {
            _tabButtons = GetComponentsInChildren<TabButton>().ToList();
            _currentActiveButton = defaultTab;
        }

        private void OnEnable(){
            _currentActiveButton.SetTabActive(true);
            ResetTabs();
        }

        private void OnDisable() => _currentActiveButton = defaultTab;

        public void OnTabEnter(TabButton button)
        {
            _currentScopedButton = button;
            ResetTabs();
        }

        public void OnTabExit()
        {
            _currentScopedButton = null;
            ResetTabs();
        }

        public void OnTabClicked(TabButton button)
        {
            _currentActiveButton = button;
            ResetTabs();
        }

        private void ResetTabs()
        {
            _tabButtons ??= new List<TabButton>();
            DisableAllTabs();
            SetTabActive();
            SetTabScoped();
        }

        private void DisableAllTabs()
        {
            foreach (var tabButton in _tabButtons.Where(tabButton => tabButton != _currentActiveButton && tabButton != _currentScopedButton)) 
                tabButton.SetTabActive(false);
        }

        private void SetTabActive()
        {
            _currentActiveButton.SetTabActive(true);
        }

        private void SetTabScoped()
        {
            if (_currentScopedButton == null || _currentActiveButton == _currentScopedButton) return;
            _currentScopedButton.SetTabScoped();
        }
    }
}
