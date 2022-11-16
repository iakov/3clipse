using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private SerializationDependencies _serializationDependencies;

        public IEnumerable<GameSave> GameSaves => _allGameSaves;

        private static List<GameSave> _allGameSaves;
        private static GameSave _currentSave;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            _serializationDependencies = GetComponent<SerializationDependencies>();
            _allGameSaves = FindAllSaves();
        }

        private List<GameSave> FindAllSaves()
        {
            var directories = Directory.GetFiles(SaveSerializer.SavePath);
            return directories.Select(SaveSerializer.Deserialize).ToList();
        }

        public void LoadSave(GameSave save)
        {
            _currentSave = save;
            SceneManager.LoadScene(save.LocationName);
            SceneManager.sceneLoaded += ApplySaveData;
        }

        private void ApplySaveData(Scene loadedScene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= ApplySaveData;
            _serializationDependencies.FindDependencies(FinalizeLoad);
        }

        private void FinalizeLoad()
        {
            _currentSave.ApplyData(_serializationDependencies);
        }

        public void DeleteSave(GameSave save)
        {
            File.Delete(SaveSerializer.SavePath + save.Id);
            _allGameSaves.Remove(save);
            if (_currentSave == save) _currentSave = null;
        }

        public GameSave CreateNewSave()
        {
            _currentSave = null;
            CreateSave();
            return _currentSave;
        }

        private void CreateSave()
        {
            var gameSave = new GameSave(_serializationDependencies);
            _allGameSaves.Add(gameSave);
            SaveSerializer.Serialize(gameSave);
            _currentSave = gameSave;
        }
    }
}
