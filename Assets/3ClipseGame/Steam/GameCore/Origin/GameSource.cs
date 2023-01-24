using _3ClipseGame.Steam.GameCore.Origin.Interfaces;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Camera;
using _3ClipseGame.Steam.GameCore.Origin.Parts.GameStates;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Input;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Player.Interfaces;
using _3ClipseGame.Steam.GameCore.Origin.Parts.UserInterface;
using UnityEngine;
using UnityEngine.SceneManagement;
using CameraType = _3ClipseGame.Steam.GameCore.Origin.Parts.Camera.CameraType;
using InputType = _3ClipseGame.Steam.GameCore.Origin.Parts.Input.InputType;

namespace _3ClipseGame.Steam.GameCore.Origin
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
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private string _startGameScene;

        public IMultiManager<InputType> GetInputManager() => _inputManager;
        public ISoloManager<CameraType> GetCameraManager() => _cameraManager;
        public ISoloManager<GameStateType> GetStatesManager() => _statesManager;
        public Player GetPlayer() => _player;
        public SerializationDependencies GetSerializationDependencies() => _serializationDependencies;
        public UIManager GetUIManager() => _uiManager;

        private void Start()
        {
            if (SceneManager.sceneCount == 1) SceneManager.LoadScene(_startGameScene, LoadSceneMode.Additive);
        }
    }
}
