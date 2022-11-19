using System.Runtime.Serialization;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.SaveSurrogates
{
    public class Texture2DSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var texture = (Texture2D)obj;
            var data = texture.EncodeToPNG();
            info.AddValue("data", data);
            info.AddValue("width", texture.width);
            info.AddValue("height", texture.height);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var imageWidth = (int)info.GetValue("width", typeof(int));
            var imageHeight = (int)info.GetValue("width", typeof(int));
            var data = (byte[])info.GetValue("data", typeof(byte[]));
            
            var texture = new Texture2D(imageWidth, imageHeight);
            texture.LoadImage(data);
            return texture;
        }
    }
}