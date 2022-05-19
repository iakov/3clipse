using _3ClipseGame.Steam.Input.HUDInput;
using _3ClipseGame.Steam.Input.MenuInput;
using _3ClipseGame.Steam.Input.PlayerInput;
using UnityEngine;

namespace _3ClipseGame.Steam.Scripts
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private HUDInputHandler hudInputHandler;
        [SerializeField] private MovementInputHandler moveInputHandler;
        [SerializeField] private MenuInputHandler menuInputHandler;
        
        public MovementInputHandler MoveInputHandler => moveInputHandler;
        public HUDInputHandler HUDInputHandler => hudInputHandler;
        public MenuInputHandler MenuInputHandler => menuInputHandler;
    }
}
