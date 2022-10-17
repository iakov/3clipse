using _3ClipseGame.Steam.Core.GameSource;
using _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.MenuInput;
using _3ClipseGame.Steam.Core.GameSource.Parts.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Menu.Scripts
{
    public class MenuDisabler : MonoBehaviour
    {
        [SerializeField] private MenuInputProcessor _menuInputProcessor;

        private void Update()
        {
            if (_menuInputProcessor.GetIsExitPressed())
                GameSource.Instance.GetStatesManager().Enable(GameStateTypes.PlayMode);
        }
    }
}
