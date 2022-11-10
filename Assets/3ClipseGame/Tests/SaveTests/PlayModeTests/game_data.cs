using System.Collections;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class game_data
{
    private SerializationDependencies _dependencies;

    [UnitySetUp]
    public IEnumerator Init()
    {
        SceneManager.LoadScene("save_serializer_test_scene");
        yield return null;
        _dependencies = Object.FindObjectOfType<SerializationDependencies>();
    }

    [UnityTest]
    public IEnumerator test_save_null_data()
    {
        var gameData = new GameData(null);
        gameData.UpdateData();
        yield break;
    }

    [UnityTest]
    public IEnumerator test_save_correct_data()
    {
        var gameData = new GameData(_dependencies);
        gameData.UpdateData();
        yield return null;

        var transform = _dependencies.Player.transform;
        var previousPosition = transform.position;
        var previousRotation = transform.rotation;
        transform.position = Random.onUnitSphere;
        transform.rotation = Random.rotation;
        yield return null;
        
        gameData.Apply(_dependencies);
        Assert.AreEqual(previousPosition, transform.position);
        Assert.AreEqual(previousRotation, transform.rotation);
    }
}