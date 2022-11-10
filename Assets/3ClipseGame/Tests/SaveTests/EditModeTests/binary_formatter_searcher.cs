using System.Runtime.Serialization;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts;
using NUnit.Framework;
using UnityEngine;

public class binary_formatter_searcher
{
    [Test]
    public void test_get_binary_formatter_vector3_surrogate()
    {
        var binaryFormatter = BinaryFormatterSearcher.GetBinaryFormatter();
        var streamingContext = new StreamingContext(StreamingContextStates.All);
        binaryFormatter.SurrogateSelector.GetSurrogate(typeof(Vector3), streamingContext, out var selector);
        Assert.NotNull(selector);
    }

    [Test]
    public void test_get_binary_formatter_quaternion_surrogate()
    {
        var binaryFormatter = BinaryFormatterSearcher.GetBinaryFormatter();
        var streamingContext = new StreamingContext(StreamingContextStates.All);
        binaryFormatter.SurrogateSelector.GetSurrogate(typeof(Quaternion), streamingContext, out var selector);
        Assert.NotNull(selector);
    }
}