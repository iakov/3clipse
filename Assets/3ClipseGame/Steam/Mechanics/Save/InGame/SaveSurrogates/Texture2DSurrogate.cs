using System.Runtime.Serialization;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.SaveSurrogates
{
    public class Texture2DSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var texture = (Texture2D)obj;
            var data = texture.EncodeToPNG();
            info.AddValue("data", data);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var data = (byte[])info.GetValue("data", typeof(byte[]));
            
            var texture = new Texture2D(1, 1);
            texture.LoadImage(data);
            return texture;
        }
    }
}