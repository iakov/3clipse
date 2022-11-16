using System.Collections;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Linq;
using Directory = System.IO.Directory;
using File = System.IO.File;
using Object = UnityEngine.Object;
using Random = System.Random;

public class save_manager
{
    private const string TestSaveName = "test_save";
    private static string TestSavePath => SaveSerializer.SavePath + TestSaveName;
    private SaveManager _saveManager;

    [UnitySetUp]
    public IEnumerator Init()
    {
        SceneManager.LoadScene("save_serializer_test_scene");
        yield return null;
        
        if (File.Exists(TestSavePath)) File.Delete(TestSavePath);
        _saveManager = Object.FindObjectOfType<SaveManager>();
    }

    // [UnityTest]
    // public IEnumerator test_create_save()
    // {
    //     var isSuccessful = _saveManager.TryCreateNewSave(TestSaveName, out var newSave);
    //     yield return null;
    //     
    //     Assert.IsTrue(isSuccessful);
    //     Assert.NotNull(newSave);
    //     Assert.IsTrue(IsContains(newSave));
    // }
    //
    // [UnityTest]
    // public IEnumerator test_create_save_twice()
    // {
    //     _saveManager.TryCreateNewSave(TestSaveName, out var newSave1);
    //     var isSuccessful = _saveManager.TryCreateNewSave(TestSaveName, out var newSave2);
    //     yield return null;
    //     
    //     Assert.IsFalse(isSuccessful);
    //     Assert.NotNull(newSave1);
    //     Assert.IsTrue(IsContains(newSave1));
    //     Assert.IsFalse(IsContains(newSave2));
    //     Assert.IsNull(newSave2);
    // }

    // [UnityTest]
    // public IEnumerator test_load_non_existing_save()
    // {
    //     var isSuccessful = _saveManager.TryLoadSave(FindNonExistingSaveName());
    //     Assert.IsFalse(isSuccessful);
    //     yield break;
    // }
    //
    // [UnityTest]
    // public IEnumerator test_load_save()
    // {
    //     _saveManager.TryCreateNewSave(TestSaveName, out _);
    //     yield return null;
    //     var isSuccessful = _saveManager.TryLoadSave(TestSaveName);
    //     Assert.IsTrue(isSuccessful);
    // }


    private bool IsContains(object save)
    {
        foreach (var element in _saveManager.GameSaves)
        {
            if (element == save) return true;
        }

        return false;
    }

    private string FindNonExistingSaveName()
    {
        var directories = Directory.GetFiles(SaveSerializer.SavePath);
        var randomName = GenerateRandomSaveName();
        
        while (directories.Contains(randomName))
        {
            randomName = GenerateRandomSaveName();
        }

        return randomName;
    }

    private string GenerateRandomSaveName()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[8];
        var random = new Random();

        for (var i = 0; i < stringChars.Length; i++)
            stringChars[i] = chars[random.Next(chars.Length)];

        return new string(stringChars);
    }
}