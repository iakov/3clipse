using _3ClipseGame.Steam.GameMechanics.LootSystem.InGame.Scripts.Dropper;
using _3ClipseGame.Steam.GameMechanics.LootSystem.InGame.Scripts.LootComponent;
using NUnit.Framework;
using UnityEngine;

namespace _3ClipseGame.Tests.LootTests.PlayMode.in_game
{
    public class pickable_loot
    {
        private PickableLoot _pickableLoot;
        private DropElement _dropElement;

        [SetUp]
        public void Init()
        {
            _pickableLoot = new GameObject().AddComponent<DePooledPickableLoot>();
            _dropElement = new DropElement(3, 0.5f);
            _pickableLoot.SetDropElement(_dropElement);
        }

        [Test]
        public void test_setting_element_tracked()
        {
            var previousAmount = _pickableLoot.GetAmount();
            var newDropElement = new DropElement(5, 1f);
            _pickableLoot.SetDropElement(newDropElement);
            var newAmount = _pickableLoot.GetAmount();

            Assert.IsFalse(previousAmount == newAmount);
        }

        [Test]
        public void test_setting_null_element_tracked()
        {
            var previousAmount = _pickableLoot.GetAmount();
            _pickableLoot.SetDropElement(null);
            var newAmount = _pickableLoot.GetAmount();

            Assert.AreEqual(previousAmount, newAmount);
        }

        [TearDown]
        public void Clear()
        {
            _pickableLoot.Disappear();
        }
    }
}