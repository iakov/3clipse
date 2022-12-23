using _3ClipseGame.Steam.GameMechanics.LootSystem.UI.Scripts.LootIcon;
using NUnit.Framework;
using UnityEngine;

namespace _3ClipseGame.Tests.LootTests.EditMode.ui
{
    public class resource_loot_icon
    {
        private ResourceLootIcon _icon;

        [SetUp]
        public void Init()
        {
            _icon = new GameObject().AddComponent<ResourceLootIcon>();
        }
    }
}
