using System;
using System.Collections.Generic;
using System.IO;
using _3ClipseGame.Steam.Core.GameSource.Parts;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Data;
using _3ClipseGame.Steam.Mechanics.Save.InGame.SaveSerializers;
using _3ClipseGame.Steam.Mechanics.Save.UI.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame
{
    [CreateAssetMenu(fileName = "New Save Manager", menuName = "Save/Save Manager")]
    public class SavesManager : ScriptableObject
    {
        [Header("New Save Data")]
        [SerializeField] private Sprite _newGameImage;
        [SerializeField] private SceneObject _newGameScene;

        public IEnumerable<GameSave> GameSaves => _gameSaves;
        public bool IsSavesFound { get; private set; }

        private List<GameSave> _gameSaves;
        private SaveSerializer _saveSerializer;

        private void Awake()
        {
            _saveSerializer = new BinarySaveSerializer();
            FindAllSaves();
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

        public void NewGame(ScenesLoaderView loaderView)
        {
            var id = Random.Range(1000, 9999);
            var save = GameSave.NewGame(id, _newGameImage, _newGameScene);
            _gameSaves.Add(save);
            LoadGame(id, loaderView);
        }

        public void SaveGame(int id, SerializationDependencies dependencies)
        {
            var save = FindSaveByID(id);
            save.Save(dependencies);
            _saveSerializer.UpdateSave(save);
        }

        public void LoadGame(int id, ScenesLoaderView loaderView)
        {
            var currentSave = FindSaveByID(id);
            loaderView.Load(currentSave);
        }

        public void DeleteSave(int id)
        {
            var save = FindSaveByID(id);
            
            _saveSerializer.DestroySave(save);
            _gameSaves.Remove(save);
        }

        private GameSave FindSaveByID(int id)
        {
            var save =  _gameSaves.Find(save => save.ID == id);
            if (save == null) throw new ArgumentException($"Cannot find game save with id: {id}");
            return save;
        }
    }
}
