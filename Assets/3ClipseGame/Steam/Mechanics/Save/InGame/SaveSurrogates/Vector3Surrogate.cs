using System.Runtime.Serialization;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.SaveSurrogates
{
    public class Vector3Surrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var serializedVector = (Vector3)obj;
            info.AddValue("x", serializedVector.x);
            info.AddValue("y", serializedVector.y);
            info.AddValue("z", serializedVector.z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var deserializedVector = (Vector3)obj;

            deserializedVector.x = (float)info.GetValue("x", typeof(float));
            deserializedVector.y = (float)info.GetValue("y", typeof(float));
            deserializedVector.z = (float)info.GetValue("z", typeof(float));

            return deserializedVector;
        }
    }
}