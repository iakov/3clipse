using System;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.Statics;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.Data
{
    [Serializable]
    public class GameSave
    {
        public readonly int ID; 
        public string SaveLocation { get; private set; }
        public string SaveDate { get; private set; }
        public Sprite Image => SpriteFromTexture();
        
        private GameData _gameData;
        private Texture2D _imageTexture;

        private Sprite SpriteFromTexture()
        {
            var fullImageRect = new Rect(0, 0, _imageTexture.width, _imageTexture.height);
            var centerPivot = new Vector2(0.5f, 0.5f);
            var sprite = Sprite.Create(_imageTexture, fullImageRect, centerPivot);
            return sprite;
        }
        
        public static GameSave NewGame(int id, Sprite defaultImage)
        {
            return new GameSave(id, defaultImage);
        }

        private GameSave(int id, Sprite defaultImage)
        {
            ID = id;
            SaveLocation = "Introduction";
            SaveDate = DateFormatter.GetDateForSave();
            _imageTexture = defaultImage.texture;
            _gameData = GameData.NewGame();
        }

        public void Save(SerializationDependencies dependencies)
        {
            SaveLocation = "Introduction";
            SaveDate = DateFormatter.GetDateForSave();
            _imageTexture = ScreenCapture.CaptureScreenshotAsTexture();
            _gameData.UpdateData(dependencies);
        }

        public void Load()
        {
            var scenesLoader = SaveManager.Instance.ScenesLoader;
            scenesLoader.LoadSaveScene(SaveLocation, OnSceneLoaded);
        }

        private void OnSceneLoaded()
        {
            var dependencies = GameSource.Instance.GetSerializationDependencies();
            _gameData.ApplyData(dependencies);
        }
    }
}
