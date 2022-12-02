using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.UI.Scripts.LootIcon;
using _3ClipseGame.Steam.Mechanics.LootSystem.UI.Scripts.LootIcon;
using NUnit.Framework;
using UnityEngine;

public class resource_loot_icon
{
    private ResourceLootIcon _icon;

    [SetUp]
    public void Init()
    {
        _icon = new GameObject().AddComponent<ResourceLootIcon>();
    }
}
