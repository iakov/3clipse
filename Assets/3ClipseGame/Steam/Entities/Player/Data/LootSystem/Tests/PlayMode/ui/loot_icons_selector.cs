using System.Collections;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.Dropper;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.UI.Scripts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.Tests.PlayMode.ui
{
    public class loot_icons_selector
    {
        private LootIconsSelector _lootIconsSelector;
        private LootPool _pool;
        private ILootCreator _lootCreator;
        private Transform _spawnPosition;
        private LootDisplay _lootDisplay;

        [UnitySetUp]
        public IEnumerator Init()
        {
            SceneManager.LoadScene("loot_icons_selector_test_scene");
            yield return null;
            
            _lootIconsSelector = Object.FindObjectOfType<LootIconsSelector>();
            _pool = Object.FindObjectOfType<LootPool>();
            _lootDisplay = Object.FindObjectOfType<LootDisplay>();
            
            InitializeLoot();
        }

        private void InitializeLoot()
        { 
            _spawnPosition = InitializeSpawnLootPosition();
           var decals = new GameObject("Decals");
           _lootCreator = new PooledLootCreator(decals, _pool);
        }
        
        private Transform InitializeSpawnLootPosition()
        {
            var spawnLootPosition = new GameObject("Spawn Position").transform;
            spawnLootPosition.transform.position = new Vector3(5, 2, -4);
            return spawnLootPosition;
        }

        [UnityTest]
        public IEnumerator test_selecting_detected_icon()
        {
            var loot = _lootCreator.CreateLootObjectInPosition(_spawnPosition); 
            loot.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            var currentSelected = _lootIconsSelector.GetCurrentSelectedLoot();
            
            Assert.IsTrue(currentSelected.IsHighlighted());
        }

        [UnityTest]
        public IEnumerator test_selecting_first_when_two_detected()
        {
            var loot = _lootCreator.CreateLootObjectInPosition(_spawnPosition);
            loot.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            var firstIcon = _lootIconsSelector.GetCurrentSelectedLoot();

            var secondLoot = _lootCreator.CreateLootObjectInPosition(_spawnPosition);
            secondLoot.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            
            Assert.IsTrue(firstIcon.IsHighlighted());
        }

        [UnityTest]
        public IEnumerator test_selecting_next_object_when_previous_destroyed()
        {
            var firstLoot = _lootCreator.CreateLootObjectInPosition(_spawnPosition);
            firstLoot.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            var firstIcon = _lootIconsSelector.GetCurrentSelectedLoot();
            
            var secondLoot = _lootCreator.CreateLootObjectInPosition(_spawnPosition);
            secondLoot.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            var secondIcon = _lootDisplay.GetIconByObject(secondLoot.GetComponent<PickableLoot>());
            
            firstLoot.GetComponent<PickableLoot>().Disappear();
            yield return null;

            Assert.IsTrue(firstIcon == null);
            Assert.IsTrue(secondIcon.IsHighlighted());
        }
    }
}
