using System;
using _3ClipseGame.Steam.GameCore.Origin;
using _3ClipseGame.Steam.GameMechanics.GameSaves.InGame.Statics;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.GameSaves.InGame.Data
{
    [Serializable]
    public class GameSave
    {
        public readonly int ID; 
        public string SaveName { get; private set; }
        public string SaveDate { get; private set; }
        public Sprite GetImage => SpriteFromTexture();
        public SceneObject SceneObject => InterSceneSavesEntry.Instance.GetSceneByName(SaveName);

        private GameData _gameData;
        private Texture2D _imageTexture;
        private bool _isInitial;

        public GameSave()
        {
            _isInitial = true;
        }
    
        private Sprite SpriteFromTexture()
        {
            var fullImageRect = new Rect(0, 0, _imageTexture.width, _imageTexture.height);
            var centerPivot = new Vector2(_imageTexture.width / 2f, _imageTexture.height / 2f);
            var sprite = Sprite.Create(_imageTexture, fullImageRect, centerPivot);
            return sprite;
        }
        
        public static GameSave NewGame(int id, Sprite defaultImage, SceneObject defaultScene)
        {
            return new GameSave(id, defaultImage, defaultScene);
        }

        private GameSave(int id, Sprite defaultImage, SceneObject defaultScene)
        {
            ID = id;
            SaveName = defaultScene.SceneName;
            SaveDate = DateFormatter.GetDateForSave();
            _imageTexture = defaultImage.texture;
            _gameData = GameData.NewGame();
        }

        public void SaveSceneData(string currentScene, SerializationDependencies dependencies)
        {
            _isInitial = false;
            SaveName = currentScene;
            SaveDate = DateFormatter.GetDateForSave();
            _imageTexture = ScreenCapture.CaptureScreenshotAsTexture();
            _gameData.UpdateData(dependencies);
        }

        public void ApplySaveDataToScene()
        {
            var gameSource = GameSource.Instance;
            if (gameSource == null) return;
            if (_isInitial) return;
            
            var dependencies = gameSource.GetSerializationDependencies();
            _gameData.ApplyData(dependencies);
        }

        private void OnSceneLoaded()
        {
            var dependencies = GameSource.Instance.GetSerializationDependencies();
            _gameData.ApplyData(dependencies);
        }
    }
}
