using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs
{
    public abstract class InputHandler : MonoBehaviour
    {
        public abstract void Enable();
        public abstract void Disable();
    }
}
