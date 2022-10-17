using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.CameraInput
{
    public class CameraInputProcessor : InputProcessor
    {
        [SerializeField] private CameraInputHandler _inputHandler;
        
        public override void Enable()
        {
            _inputHandler.Enable();
        }

        public override void Disable()
        {
            _inputHandler.Disable();
        }
    }
}