using System.Numerics;
using System.Runtime.Serialization;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.SaveSurrogates
{
    public class Vector3Surrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var serializedVector = (Vector3)obj;
            info.AddValue("x", serializedVector.X);
            info.AddValue("y", serializedVector.Y);
            info.AddValue("z", serializedVector.Z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var deserializedVector = (Vector3)obj;

            deserializedVector.X = (float)info.GetValue("x", typeof(float));
            deserializedVector.Y = (float)info.GetValue("y", typeof(float));
            deserializedVector.Z = (float)info.GetValue("z", typeof(float));

            return deserializedVector;
        }
    }
}