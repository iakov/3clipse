using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private SerializationDependencies _serializationDependencies;

        public IEnumerable GameSaves => _allGameSaves;
        
        private List<GameSave> _allGameSaves;
        private string _saveDirectory;
        
        private void Awake()
        {
            _saveDirectory = Application.persistentDataPath + "/saves";
            if (!Directory.Exists(_saveDirectory)) Directory.CreateDirectory(_saveDirectory);
            _allGameSaves = FindAllSaves();
        }

        private List<GameSave> FindAllSaves()
        {
            var directories = Directory.GetFiles(_saveDirectory);

            return directories.Select(SaveSerializer.Deserialize).ToList();
        }

        public bool TryCreateNewSave(string saveName, out GameSave newSave)
        {
            newSave = null;
            var path = GetPathWithName(saveName);
            if (IsFileExists(path)) return false;
            
            newSave = CreateSave(saveName);
            SaveSerializer.Serialize(path, newSave);
            return true;
        }

        private GameSave CreateSave(string saveName)
        {
            var save = new GameSave(saveName, _serializationDependencies);
            save.UpdateData();
            return save;
        }

        public bool TryLoadSave(string saveName)
        {
            var path = GetPathWithName(saveName);
            if (!IsFileExists(path)) return false;
            LoadSave(path);
            return true;
        }

        private void LoadSave(string path)
        {
            var newLoaded = SaveSerializer.Deserialize(path);
            newLoaded.ApplyData(_serializationDependencies);
        }

        public void DeleteSave(GameSave save)
        {
            _allGameSaves.Remove(save);
            var path = GetPathWithName(save.Name);
            File.Delete(path);
        }
        
        private string GetPathWithName(string saveName)
        {
            var path = string.Concat(_saveDirectory, "/", saveName, ".save");
            return path;
        }
        
        private bool IsFileExists(string path)
        {
            return File.Exists(path);
        }
    }
}
