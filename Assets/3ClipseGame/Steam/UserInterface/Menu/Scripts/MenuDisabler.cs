using _3ClipseGame.Steam.GameCore.Origin;
using _3ClipseGame.Steam.GameCore.Origin.Parts.GameStates;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs.MenuInput;
using UnityEngine;

namespace _3ClipseGame.Steam.UserInterface.Menu.Scripts
{
    public class MenuDisabler : MonoBehaviour
    {
        [SerializeField] private MenuInputProcessor _menuInputProcessor;

        private void Update()
        {
            if (_menuInputProcessor.GetIsExitPressed())
                GameSource.Instance.GetStatesManager().Enable(GameStateType.PlayMode);
        }
    }
}
