using UnityEngine;

namespace _3ClipseGame.Steam.Global.UI.Scripts
{
    public class UIManager : MonoBehaviour
    {
        #region Initialization

        [SerializeField] private GameObject menuVisualObject;
        [SerializeField] private GameObject hudVisualObject;

        #endregion

        #region PublicMethods

        public void SwitchHUD(bool isActive) => hudVisualObject.SetActive(isActive);
        public void SwitchMenu(bool isActive) => menuVisualObject.SetActive(isActive);

        #endregion
    }
}
