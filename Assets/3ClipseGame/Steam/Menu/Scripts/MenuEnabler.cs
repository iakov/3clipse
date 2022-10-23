using _3ClipseGame.Steam.Core.GameSource;
using _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.HUDInput;
using _3ClipseGame.Steam.Core.GameSource.Parts.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Menu.Scripts
{
    public class MenuEnabler : MonoBehaviour
    {
        [SerializeField] private HUDInputProcessor _hudInputProcessor;

        private void Update()
        {
            if (_hudInputProcessor.GetIsToggleMenu())
                GameSource.Instance.GetStatesManager().Enable(GameStateType.Menu);
        }
    }
}
