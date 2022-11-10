using System.Collections;
using _3ClipseGame.Steam.Core.GameSource.Parts.Player;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts.SaveParts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class player_save_data
{
    private Player _player;

    [UnitySetUp]
    public IEnumerator Init()
    {
        SceneManager.LoadScene("save_serializer_test_scene");
        yield return null;

        _player = Object.FindObjectOfType<Player>();
    }

    [UnityTest]
    public IEnumerator test_save_and_load_with_player_null()
    {
        var playerSaveData = new PlayerSaveData(null);
        var randomPosition = Random.onUnitSphere;
        var randomRotation = Random.rotation;

        playerSaveData.Save();
        ApplyData(randomRotation, randomPosition);
        yield return null;

        playerSaveData.Load(_player);
        Assert.AreEqual(randomPosition, _player.transform.position);
        Assert.AreEqual(randomRotation, _player.transform.rotation);
    }

    private void ApplyData(Quaternion rotation, Vector3 position)
    {
        var objectTransform = _player.transform;
        objectTransform.rotation = rotation;
        objectTransform.position = position;
    }

    [UnityTest]
    public IEnumerator test_save_and_load_with_correct_data()
    {
        var playerTransform = _player.transform;
        var startPosition = playerTransform.position;
        var startRotation = playerTransform.rotation;
        var randomPosition = Random.onUnitSphere;
        var randomRotation = Random.rotation;

        var playerSaveData = new PlayerSaveData(_player);
        playerSaveData.Save();
        ApplyData(randomRotation, randomPosition);
        yield return null;

        playerSaveData.Load(_player);
        Assert.AreEqual(startPosition, _player.transform.position);
        Assert.AreEqual(startRotation, _player.transform.rotation);
    }

    [UnityTest]
    public IEnumerator test_load_null()
    {
        var playerSaveData = new PlayerSaveData(_player);
        var randomPosition = Random.onUnitSphere;
        var randomRotation = Random.rotation;

        playerSaveData.Save();
        ApplyData(randomRotation, randomPosition);
        yield return null;

        playerSaveData.Load(null);
        Assert.AreEqual(randomPosition, _player.transform.position);
        Assert.AreEqual(randomRotation, _player.transform.rotation);
    }
}