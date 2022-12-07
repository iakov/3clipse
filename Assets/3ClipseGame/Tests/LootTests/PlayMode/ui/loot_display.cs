using System.Collections;
using _3ClipseGame.Steam.Mechanics.LootSystem;
using _3ClipseGame.Steam.Mechanics.LootSystem.InGame.Scripts.Dropper;
using _3ClipseGame.Steam.Mechanics.LootSystem.InGame.Scripts.LootComponent;
using _3ClipseGame.Steam.Mechanics.LootSystem.UI.Scripts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace _3ClipseGame.Tests.LootTests.PlayMode.ui
{
    public class loot_display
    {
        private ILootCreator _lootCreator;
        private LootDisplay _lootDisplay;
        private Transform _spawnLootPosition;
        private GameObject _loot;

        [UnitySetUp]
        public IEnumerator Init()
        {
            SceneManager.LoadScene("loot_display_test_scene");
            yield return null;
            InitializeLootCreator();
            Debug.Log("1");
            _lootDisplay = Object.FindObjectOfType<LootDisplay>();
            Debug.Log("2");
            InitializeLoot();
            Debug.Log("3");
        }

        private void InitializeLootCreator()
        {
            var lootPool = Object.FindObjectOfType<LootPool>();
            var decalsObject = new GameObject("Decals");
            _lootCreator = new PooledLootCreator(decalsObject, lootPool);
        }

        private void InitializeLoot()
        {
            InitializeSpawnLootPosition();
            _loot = _lootCreator.CreateLootObjectInPosition(_spawnLootPosition);
            _loot.SetActive(true);
        }

        private void InitializeSpawnLootPosition()
        {
            _spawnLootPosition = new GameObject("Spawn Position").transform;
            _spawnLootPosition.transform.position = new Vector3(5, 2, -4);
        }

        [UnityTest]
        public IEnumerator test_get_icon_by_detected_loot()
        {
            yield return new WaitForSeconds(1f);

            var pickableLootElement = _loot.GetComponent<PickableLoot>();
            var lootIcon = _lootDisplay.GetIconByObject(pickableLootElement);

            Assert.IsTrue(ReferenceEquals(pickableLootElement, lootIcon.GetCurrentLoot()));
        }

        [UnityTest]
        public IEnumerator test_get_icon_by_index_normal()
        {
            yield return new WaitForSeconds(1f);

            var pickableLootElement = _loot.GetComponent<PickableLoot>();
            var lootIcon = _lootDisplay.GetIconByIndex(0);

            Assert.IsTrue(ReferenceEquals(pickableLootElement, lootIcon.GetCurrentLoot()));
        }

        [UnityTest]
        public IEnumerator test_get_icon_by_index_too_big()
        {
            var secondLoot = Object.Instantiate(_loot);
            secondLoot.transform.position += Vector3.up * 0.3f;

            yield return new WaitForSeconds(1f);

            var pickableLootElement = secondLoot.GetComponent<PickableLoot>();
            var lootIcon = _lootDisplay.GetIconByIndex(2);

            Assert.IsTrue(ReferenceEquals(pickableLootElement, lootIcon.GetCurrentLoot()));
        }

        [UnityTest]
        public IEnumerator test_get_icon_by_index_too_small()
        {
            var secondLoot = Object.Instantiate(_loot);
            secondLoot.transform.position += Vector3.up * 0.3f;

            var thirdLoot = Object.Instantiate(secondLoot);
            thirdLoot.transform.position += Vector3.up * 0.3f;

            yield return new WaitForSeconds(1f);

            var pickableLootElement = _loot.GetComponent<PickableLoot>();
            var lootIcon = _lootDisplay.GetIconByIndex(-2);

            Assert.IsTrue(ReferenceEquals(pickableLootElement, lootIcon.GetCurrentLoot()));
        }

        [UnityTest]
        public IEnumerator test_get_next_icon_when_only_one_detected()
        {
            yield return new WaitForSeconds(1f);

            var pickableLootElement = _loot.GetComponent<PickableLoot>();
            var lootIcon = _lootDisplay.GetIconByObject(pickableLootElement);
            var nextIcon = _lootDisplay.GetNextObject(lootIcon);

            Assert.IsTrue(ReferenceEquals(lootIcon, nextIcon));
        }

        [UnityTest]
        public IEnumerator test_get_previous_icon_when_only_one_detected()
        {
            yield return new WaitForSeconds(1f);

            var pickableLootElement = _loot.GetComponent<PickableLoot>();
            var lootIcon = _lootDisplay.GetIconByObject(pickableLootElement);
            var previousIcon = _lootDisplay.GetPreviousObject(lootIcon);

            Assert.IsTrue(ReferenceEquals(lootIcon, previousIcon));
        }

        [UnityTest]
        public IEnumerator test_get_next_icon()
        {
            var secondLoot = Object.Instantiate(_loot);
            secondLoot.transform.position += Vector3.up * 0.3f;

            yield return new WaitForSeconds(1f);

            var pickableLootElement = _loot.GetComponent<PickableLoot>();
            var lootIcon = _lootDisplay.GetIconByObject(pickableLootElement);
            var nextIcon = _lootDisplay.GetNextObject(lootIcon);

            Assert.IsFalse(ReferenceEquals(lootIcon, nextIcon));
            Assert.NotNull(nextIcon);
        }

        [UnityTest]
        public IEnumerator test_get_previous_icon()
        {
            var secondLoot = Object.Instantiate(_loot);
            secondLoot.transform.position += Vector3.up * 0.3f;

            yield return new WaitForSeconds(1f);

            var pickableLootElement = secondLoot.GetComponent<PickableLoot>();
            var lootIcon = _lootDisplay.GetIconByObject(pickableLootElement);
            var previousIcon = _lootDisplay.GetPreviousObject(lootIcon);

            Assert.IsFalse(ReferenceEquals(lootIcon, previousIcon));
            Assert.NotNull(previousIcon);
        }
    }
}
