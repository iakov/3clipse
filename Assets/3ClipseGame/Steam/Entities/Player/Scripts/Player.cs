using _3ClipseGame.Steam.Globals.UI.Scripts.TabSystem;
using _3ClipseGame.Steam.UI.Scripts.TabSystem;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private TabGroup inventoryTabsSystem;
        private TabButton _lastTabButton;

        public void ActivateLastTab() => inventoryTabsSystem.OnTabClicked(_lastTabButton);
    }
}
