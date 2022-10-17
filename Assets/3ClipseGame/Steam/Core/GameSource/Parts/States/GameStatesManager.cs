using System.Collections.Generic;
using _3ClipseGame.Steam.Core.GameSource.Parts.States.GameStates;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.States
{
    public class GameStatesManager : MonoBehaviour, ISoloManager<GameStateTypes>
    {
        [SerializeField] private List<GameMode> _gameModes;
        [SerializeField] private GameMode _currentGameMode;

        private void Start()
        {
            _currentGameMode.StartEnter();
        }
        
        public void Enable(GameStateTypes enableObjectType)
        {
            var newMode = _gameModes.Find(mode => mode.GetModeType() == enableObjectType);
            if (newMode != null)
            {
                _currentGameMode.Exit();
                _currentGameMode = newMode;
                _currentGameMode.StartEnter();
            }
        }

        public GameStateTypes[] GetActive()
        {
            return new GameStateTypes[]{_currentGameMode.GetModeType()};
        }
    }
}
