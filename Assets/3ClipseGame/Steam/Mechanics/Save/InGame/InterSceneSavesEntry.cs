using _3ClipseGame.Steam.Mechanics.Save.UI.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame
{
    public class InterSceneSavesEntry : MonoBehaviour
    {
        [SerializeField] private ScenesLoaderView _saveScenesLoader;
        [SerializeField] private SavesManager _savesManager;
        
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
            _savesManager.Initiate();
        }

        public void LoadSave(int id) => _savesManager.LoadGame(id, _saveScenesLoader);

        public void LoadScene(SceneObject scene) => _saveScenesLoader.Load(scene);

        public void NewGame() => _savesManager.NewGame(_saveScenesLoader);
    }
}
