using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts
{
    public class CursorScript : MonoBehaviour
    {
        private void Start() => Cursor.lockState = CursorLockMode.Locked;
    }
}
