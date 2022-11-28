using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts.Panels
{
    public class JourneyPanel : MonoBehaviour
    {
        [SerializeField] private CursorScript _cursorScript;
        [SerializeField] private HeadLookAt _headLookAtScript;

        private void OnEnable()
        {
            Enable();
        }

        private void Enable()
        {
            _cursorScript.GameMode(true);
            _headLookAtScript.Switch(true);
        }

        private void OnDisable()
        {
            _cursorScript.GameMode(false);
            _headLookAtScript.Switch(false);
        }
    }
}
