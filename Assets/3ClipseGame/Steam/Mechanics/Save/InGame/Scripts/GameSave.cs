using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts
{
    [Serializable]
    public class GameSave
    {
        public string LocationName { get; private set; }
        public string Date { get; private set; }
        public readonly int Id;
        
        private readonly byte[] _image;
        private readonly GameData _gameData;
        
        public Sprite Image => CreateScreenshotSprite(_image);

        public GameSave(SerializationDependencies data)
        {
            LocationName = SceneManager.GetActiveScene().name;
            Date = DateFormatter.GetDateForSave();
            Id = Directory.GetFiles(SaveSerializer.SavePath).Length + 1;

            _image = TakeScreenshot();
            _gameData = new GameData(data);
        }

        private byte[] TakeScreenshot()
        {
            var texture2d = ScreenCapture.CaptureScreenshotAsTexture();
            return texture2d.EncodeToPNG();
        }

        public void ApplyData(SerializationDependencies data)
        {
            _gameData.Apply(data);
        }

        public void UpdateData()
        {
            LocationName = SceneManager.GetActiveScene().name;
            Date = DateFormatter.GetDateForSave();
            _gameData.UpdateData();
        }

        private Sprite CreateScreenshotSprite(byte[] data)
        {
            var texture2d = CreateTexture(data);
            
            var fullImageRect = new Rect(0, 0, texture2d.width, texture2d.height);
            var centerPivot = new Vector2(0.5f, 0.5f);
            var sprite = Sprite.Create(texture2d, fullImageRect, centerPivot);
            return sprite;
        }
        
        private Texture2D CreateTexture(byte[] data)
        {
            var texture = new Texture2D(Screen.width, Screen.height);
            texture.LoadImage(data);
            return texture;
        }
    }
}
