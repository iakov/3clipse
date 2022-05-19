using UnityEngine;

namespace _3ClipseGame.Steam.Scripts
{
    public class CursorScript : MonoBehaviour
    {
        public static CursorScript Instance { get; private set; }
        
        private void Awake() => Instance = this;
        private void Start() => SwitchCursorMode(CursorLockMode.Locked);
        public void SwitchCursorMode(CursorLockMode mode) => Cursor.lockState = mode;
    }
}
