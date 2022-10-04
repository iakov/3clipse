using System.Collections;
using System.Linq;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.Dropper;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.UI.Scripts;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.UI.Scripts.LootIcon;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.Tests.PlayMode.ui
{
    public class loot_highlighter
    {
        private LootHighlighter _lootHighlighter;
        private LootIcon _highlightedIcon;
        private LootIcon _notHighlightedIcon;

        [UnitySetUp]
        public IEnumerator Init()
        {
            SceneManager.LoadScene("resource_loot_icon_test_scene");
            yield return null;
            _lootHighlighter = new LootHighlighter();
            _highlightedIcon = Object.FindObjectsOfType<LootIcon>().First(icon => icon.IsHighlighted());
            _notHighlightedIcon = Object.FindObjectsOfType<LootIcon>().First(icon => !icon.IsHighlighted());
        }

        [UnityTest]
        public IEnumerator test_highlighting_new_icon_when_previous_null()
        {
            _lootHighlighter.SwitchHighlightedIcon(null, _notHighlightedIcon);
            yield return null;
            Assert.IsTrue(_notHighlightedIcon.IsHighlighted());
        }

        [UnityTest]
        public IEnumerator test_highlighting_new_icon_when_previous_not_null()
        {
            _lootHighlighter.SwitchHighlightedIcon(_highlightedIcon, _notHighlightedIcon);
            yield return null;
            
            Assert.IsFalse(_highlightedIcon.IsHighlighted());
            Assert.IsTrue(_notHighlightedIcon.IsHighlighted());
        }

        [UnityTest]
        public IEnumerator test_highlighting_null_icon()
        {
            _lootHighlighter.SwitchHighlightedIcon(null, null);
            yield break;
        }
    }
}
