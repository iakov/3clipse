using System.Collections;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using File = System.IO.File;

public class save_serializer
{
    private string _testFilePath;
    private GameSave _gameSave;

    [SetUp]
    public void Init()
    {
        SetFields();
        File.Create(_testFilePath);
    }

    private void SetFields()
    {
        _testFilePath = Application.persistentDataPath + "/saves/test_file";
        _gameSave = new GameSave(_testFilePath, new SerializationDependencies());
    }

    [TearDown]
    public void Clear()
    {
        if (File.Exists(_testFilePath))
            File.Delete(_testFilePath);
    }

    [UnityTest]
    public IEnumerator test_serialize_non_existing_path()
    {
        File.Delete(_testFilePath);
        SaveSerializer.Serialize(_testFilePath, _gameSave);
        yield break;
    }

    [UnityTest]
    public IEnumerator test_serialize_non_existing_object()
    {
        SaveSerializer.Serialize(_testFilePath, null);
        yield break;
    }

    [UnityTest]
    public IEnumerator test_serialize_correct_params()
    {
        SaveSerializer.Serialize(_testFilePath, _gameSave);
        Assert.IsTrue(File.Exists(_testFilePath));
        yield break;
    }
}