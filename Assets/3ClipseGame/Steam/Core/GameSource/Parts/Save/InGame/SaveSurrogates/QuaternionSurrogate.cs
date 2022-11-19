using System.Numerics;
using System.Runtime.Serialization;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.SaveSurrogates
{
    public class QuaternionSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var serializedQuaternion = (Quaternion)obj;

            info.AddValue("x", serializedQuaternion.X);
            info.AddValue("y", serializedQuaternion.Y);
            info.AddValue("z", serializedQuaternion.Z);
            info.AddValue("w", serializedQuaternion.W);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var deserializedQuaternion = (Quaternion)obj;

            deserializedQuaternion.X = (float)info.GetValue("x", typeof(float));
            deserializedQuaternion.Y = (float)info.GetValue("y", typeof(float));
            deserializedQuaternion.Z = (float)info.GetValue("z", typeof(float));
            deserializedQuaternion.W = (float)info.GetValue("w", typeof(float));

            return deserializedQuaternion;
        }
    }
}