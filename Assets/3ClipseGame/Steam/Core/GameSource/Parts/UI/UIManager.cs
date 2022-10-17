using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject menuVisualObject;
        [SerializeField] private GameObject hudVisualObject;

        public void SwitchHUD(bool isActive) => hudVisualObject.SetActive(isActive);
        public void SwitchMenu(bool isActive) => menuVisualObject.SetActive(isActive);
    }
}
