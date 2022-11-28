using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts.Panels
{
    public class JourneyPanel : MonoBehaviour
    {
        [SerializeField] private CursorScript _cursorScript;
        [SerializeField] private HeadLookAt _headLookAtScript;

        private void OnEnable()
        {
            _cursorScript.Switch(true, true);
            _headLookAtScript.Switch(true);
        }

        private void OnDisable()
        {
            _cursorScript.Switch(false, false);
            _headLookAtScript.Switch(false);
        }
    }
}
