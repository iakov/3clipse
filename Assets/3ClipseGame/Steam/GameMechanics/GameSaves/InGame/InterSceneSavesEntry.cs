using System.Collections.Generic;
using System.Linq;
using _3ClipseGame.Steam.GameCore.Origin;
using _3ClipseGame.Steam.GameMechanics.GameSaves.UI.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _3ClipseGame.Steam.GameMechanics.GameSaves.InGame
{
    public class InterSceneSavesEntry : MonoBehaviour
    {
        [SerializeField] private string _saveManagerSceneName;
        [SerializeField] private ScenesLoaderView _saveScenesLoader;
        [SerializeField] private SavesManager _savesManager;
        [SerializeField] private List<SceneObject> _sceneObjects;

        public static InterSceneSavesEntry Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                Debug.LogError("Found more than one instance of SavesEntry");
            }
            
            Instance = this;
        }

        private void Start()
        {
            foreach (var scene in SceneManager.GetAllScenes().Where(scene => scene.name != _saveManagerSceneName)) 
                SceneManager.UnloadSceneAsync(scene);
            
            _savesManager.Initiate();
        }

        public void LoadSave(int id) => _savesManager.LoadGame(id, _saveScenesLoader);

        public void LoadScene(SceneObject scene) => _saveScenesLoader.Load(scene);

        public void NewGame() => _savesManager.NewGame(_saveScenesLoader);

        public void SaveGame()
        {
            var gameSource = GameSource.Instance;
            var sceneName = _saveScenesLoader.CurrentScene.SceneName;
            if(gameSource == null) return;
            
            _savesManager.SaveGame(sceneName, gameSource.GetSerializationDependencies());
        }

        public SceneObject GetSceneByName(string sceneName) =>
            _sceneObjects.Find(sceneObject => sceneObject.SceneName == sceneName);
    }
}
