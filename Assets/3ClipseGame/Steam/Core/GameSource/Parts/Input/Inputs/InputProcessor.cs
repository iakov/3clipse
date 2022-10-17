using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs
{
    public abstract class InputProcessor : MonoBehaviour
    {
        [SerializeField] private InputType type;

        public abstract void Enable();
        public abstract void Disable();
        public InputType GetInputType() => type;
    }
}
