using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs.CameraInput
{
    public class PointerManager : MonoBehaviour
    {
        public void SwitchPointerMode(CursorLockMode mode) => Cursor.lockState = mode;
    }
}
