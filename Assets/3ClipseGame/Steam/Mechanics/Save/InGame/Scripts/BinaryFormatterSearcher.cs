using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts.Surrogates;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts
{
    public static class BinaryFormatterSearcher
    {
        public static BinaryFormatter GetBinaryFormatter()
        {
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.SurrogateSelector = GetSurrogateSelector();
            return binaryFormatter;
        }
        
        private static SurrogateSelector GetSurrogateSelector()
        {
            var surrogateSelector = new SurrogateSelector();

            var vector3Surrogate = new Vector3Surrogate();
            var quaternionSurrogate = new QuaternionSurrogate();
            
            surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
            surrogateSelector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);

            return surrogateSelector;
        }
    }
}
