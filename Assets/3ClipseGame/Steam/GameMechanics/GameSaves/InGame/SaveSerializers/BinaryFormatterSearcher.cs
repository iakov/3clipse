using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using _3ClipseGame.Steam.GameMechanics.GameSaves.InGame.SaveSurrogates;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.GameSaves.InGame.SaveSerializers
{
    public class BinaryFormatterSearcher
    {
        public BinaryFormatter GetBinaryFormatter()
        {
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.SurrogateSelector = GetSurrogateSelector();
            return binaryFormatter;
        }

        private SurrogateSelector GetSurrogateSelector()
        {
            var surrogateSelector = new SurrogateSelector();

            var vector3Surrogate = new Vector3Surrogate();
            var quaternionSurrogate = new QuaternionSurrogate();
            var texture2dSurrogate = new Texture2DSurrogate();

            surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
            surrogateSelector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);
            surrogateSelector.AddSurrogate(typeof(Texture2D), new StreamingContext(StreamingContextStates.All), texture2dSurrogate);

            return surrogateSelector;
        }
    }
}