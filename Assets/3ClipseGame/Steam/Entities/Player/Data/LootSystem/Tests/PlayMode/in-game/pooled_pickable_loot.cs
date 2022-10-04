using System.Collections;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.Tests.PlayMode.in_game
{
    public class pooled_pickable_loot
    {
        private LootPool _lootPool; 
        private GameObject _pooledLoot;
        
        [UnitySetUp]
        public IEnumerator Init()
        {
            SceneManager.LoadScene("pooled_loot_creator_test_scene");
            yield return null;
            _lootPool = Object.FindObjectOfType<LootPool>();
            _pooledLoot = _lootPool.GetPoolObject();
            _pooledLoot.SetActive(true);
        }
        
        [UnityTest]
        public IEnumerator test_disappear_loot()
        {
            _pooledLoot.GetComponent<PickableLoot>().Disappear();
            yield return null;

            var isEnabled = _pooledLoot.activeInHierarchy;
            Assert.IsFalse(isEnabled);
        }

        [UnityTest]
        public IEnumerator test_disappear_loot_event()
        {
            var isEventTriggered = false;
            _pooledLoot.GetComponent<PickableLoot>().Disappeared += (_) => { isEventTriggered = true; };
            
            _pooledLoot.GetComponent<PickableLoot>().Disappear();
            yield return null;
            
            Assert.IsTrue(isEventTriggered);
        }
    }
}
