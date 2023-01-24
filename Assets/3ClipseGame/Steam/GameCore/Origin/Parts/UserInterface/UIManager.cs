using System.Collections.Generic;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.UserInterface
{
    public class UIManager : MonoBehaviour
    {
        private List<GameObject> _currentPanels = new();

        public void DrawNewPanel(GameObject panel, DrawMode drawMode)
        {
            if (drawMode == DrawMode.Mono) HideEverything();
            
            panel.SetActive(true);
            _currentPanels.Add(panel);
        }

        public void HideEverything()
        {
            foreach (var displayedPanel in _currentPanels) 
                HidePanel(displayedPanel);
        }

        public void HidePanel(GameObject panel)
        {
            panel.SetActive(false);
            _currentPanels.Remove(panel);
        }
    }
}
