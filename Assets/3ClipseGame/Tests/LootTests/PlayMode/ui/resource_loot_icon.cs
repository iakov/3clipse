using System.Collections;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.UI.Scripts.LootIcon;
using _3ClipseGame.Steam.Mechanics.LootSystem.InGame.Scripts.LootComponent;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class resource_loot_icon
{
    private LootIcon _icon;

    [UnitySetUp]
    public IEnumerator Init()
    {
        SceneManager.LoadScene("resource_loot_icon_test_scene");
        yield return null;
        _icon = Object.FindObjectOfType<LootIcon>();
    }

    [UnityTest]
    public IEnumerator test_is_active()
    {
        Assert.IsFalse(_icon.IsHighlighted());
        yield break;
    }

    [UnityTest]
    public IEnumerator test_set_highlight()
    {
        _icon.SetHighlight(false);
        Assert.IsFalse(_icon.IsHighlighted());
        yield break;
    }

    [UnityTest]
    public IEnumerator test_switch_track()
    {
        var pickableLoot = Object.FindObjectOfType<PickableLoot>();
        _icon.SwitchTrack(pickableLoot);
        yield return null;
        Assert.AreEqual(pickableLoot, _icon.GetCurrentLoot());
    }

    [UnityTest]
    public IEnumerator test_switch_track_to_null()
    {
        _icon.SwitchTrack(null);
        Assert.AreEqual(null, _icon.GetCurrentLoot());
        yield break;
    }

    [UnityTest]
    public IEnumerator test_switch_track_to_loot_with_null_resource()
    {
        var pickableLoot = Object.FindObjectOfType<PickableLoot>();
        pickableLoot.SetDropElement(null);
        _icon.SwitchTrack(pickableLoot);
        yield return null;
    }
}
