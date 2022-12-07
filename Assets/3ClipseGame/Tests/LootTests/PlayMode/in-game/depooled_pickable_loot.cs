using System.Collections;
using _3ClipseGame.Steam.Mechanics.LootSystem.InGame.Scripts.LootComponent;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace _3ClipseGame.Tests.LootTests.PlayMode.in_game
{
    public class depooled_pickable_loot
    {
        private GameObject _lootObject;

        [UnityTest]
        public IEnumerator test_disappear_loot()
        {
            _lootObject.GetComponent<PickableLoot>().Disappear();
            yield return null;

            Assert.IsTrue(_lootObject == null);
        }

        [UnityTest]
        public IEnumerator test_disappear_loot_event()
        {
            var isEventInvoked = false;
            var pickableLoot = _lootObject.GetComponent<PickableLoot>();
            pickableLoot.Disappeared += (_) => { isEventInvoked = true; };

            pickableLoot.Disappear();
            yield return null;

            Assert.IsTrue(isEventInvoked);
        }


        [SetUp]
        public void Init()
        {
            _lootObject = new GameObject();
            _lootObject.AddComponent<DePooledPickableLoot>();
        }
    }
}