using System;
using System.Collections.Generic;
using System.IO;
using _3ClipseGame.Steam.Core.GameSource.Parts;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Data;
using _3ClipseGame.Steam.Mechanics.Save.InGame.SaveSerializers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance { get; private set; }
        
        [Header("New Save Data")]
        [SerializeField] private Sprite _newGameImage;
        [SerializeField] private SceneObject _newGameScene;

        public IEnumerable<GameSave> GameSaves => _gameSaves;
        public SaveScenesLoader ScenesLoader => GetComponent<SaveScenesLoader>();
        public bool IsSavesFound { get; private set; }

        private List<GameSave> _gameSaves;
        private SaveSerializer _saveSerializer;

        private void Awake()
        {
            _saveSerializer = new BinarySaveSerializer();
            
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
            var save = GameSave.NewGame(id, _newGameImage, _newGameScene);
            _gameSaves.Add(save);
            LoadGame(id);
        }

        public void SaveGame(int id, SerializationDependencies dependencies)
        {
            var save = FindSaveByID(id);
            save.Save(dependencies);
            _saveSerializer.UpdateSave(save);
        }

        private GameSave _currentSave;

        public void LoadGame(int id)
        {
            _currentSave = FindSaveByID(id);
            _currentSave.Load(ScenesLoader);
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
