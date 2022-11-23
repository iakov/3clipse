using System;
using System.Collections.Generic;
using System.IO;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.Data;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.SaveSerializers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance { get; private set; }
        
        [SerializeField] private Sprite _newGameImage;

        public IEnumerable<GameSave> GameSaves => _gameSaves;
        public SaveScenesLoader ScenesLoader => _scenesLoader;
        public bool IsSavesFound { get; private set; }

        private List<GameSave> _gameSaves;
        private SaveSerializer _saveSerializer;
        private SaveScenesLoader _scenesLoader;

        private void Awake()
        {
            _saveSerializer = new BinarySaveSerializer();
            _scenesLoader = GetComponent<SaveScenesLoader>();
            
            InitializeSingleton();
            FindAllSaves();
        }

        private void InitializeSingleton()
        {
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;
        }

        private void FindAllSaves()
        {
            _gameSaves = new List<GameSave>();
            var files = Directory.GetFiles(_saveSerializer.SavePath);
            
            foreach (var file in files)
            {
                var gameSave = _saveSerializer.Deserialize(file);
                _gameSaves.Add(gameSave);
            }

            IsSavesFound = true;
        }

        public void NewGame()
        {
            var id = Random.Range(1000, 9999);
            var newGameSave = GameSave.NewGame(id, _newGameImage);
            
            _saveSerializer.Serialize(newGameSave);
            _gameSaves.Add(newGameSave);

            newGameSave.Load();
        }
        
        public void SaveGame(int id, SerializationDependencies dependencies)
        {
            var gameSave = FindSaveByID(id);
            
            if (gameSave == null) throw new ArgumentException($"Cannot find game save with id: {id}"); 
            
            gameSave.Save(dependencies);
            _saveSerializer.UpdateSave(gameSave);
        }

        public void LoadGame(int id)
        {
            var gameSave = FindSaveByID(id);

            if (gameSave == null) NewGame();
            else gameSave.Load();
        }

        public void DeleteSave(int id)
        {
            var save = FindSaveByID(id);
            if (save == null) throw new ArgumentException($"Cannot find game save with id: {id}");
            
            _saveSerializer.DestroySave(save);
            _gameSaves.Remove(save);
        }

        private GameSave FindSaveByID(int id)
        {
            return _gameSaves.Find(save => save.ID == id);
        }
    }
}
