using _3ClipseGame.Steam.Core.GameSource.Parts.Camera;
using _3ClipseGame.Steam.Core.GameSource.Parts.Input;
using _3ClipseGame.Steam.Core.GameSource.Parts.Player;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame;
using _3ClipseGame.Steam.Core.GameSource.Parts.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using CameraType = _3ClipseGame.Steam.Core.GameSource.Parts.Camera.CameraType;
using InputType = _3ClipseGame.Steam.Core.GameSource.Parts.Input.InputType;

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
        [SerializeField] private SerializationDependencies _serializationDependencies;
        [SerializeField] private string _startGameScene;

        public IMultiManager<InputType> GetInputManager() => _inputManager;
        public ISoloManager<CameraType> GetCameraManager() => _cameraManager;
        public ISoloManager<GameStateType> GetStatesManager() => _statesManager;
        public Player GetPlayer() => _player;
        public SerializationDependencies GetSerializationDependencies() => _serializationDependencies;

        private void Start()
        {
            if (SceneManager.sceneCount == 1) SceneManager.LoadScene(_startGameScene, LoadSceneMode.Additive);
        }
    }
}
