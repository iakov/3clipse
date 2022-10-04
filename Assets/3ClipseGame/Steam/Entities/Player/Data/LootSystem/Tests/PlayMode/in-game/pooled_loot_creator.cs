using System.Collections;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.Dropper;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.Tests.PlayMode.in_game
{
    public class pooled_loot_creator
    {
        private ILootCreator _lootCreator;
        private GameObject _decals;
        private GameObject _lootPool;

        [UnitySetUp]
        public IEnumerator Init()
        {
            SceneManager.LoadScene("pooled_loot_creator_test_scene");
            yield return null;
            
            _decals = new GameObject("Decals");
            _lootPool = Object.FindObjectOfType<LootPool>().gameObject;
            
            _lootCreator = new PooledLootCreator(_decals, _lootPool.GetComponent<LootPool>());
        }
        
        [UnityTest]
        public IEnumerator test_created_loot_position()
        {
            var lootElement = _lootCreator.CreateLootObjectInPosition(_decals.transform);
            yield return null;
            Assert.AreEqual(_decals.transform.position, lootElement.transform.position);
        }

        [UnityTest]
        public IEnumerator test_created_loot_parent()
        {
            var lootElement = _lootCreator.CreateLootObjectInPosition(_decals.transform);
            yield return null;
            Assert.IsTrue(lootElement.transform.IsChildOf(_decals.transform));
        }

        [UnityTest]
        public IEnumerator test_created_loot_disabled()
        {
            var lootElement = _lootCreator.CreateLootObjectInPosition(_decals.transform);
            yield return null;
            Assert.IsFalse(lootElement.activeInHierarchy);
        }
    }
}
