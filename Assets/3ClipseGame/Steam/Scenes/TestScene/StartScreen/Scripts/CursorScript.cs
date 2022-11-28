using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts
{
    public class CursorScript : MonoBehaviour
    {
        [SerializeField] private Camera _activeCamera;
        [SerializeField] private LayerMask _hitLayers;
        [SerializeField] private Sprite _cursorSprite;

        private bool _isActive;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.SetCursor(_cursorSprite.texture, Vector2.zero, CursorMode.Auto);
        }

        private void Update()
        {
            if (!_isActive) return;
            
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

        public void SwitchCursorVisibility(bool isActive)
        {
            Cursor.visible = isActive;
        }

        public void SwitchObjectVisibility(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SwitchObjectTrack(bool isActive)
        {
            _isActive = isActive;
        }
    }
}
