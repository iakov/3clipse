using _3ClipseGame.Steam.Core.GameSource.Parts.Camera;
using _3ClipseGame.Steam.Core.GameSource.Parts.Input;
using _3ClipseGame.Steam.Core.GameSource.Parts.Player;
using _3ClipseGame.Steam.Core.GameSource.Parts.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource
{
    public class GameSource : MonoBehaviour
    {
        #region Singleton

        public static GameSource Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        #endregion

        [SerializeField] private Player _player;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private CameraManager _cameraManager;
        [SerializeField] private GameStatesManager _statesManager;

        public InputManager GetInputManager() => _inputManager;
        public CameraManager GetCameraManager() => _cameraManager;
        public GameStatesManager GetStatesManager() => _statesManager;
        public Player GetPlayer() => _player;
    }
}
