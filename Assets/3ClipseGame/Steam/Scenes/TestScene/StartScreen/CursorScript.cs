using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen
{
    public class CursorScript : MonoBehaviour
    {
        [SerializeField] private Camera _activeCamera;
        [SerializeField] private LayerMask _hitLayers;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }

        private void Update()
        {
            var newPosition = GetCursorWorldPosition();
            transform.position = newPosition;
        }
        
        private Vector3 GetCursorWorldPosition()
        {
            Vector3 screenMousePosition = Mouse.current.position.ReadValue();
            screenMousePosition.z = _activeCamera.nearClipPlane;
            var ray = _activeCamera.ScreenPointToRay(screenMousePosition);
            Physics.Raycast(ray, out var hitInfo, float.PositiveInfinity, _hitLayers);
            return hitInfo.point;
        }
    }
}
