using System.Collections;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.Tests.PlayMode
{
    public class pooled_pickable_loot
    {
        private LootPool _lootPool;
        private GameObject _pooledLoot;
        private PooledPickableLoot _pooledPickableLoot;
        
        [UnityTest]
        public IEnumerator test_disappear_loot()
        {
            _pooledPickableLoot.Disappear();
            yield return null;

            var isEnabled = _pooledLoot.activeInHierarchy;
            Assert.IsFalse(isEnabled);
        }

        [UnityTest]
        public IEnumerator test_disappear_loot_event()
        {
            var isEventTriggered = false;
            _pooledPickableLoot.Disappeared += (_) => { isEventTriggered = true; };
            
            _pooledPickableLoot.Disappear();
            yield return null;
            
            Assert.IsTrue(isEventTriggered);
        }

        [OneTimeSetUp]
        public void OnMount()
        {
            _lootPool = new GameObject("Loot Pool2").AddComponent<LootPool>();
        }

        [UnitySetUp]
        public IEnumerator Init()
        {
            yield return null;
            _pooledLoot = _lootPool.GetPoolObject();
            _pooledPickableLoot = _pooledLoot.GetComponent<PooledPickableLoot>();
            _pooledLoot.SetActive(true);
        }

        [OneTimeTearDown]
        public void Dismount()
        {
            Object.Destroy(_lootPool.gameObject);
        }
    }
}
