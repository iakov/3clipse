using System.Collections;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.Dropper;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.Tests.PlayMode
{
    public class pooled_loot_creator
    {
        private ILootCreator _lootInitializer;
        private GameObject _decals;
        private GameObject _lootPool;
        private GameObject _lootElement;

        [OneTimeSetUp]
        public void Setup()
        {
            _decals = new GameObject("Decals");
            _lootPool = new GameObject("Loot Pool");
            _lootPool.AddComponent<LootPool>();
        }
        
        [UnitySetUp]
        public IEnumerator Init()
        {
            InitiateLootInitializer();
            yield return null;
            _lootElement = _lootInitializer.CreateLootObjectInPosition(_decals.transform);
        }

        private void InitiateLootInitializer()
        {
            _lootInitializer = new PooledLootCreator(_decals, _lootPool.GetComponent<LootPool>());
        }
        
        [UnityTest]
        public IEnumerator test_created_loot_position()
        {
            Assert.AreEqual(_decals.transform.position, _lootElement.transform.position);
            yield break;
        }

        [UnityTest]
        public IEnumerator test_created_loot_parent()
        {
            Assert.IsTrue(_lootElement.transform.IsChildOf(_decals.transform));
            yield break;
        }

        [UnityTest]
        public IEnumerator test_created_loot_disabled()
        {
            Assert.IsFalse(_lootElement.activeInHierarchy);
            yield break;
        }

        [TearDown]
        public void Clear()
        {
            Object.Destroy(_lootElement);
        }

        [OneTimeTearDown]
        public void Destroy()
        {
            Object.Destroy(_decals);
            Object.Destroy(_lootPool);
        }
    }
}
