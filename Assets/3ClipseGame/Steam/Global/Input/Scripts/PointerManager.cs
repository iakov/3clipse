using UnityEngine;

namespace _3ClipseGame.Steam.Global.Input.Scripts
{
    public class PointerManager : MonoBehaviour
    {
        #region MonoBehaviourMethods

        private void OnEnable() => SwitchPointerMode(CursorLockMode.Locked);

        #endregion

        #region PublicMethods

        public void SwitchPointerMode(CursorLockMode mode) => Cursor.lockState = mode;

        #endregion
    }
}
