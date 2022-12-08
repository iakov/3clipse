using System;
using _3ClipseGame.Steam.Core.GameSource;
using _3ClipseGame.Steam.Core.GameSource.Parts;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Statics;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Data
{
    [Serializable]
    public class GameSave
    {
        public readonly int ID; 
        public string SaveLocation { get; private set; }
        public string SaveDate { get; private set; }
        public Sprite GetImage => SpriteFromTexture();

        public event Action Loaded;
        
        private GameData _gameData;
        private Texture2D _imageTexture;
        [NonSerialized] private SceneObject _sceneObject;
        [NonSerialized] private SaveScenesLoader _lastScenesLoader;
    
        private Sprite SpriteFromTexture()
        {
            var fullImageRect = new Rect(0, 0, _imageTexture.width, _imageTexture.height);
            var centerPivot = new Vector2(0.5f, 0.5f);
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
            SaveLocation = defaultScene.SceneName;
            SaveDate = DateFormatter.GetDateForSave();
            _imageTexture = defaultImage.texture;
            _gameData = GameData.NewGame();
            _sceneObject = defaultScene;
        }   

        public void Save(SerializationDependencies dependencies)
        {
            SaveDate = DateFormatter.GetDateForSave();
            _imageTexture = ScreenCapture.CaptureScreenshotAsTexture();
            _gameData.UpdateData(dependencies);
        }

        public void Load(SaveScenesLoader loader)
        {
            _lastScenesLoader = loader;
            loader.LoadScene(_sceneObject);
            loader.LoadFinished += FinishLoad;
        }

        private void FinishLoad()
        {
            _lastScenesLoader.LoadFinished -= FinishLoad;
            var gameSource = GameSource.Instance;
            if (gameSource != null)
            {
                var dependencies = gameSource.GetSerializationDependencies();
                _gameData.ApplyData(dependencies);
            }

            Loaded?.Invoke();
        }

        private void OnSceneLoaded()
        {
            var dependencies = GameSource.Instance.GetSerializationDependencies();
            _gameData.ApplyData(dependencies);
        }
    }
}
