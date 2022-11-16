using System.Collections;
using System.IO;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class game_save
{
    private GameSave _gameSave;
    private string _saveName = "testSave";
    private string _savePath => Application.persistentDataPath + _saveName;

    [UnitySetUp]
    public IEnumerator Init()
    {
        SceneManager.LoadScene("save_serializer_test_scene");
        yield return null;

        var serializationDependencies = Object.FindObjectOfType<SerializationDependencies>();
        _gameSave = new GameSave(serializationDependencies);
    }

    [TearDown]
    public void Clear()
    {
        if (File.Exists(SaveSerializer.SavePath + _saveName))
            File.Delete(SaveSerializer.SavePath + _saveName);
    }

    [UnityTest]
    public IEnumerator test_save_image_size()
    {
        var screenshot = ScreenCapture.CaptureScreenshotAsTexture();
        SaveSerializer.Serialize(_gameSave);
        yield return null;
        var newGameSave = SaveSerializer.Deserialize(_savePath);
        Assert.AreEqual(screenshot.width, newGameSave.Image.texture.width);
        Assert.AreEqual(screenshot.height, newGameSave.Image.texture.height);
    }

    [UnityTest]
    public IEnumerator test_save_date()
    {
        var date = DateFormatter.GetDateForSave();
        SaveSerializer.Serialize(_gameSave);
        yield return null;
        var newGameSave = SaveSerializer.Deserialize(_savePath);
        Assert.AreEqual(date, newGameSave.Date);
    }

    [UnityTest]
    public IEnumerator test_save_name()
    {
        var name = "test";
        SaveSerializer.Serialize(_gameSave);
        yield return null;
        var newGameSave = SaveSerializer.Deserialize(_savePath);
        Assert.AreEqual(name, newGameSave.LocationName);
    }

    // [UnityTest]
    // public IEnumerator test_save_image_raw_data()
    // {
    //     var screenshotData = ScreenCapture.CaptureScreenshotAsTexture().EncodeToPNG();
    //     SaveSerializer.Serialize(_path, _gameSave);
    //     yield return null;
    //     var newGameSave = SaveSerializer.Deserialize(_path);
    //     var rawImageData = newGameSave.Image.texture.EncodeToPNG();
    //
    //     Assert.IsTrue(rawImageData.Equals(screenshotData));
    // }
 }
