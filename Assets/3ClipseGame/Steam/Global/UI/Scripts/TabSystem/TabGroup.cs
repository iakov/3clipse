using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3ClipseGame.Steam.Global.UI.Scripts.TabSystem
{
    public class TabGroup : MonoBehaviour
    {
        #region PrivateInitialization

        [SerializeField] private TabButton defaultTab;
        
        private List<TabButton> _tabButtons;
        
        private TabButton _currentActiveButton;
        private TabButton _currentScopedButton;
        
        #endregion

        #region PublicMethods

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

        #endregion

        #region PrivateMethods

        private void ResetTabs()
        {
            if (_tabButtons == null) _tabButtons = new List<TabButton>();
            
            foreach (var tabButton in _tabButtons.Where(tabButton => tabButton != _currentActiveButton && tabButton != _currentScopedButton)) 
                tabButton.SetTabActive(false);
            
            
            _currentActiveButton.SetTabActive(true);

            if (_currentScopedButton == null || _currentActiveButton == _currentScopedButton) return;
            _currentScopedButton.SetTabScoped();
        }

        #endregion
    }
}
