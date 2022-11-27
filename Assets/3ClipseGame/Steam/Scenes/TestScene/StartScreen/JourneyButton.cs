using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen
{
    public class JourneyButton : MonoBehaviour
    {
        [SerializeField] private CursorScript _cursorScript;
        [SerializeField] private HeadLookAt _headLookAtScript;

        private void OnEnable()
        {
            _cursorScript.Switch(true);
            _headLookAtScript.Switch(true);
        }

        private void OnDisable()
        {
            _cursorScript.Switch(false);
            _headLookAtScript.Switch(false);
        }
    }
}
