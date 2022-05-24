using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3ClipseGame.Steam.Global.UI.Scripts.TabSystem
{
    public class TabGroup : MonoBehaviour
    {
        #region PrivateInitialization

        private List<TabButton> _tabButtons;
        
        private TabButton _currentActiveButton;
        private TabButton _currentScopedButton;
        

        #endregion

        #region PublicMethods

        public void Subscribe(TabButton button)
        {
            _tabButtons ??= new List<TabButton>();
            _tabButtons.Add(button);
        }

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
            {
                tabButton.SetTabActive(false);
                tabButton.BackgroundImage.sprite = tabButton.tabIdle;
            }
            
            _currentActiveButton.SetTabActive(true);
            _currentActiveButton.BackgroundImage.sprite = _currentActiveButton.tabActive;

            if (_currentScopedButton == null || _currentActiveButton == _currentScopedButton) return;
            _currentScopedButton.BackgroundImage.sprite = _currentScopedButton.tabHover;
        }

        #endregion
    }
}
