using System;
using System.Collections.Generic;
using System.IO;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance { get; private set; }
        
        [SerializeField] private Sprite _newGameImage;
        [SerializeField] private InputAction _saveInputAction;
        
        public IEnumerable<GameSave> GameSaves => _gameSaves;

        private List<GameSave> _gameSaves;
        private GameSave _currentGameSave;

        private void Awake()
        {
            InitializeSingleton();
            FindAllSaves();
        }

        private void OnEnable()
        {
            _saveInputAction.started += TrySaveGame;
        }

        private void OnDisable()
        {
            _saveInputAction.started -= TrySaveGame;
        }

        private void TrySaveGame(InputAction.CallbackContext context)
        {
            if(_currentGameSave == null) return;

            var dependencies = GameSource.Instance.GetSerializationDependencies();
            SaveGame(_currentGameSave.ID, dependencies);
            Debug.Log("Save");
        }

        private void InitializeSingleton()
        {
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;
        }

        private void FindAllSaves()
        {
            _gameSaves = new List<GameSave>();
            var files = Directory.GetFiles(SaveSerializer.SavePath);
            
            foreach (var file in files)
            {
                var gameSave = SaveSerializer.Deserialize(file);
                _gameSaves.Add(gameSave);
            }
        }

        public void NewGame(int id)
        {
            var texture2D = _newGameImage.texture;
            var newGameSave = GameSave.NewGame(id, texture2D);
            
            _gameSaves.Add(newGameSave);
            SaveSerializer.Serialize(newGameSave);
            
            newGameSave.Load();
        }

        public void LoadGame(int id)
        {
            var gameSave = FindSaveByID(id);

            if (gameSave == null)
            {
                NewGame(id);
            }
            else
            {
                gameSave.Load();
                _currentGameSave = gameSave;
            }
        }

        public void SaveGame(int id, SerializationDependencies dependencies)
        {
            var gameSave = FindSaveByID(id);
            
            if (gameSave == null) throw new ArgumentException($"Cannot find game save with id: {id}"); 
            
            gameSave.Save(dependencies);
            SaveSerializer.DeleteFile(id.ToString());
            SaveSerializer.Serialize(gameSave);
        }

        public void DeleteSave(int id)
        {
            var save = FindSaveByID(id);
            if (save == null) throw new ArgumentException($"Cannot find game save with id: {id}");
            
            SaveSerializer.DeleteFile(SaveSerializer.GetSavePathWithId(id));
            _gameSaves.Remove(save);
        }

        private GameSave FindSaveByID(int id)
        {
            return _gameSaves.Find(save => save.ID == id);
        }
    }
}
