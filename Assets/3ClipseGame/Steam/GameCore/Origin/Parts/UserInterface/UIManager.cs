using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.UserInterface
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject menuVisualObject;
        [SerializeField] private GameObject hudVisualObject;
        [SerializeField] private GameObject dialoguesVisualObject;

        public void SwitchHUD(bool isActive) => hudVisualObject.SetActive(isActive);
        public void SwitchMenu(bool isActive) => menuVisualObject.SetActive(isActive);
        public void SwitchDialogue(bool isActive) => dialoguesVisualObject.SetActive(isActive);
    }
}
