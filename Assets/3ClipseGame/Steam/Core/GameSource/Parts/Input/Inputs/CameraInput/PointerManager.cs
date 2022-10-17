using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.CameraInput
{
    public class PointerManager : MonoBehaviour
    {
        private void OnEnable() => SwitchPointerMode(CursorLockMode.Locked);
        
        public void SwitchPointerMode(CursorLockMode mode) => Cursor.lockState = mode;
    }
}
