using UnityEngine;

namespace _3ClipseGame.Steam.Global.Input.Scripts
{
    public abstract class InputHandler : MonoBehaviour
    {
        #region AbstractMethods

        public abstract void Enable();
        public abstract void Disable();

        #endregion
    }
}
