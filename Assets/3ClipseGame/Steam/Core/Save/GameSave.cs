using System;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.Save
{
    [Serializable]
    public class GameSave
    {
        public GameSave(string name, SerializationDependencies data)
        {
            Name = name;
            Date = string.Concat(DateTime.Now.ToShortDateString(), "  ", DateTime.Now.ToShortTimeString());
            
            _image = TakeScreenshot();
            _gameData = new GameData(data);
        }

        private byte[] TakeScreenshot()
        {
            var texture2d = ScreenCapture.CaptureScreenshotAsTexture();
            return texture2d.EncodeToPNG();
        }

        private Sprite CreateScreenshotSprite(byte[] data)
        {
            var texture2d = CreateTexture(data);
            var rect = new Rect(0, 0, texture2d.width, texture2d.height);
            var pivot = new Vector2(0.5f, 0.5f);
            var sprite = Sprite.Create(texture2d, rect, pivot);
            return sprite;
        }

        private Texture2D CreateTexture(byte[] data)
        {
            var texture = new Texture2D(Screen.width, Screen.height);
            texture.LoadImage(data);
            return texture;
        }
        
        public void ApplyData(SerializationDependencies data)
        {
            _gameData.Apply(data);
        }

        public void UpdateData()
        {
            _gameData.UpdateData();
        }
        
        public Sprite Image => CreateScreenshotSprite(_image);

        public readonly string Name;
        public readonly string Date;
        private readonly byte[] _image;
        private readonly GameData _gameData;
    }
}
