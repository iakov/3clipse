using _3ClipseGame.Steam.Globals.Input.CameraInput;
using _3ClipseGame.Steam.Globals.Input.HUDInput;
using _3ClipseGame.Steam.Globals.Input.MenuInput;
using _3ClipseGame.Steam.Input.PlayerInput;
using UnityEngine;

namespace _3ClipseGame.Steam.Globals.Scripts
{
    public class InputManager : MonoBehaviour
    {
        [Header("Input Handlers")]
        [SerializeField] private HUDInputHandler hudInputHandler;
        [SerializeField] private MovementInputHandler moveInputHandler;
        [SerializeField] private MenuInputHandler menuInputHandler;
        [SerializeField] private CameraControllsHandler cameraControllsHandler;

        public MovementInputHandler MoveInputHandler => moveInputHandler;
        public HUDInputHandler HUDInputHandler => hudInputHandler;
        public MenuInputHandler MenuInputHandler => menuInputHandler;
        public CameraControllsHandler CameraControllsHandler => cameraControllsHandler;
    }
}
