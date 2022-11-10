using System.Runtime.Serialization;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts.Surrogates
{
    public class QuaternionSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var serializedQuaternion = (Quaternion)obj;
            
            info.AddValue("x", serializedQuaternion.x);
            info.AddValue("y", serializedQuaternion.y);
            info.AddValue("z", serializedQuaternion.z);
            info.AddValue("w", serializedQuaternion.w);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var deserializedQuaternion = (Quaternion)obj;

            deserializedQuaternion.x = (float)info.GetValue("x", typeof(float));
            deserializedQuaternion.y = (float)info.GetValue("y", typeof(float));
            deserializedQuaternion.z = (float)info.GetValue("z", typeof(float));
            deserializedQuaternion.w = (float)info.GetValue("w", typeof(float));

            return deserializedQuaternion;
        }
    }
}
